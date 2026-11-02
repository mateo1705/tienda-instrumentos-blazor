

using Final_Edi3.Models;

namespace Final_Edi3.Services;

/// <summary>
/// Servicio de acceso a datos para productos y categorías de la tienda.
/// </summary>
public interface IProductoService
{
    /// <summary>Se dispara después de crear, actualizar o eliminar un producto.</summary>
    event Action? OnChange;

    /// <summary>Obtiene una página de productos, con búsqueda y filtro opcionales.</summary>
    /// <param name="pagina">Número de página, empezando en 1.</param>
    /// <param name="tamanio">Cantidad de productos por página.</param>
    /// <param name="busqueda">Texto a buscar en el nombre o la marca. Si es nulo, no filtra.</param>
    /// <param name="categoriaId">Id de categoría para filtrar. Si es nulo, incluye todas.</param>
    /// <returns>Resultado paginado con los productos y el total de registros.</returns>
    Task<PagedResult<ProductoEntity>> ObtenerPaginado(
        int pagina, int tamanio, string? busqueda = null, int? categoriaId = null);

    /// <summary>Obtiene un producto con su categoría.</summary>
    /// <param name="id">Identificador del producto.</param>
    /// <returns>El producto, o <c>null</c> si no existe.</returns>
    Task<ProductoEntity?> ObtenerPorId(int id);

    /// <summary>Obtiene todas las categorías ordenadas por nombre.</summary>
    Task<List<Categoria>> ObtenerCategorias();

    /// <summary>Obtiene los productos con stock mayor a cero y menor o igual al umbral.</summary>
    /// <param name="umbral">Cantidad máxima de unidades para considerar stock bajo.</param>
    Task<List<ProductoEntity>> ObtenerStockBajo(int umbral);

    /// <summary>Calcula el total de productos, los que no tienen stock y el valor del inventario.</summary>
    Task<EstadisticasInventario> ObtenerEstadisticas();

    /// <summary>Crea un nuevo producto.</summary>
    /// <param name="producto">Datos del producto. Debe incluir un <c>CategoriaId</c> válido.</param>
    /// <returns>El producto creado, con el Id asignado.</returns>
    /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Si falla el guardado.</exception>
    Task<ProductoEntity> Crear(ProductoEntity producto);

    /// <summary>Actualiza los datos de un producto existente.</summary>
    /// <param name="producto">Producto con los valores nuevos.</param>
    /// <returns>El producto actualizado.</returns>
    /// <exception cref="InvalidOperationException">Si el producto no existe.</exception>
    /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Si falla el guardado.</exception>
    Task<ProductoEntity> Actualizar(ProductoEntity producto);

    /// <summary>Elimina un producto. Si no existe, no hace nada.</summary>
    /// <param name="id">Identificador del producto a eliminar.</param>
    /// <exception cref="Microsoft.EntityFrameworkCore.DbUpdateException">Si falla el borrado.</exception>
    Task Eliminar(int id);
}