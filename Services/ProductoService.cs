using Final_Edi3.Data;
using Final_Edi3.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Edi3.Services;

public class ProductoService : IProductoService
{
    private readonly IDbContextFactory<TiendaDbContext> _factory;
    private readonly ILogger<ProductoService> _logger;

    public event Action? OnChange;

    public ProductoService(IDbContextFactory<TiendaDbContext> factory,
                           ILogger<ProductoService> logger)
    {
        _factory = factory;
        _logger = logger;
    }

    public async Task<PagedResult<ProductoEntity>> ObtenerPaginado(
        int pagina, int tamanio, string? busqueda = null, int? categoriaId = null)
    {
        if (pagina < 1) pagina = 1;

        await using var ctx = await _factory.CreateDbContextAsync();

        IQueryable<ProductoEntity> query = ctx.Productos.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var patron = $"%{busqueda.Trim()}%";
            query = query.Where(p => EF.Functions.Like(p.Nombre, patron)
                                  || EF.Functions.Like(p.Marca, patron));
        }

        if (categoriaId.HasValue)
            query = query.Where(p => p.CategoriaId == categoriaId.Value);

        var total = await query.CountAsync();

        var items = await query
            .Include(p => p.Categoria)
            .OrderBy(p => p.Nombre)
            .Skip((pagina - 1) * tamanio)
            .Take(tamanio)
            .ToListAsync();

        return new PagedResult<ProductoEntity>
        {
            Items = items,
            Pagina = pagina,
            TamanioPagina = tamanio,
            TotalRegistros = total
        };
    }

    public async Task<ProductoEntity?> ObtenerPorId(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Productos
            .Include(p => p.Categoria)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<List<Categoria>> ObtenerCategorias()
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Categorias.AsNoTracking().OrderBy(c => c.Nombre).ToListAsync();
    }

    public async Task<List<ProductoEntity>> ObtenerStockBajo(int umbral)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        return await ctx.Productos
            .AsNoTracking()
            .Where(p => p.Stock > 0 && p.Stock <= umbral)
            .OrderBy(p => p.Stock)
            .ToListAsync();
    }

    public async Task<EstadisticasInventario> ObtenerEstadisticas()
    {
        await using var ctx = await _factory.CreateDbContextAsync();

        // SQLite no puede sumar decimales en SQL, así que traemos solo
        // precio y stock y calculamos en memoria (para este catálogo alcanza).
        var datos = await ctx.Productos
            .AsNoTracking()
            .Select(p => new { p.Precio, p.Stock })
            .ToListAsync();

        return new EstadisticasInventario(
            datos.Count,
            datos.Count(d => d.Stock == 0),
            datos.Sum(d => d.Precio * d.Stock));
    }

    public async Task<ProductoEntity> Crear(ProductoEntity producto)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        try
        {
            producto.Categoria = null; // solo se guarda la clave foránea
            ctx.Productos.Add(producto);
            await ctx.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al crear el producto {Nombre}", producto.Nombre);
            throw;
        }

        _logger.LogInformation("Producto creado: {Nombre} (Id {Id})", producto.Nombre, producto.Id);
        OnChange?.Invoke();
        return producto;
    }

    public async Task<ProductoEntity> Actualizar(ProductoEntity producto)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        try
        {
            var existente = await ctx.Productos.FindAsync(producto.Id)
                ?? throw new InvalidOperationException($"No existe el producto {producto.Id}.");

            // Copia solo las propiedades simples (incluye CategoriaId)
            ctx.Entry(existente).CurrentValues.SetValues(producto);
            await ctx.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al actualizar el producto {Id}", producto.Id);
            throw;
        }

        OnChange?.Invoke();
        return producto;
    }

    public async Task Eliminar(int id)
    {
        await using var ctx = await _factory.CreateDbContextAsync();
        try
        {
            var existente = await ctx.Productos.FindAsync(id);
            if (existente is null) return;

            ctx.Productos.Remove(existente);
            await ctx.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "Error al eliminar el producto {Id}", id);
            throw;
        }

        OnChange?.Invoke();
    }
}