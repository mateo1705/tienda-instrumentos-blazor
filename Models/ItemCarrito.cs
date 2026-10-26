namespace Final_Edi3.Models
{
    public class ItemCarrito
    {
        public ProductoEntity Producto { get; set; } = default!;
        public int Cantidad { get; set; }
        public decimal Subtotal => Producto.Precio * Cantidad;
    
    

    }
}
