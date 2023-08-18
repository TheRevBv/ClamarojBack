using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClamarojBack.Migrations
{
    /// <inheritdoc />
    public partial class FixForeignKeyProductoInDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoIdProducto",
                table: "DetallePedidos");

            migrationBuilder.DropIndex(
                name: "IX_DetallePedidos_ProductoIdProducto",
                table: "DetallePedidos");

            migrationBuilder.DropColumn(
                name: "ProductoIdProducto",
                table: "DetallePedidos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoIdProducto",
                table: "DetallePedidos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_ProductoIdProducto",
                table: "DetallePedidos",
                column: "ProductoIdProducto");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallePedidos_Productos_ProductoIdProducto",
                table: "DetallePedidos",
                column: "ProductoIdProducto",
                principalTable: "Productos",
                principalColumn: "IdProducto");
        }
    }
}
