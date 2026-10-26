using System.ComponentModel.DataAnnotations;

namespace Final_Edi3.Models;

public class Categoria
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
    [StringLength(50)]
    public string Nombre { get; set; } = string.Empty;

    // Propiedad de navegación: una categoría tiene muchos productos
    public List<ProductoEntity> Productos { get; set; } = new();
}