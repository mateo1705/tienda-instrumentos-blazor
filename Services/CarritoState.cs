using Final_Edi3.Models;

namespace Final_Edi3.Services
{
    public class CarritoState
    {

        private readonly List<ItemCarrito> _items = new();

        public IReadOnlyList<ItemCarrito> Items => _items.AsReadOnly();
        public int CantidadTotal => _items.Sum(i => i.Cantidad);
        public decimal Total => _items.Sum(i => i.Subtotal);

        public event Action? OnChange;

        /// <summary>Agrega una unidad. Devuelve false si no hay más stock.</summary>
        public bool Agregar(ProductoEntity producto)
        {
            var item = _items.FirstOrDefault(i => i.Producto.Id == producto.Id);

            if (item is null)
            {
                if (producto.Stock < 1) return false;
                _items.Add(new ItemCarrito { Producto = producto, Cantidad = 1 });
            }
            else
            {
                if (item.Cantidad >= producto.Stock) return false;
                item.Cantidad++;
            }

            NotificarCambio();
            return true;
        }

        public void Disminuir(int productoId)
        {
            var item = _items.FirstOrDefault(i => i.Producto.Id == productoId);
            if (item is null) return;

            item.Cantidad--;
            if (item.Cantidad <= 0) _items.Remove(item);
            NotificarCambio();
        }

        public void Quitar(int productoId)
        {
            _items.RemoveAll(i => i.Producto.Id == productoId);
            NotificarCambio();
        }

        public void Vaciar()
        {
            _items.Clear();
            NotificarCambio();
        }

        private void NotificarCambio() => OnChange?.Invoke();
    }
}
