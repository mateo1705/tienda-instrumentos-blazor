using System.ComponentModel.DataAnnotations;

namespace Final_Edi3.Models;

public class ProductoEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(80, MinimumLength = 3,
        ErrorMessage = "Debe tener entre 3 y 80 caracteres")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "La marca es obligatoria")]
    [StringLength(50, ErrorMessage = "Máximo 50 caracteres")]
    public string Marca { get; set; } = string.Empty;

    [StringLength(300, ErrorMessage = "Máximo 300 caracteres")]
    public string Descripcion { get; set; } = string.Empty;

    [Range(1, 100000000, ErrorMessage = "El precio debe ser mayor a 0")]
    public decimal Precio { get; set; }

    [Range(0, 1000, ErrorMessage = "El stock debe estar entre 0 y 1000")]
    public int Stock { get; set; }

    // Clave foránea + propiedad de navegación
    [Range(1, int.MaxValue, ErrorMessage = "Seleccioná una categoría")]
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
}