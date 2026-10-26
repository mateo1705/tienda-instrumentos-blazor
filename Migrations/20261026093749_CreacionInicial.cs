using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Final_Edi3.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Marca = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    Precio = table.Column<double>(type: "REAL", nullable: false),
                    Stock = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoriaId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Guitarras" },
                    { 2, "Teclados" },
                    { 3, "Percusión" },
                    { 4, "Vientos" },
                    { 5, "Accesorios" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "CategoriaId", "Descripcion", "Marca", "Nombre", "Precio", "Stock" },
                values: new object[,]
                {
                    { 1, 1, "Ideal para principiantes.", "Gracia", "Guitarra Criolla", 185000.0, 5 },
                    { 2, 1, "Tres micrófonos single coil.", "Squier", "Guitarra Eléctrica Stratocaster", 520000.0, 3 },
                    { 3, 2, "Teclas sensitivas y 400 sonidos.", "Yamaha", "Teclado 61 teclas", 320000.0, 3 },
                    { 4, 2, "Teclado con acción martillo.", "Casio", "Piano Digital 88 teclas", 780000.0, 1 },
                    { 5, 3, "Madera de haya, sonido profundo.", "Fonseca", "Cajón Peruano", 95000.0, 0 },
                    { 6, 3, "Incluye platillos y banqueta.", "Pearl", "Batería Acústica 5 piezas", 1450000.0, 2 },
                    { 7, 4, "Cuerpo plateado, con estuche.", "Armstrong", "Flauta Traversa", 410000.0, 2 },
                    { 8, 4, "Ideal para estudiantes avanzados.", "Yamaha", "Saxo Alto", 1900000.0, 1 },
                    { 9, 5, "Calibre 010-046.", "Ernie Ball", "Set de cuerdas para guitarra", 18000.0, 40 },
                    { 10, 5, "Con clip para púa o cabezal.", "Snark", "Afinador cromático", 22000.0, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categorias_Nombre",
                table: "Categorias",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Nombre",
                table: "Productos",
                column: "Nombre");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
