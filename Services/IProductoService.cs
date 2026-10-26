using Final_Edi3.Models;

namespace Final_Edi3.Services;

/// <summary>Acceso a datos de productos y categorías.</summary>
public interface IProductoService
{
    /// <summary>Se dispara tras crear, actualizar o eliminar un producto.</summary>
    event Action? OnChange;

    /// <summary>Productos paginados, con búsqueda por nombre o marca y filtro por categoría.</summary>
    Task<PagedResult<ProductoEntity>> ObtenerPaginado(
        int pagina, int tamanio, string? busqueda = null, int? categoriaId = null);

    Task<ProductoEntity?> ObtenerPorId(int id);
    Task<List<Categoria>> ObtenerCategorias();
    Task<List<ProductoEntity>> ObtenerStockBajo(int umbral);
    Task<EstadisticasInventario> ObtenerEstadisticas();

    Task<ProductoEntity> Crear(ProductoEntity producto);
    Task<ProductoEntity> Actualizar(ProductoEntity producto);
    Task Eliminar(int id);
}