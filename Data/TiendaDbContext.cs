using Final_Edi3.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Final_Edi3.Data;

public class TiendaDbContext : DbContext
{
    public TiendaDbContext(DbContextOptions<TiendaDbContext> options)
        : base(options) { }

    public DbSet<ProductoEntity> Productos => Set<ProductoEntity>();
    public DbSet<Categoria> Categorias => Set<Categoria>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<Categoria>(e =>
        {
            e.HasIndex(c => c.Nombre).IsUnique();
        });

        mb.Entity<ProductoEntity>(e =>
        {
            // Relación uno-a-muchos con Fluent API
            e.HasOne(p => p.Categoria)
             .WithMany(c => c.Productos)
             .HasForeignKey(p => p.CategoriaId)
             .OnDelete(DeleteBehavior.Restrict); // no se puede borrar una categoría con productos

            e.HasIndex(p => p.Nombre);

            // SQLite no puede ordenar ni comparar decimales en SQL.
            // Guardándolos como double, los ORDER BY y filtros por precio funcionan.
            e.Property(p => p.Precio).HasConversion<double>();
        });

        // ---------- Datos de seed: 5 categorías y 10 productos ----------
        mb.Entity<Categoria>().HasData(
            new Categoria { Id = 1, Nombre = "Guitarras" },
            new Categoria { Id = 2, Nombre = "Teclados" },
            new Categoria { Id = 3, Nombre = "Percusión" },
            new Categoria { Id = 4, Nombre = "Vientos" },
            new Categoria { Id = 5, Nombre = "Accesorios" }
        );

        mb.Entity<ProductoEntity>().HasData(
            new ProductoEntity { Id = 1, Nombre = "Guitarra Criolla", Marca = "Gracia", CategoriaId = 1, Precio = 185000, Stock = 5, Descripcion = "Ideal para principiantes." },
            new ProductoEntity { Id = 2, Nombre = "Guitarra Eléctrica Stratocaster", Marca = "Squier", CategoriaId = 1, Precio = 520000, Stock = 3, Descripcion = "Tres micrófonos single coil." },
            new ProductoEntity { Id = 3, Nombre = "Teclado 61 teclas", Marca = "Yamaha", CategoriaId = 2, Precio = 320000, Stock = 3, Descripcion = "Teclas sensitivas y 400 sonidos." },
            new ProductoEntity { Id = 4, Nombre = "Piano Digital 88 teclas", Marca = "Casio", CategoriaId = 2, Precio = 780000, Stock = 1, Descripcion = "Teclado con acción martillo." },
            new ProductoEntity { Id = 5, Nombre = "Cajón Peruano", Marca = "Fonseca", CategoriaId = 3, Precio = 95000, Stock = 0, Descripcion = "Madera de haya, sonido profundo." },
            new ProductoEntity { Id = 6, Nombre = "Batería Acústica 5 piezas", Marca = "Pearl", CategoriaId = 3, Precio = 1450000, Stock = 2, Descripcion = "Incluye platillos y banqueta." },
            new ProductoEntity { Id = 7, Nombre = "Flauta Traversa", Marca = "Armstrong", CategoriaId = 4, Precio = 410000, Stock = 2, Descripcion = "Cuerpo plateado, con estuche." },
            new ProductoEntity { Id = 8, Nombre = "Saxo Alto", Marca = "Yamaha", CategoriaId = 4, Precio = 1900000, Stock = 1, Descripcion = "Ideal para estudiantes avanzados." },
            new ProductoEntity { Id = 9, Nombre = "Set de cuerdas para guitarra", Marca = "Ernie Ball", CategoriaId = 5, Precio = 18000, Stock = 40, Descripcion = "Calibre 010-046." },
            new ProductoEntity { Id = 10, Nombre = "Afinador cromático", Marca = "Snark", CategoriaId = 5, Precio = 22000, Stock = 25, Descripcion = "Con clip para púa o cabezal." }
        );
    }
}