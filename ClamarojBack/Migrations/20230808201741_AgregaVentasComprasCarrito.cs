using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClamarojBack.Migrations
{
    /// <inheritdoc />
    public partial class AgregaVentasComprasCarrito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UnidadesMedida",
                columns: table => new
                {
                    IdUnidadMedida = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesMedida", x => x.IdUnidadMedida);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rfc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    Domicilio = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: true),
                    Rfc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    TipoPago = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TipoEnvio = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    TipoPedido = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => new { x.IdPedido, x.Fecha });
                    table.ForeignKey(
                        name: "FK_Pedidos_Estatus_IdStatus",
                        column: x => x.IdStatus,
                        principalTable: "Estatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pedidos_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proveedores",
                columns: table => new
                {
                    IdProveedor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rfc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    RazonSocial = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proveedores", x => x.IdProveedor);
                    table.ForeignKey(
                        name: "FK_Proveedores_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolesUsuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolesUsuarios", x => new { x.IdRol, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Roles_IdRol",
                        column: x => x.IdRol,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolesUsuarios_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carritos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carritos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ventas_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ventas_Pedidos_IdPedido_Fecha",
                        columns: x => new { x.IdPedido, x.Fecha },
                        principalTable: "Pedidos",
                        principalColumns: new[] { "IdPedido", "Fecha" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Pedidos_IdPedido_Fecha",
                        columns: x => new { x.IdPedido, x.Fecha },
                        principalTable: "Pedidos",
                        principalColumns: new[] { "IdPedido", "Fecha" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Compras_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MateriasPrimas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Perecedero = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CantMinima = table.Column<int>(type: "int", nullable: false),
                    CantMaxima = table.Column<int>(type: "int", nullable: false),
                    IdUnidadMedida = table.Column<int>(type: "int", nullable: false),
                    UnidadMedidaIdUnidadMedida = table.Column<int>(type: "int", nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    ProveedorIdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasPrimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasPrimas_Proveedores_ProveedorIdProveedor",
                        column: x => x.ProveedorIdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriasPrimas_UnidadesMedida_UnidadMedidaIdUnidadMedida",
                        column: x => x.UnidadMedidaIdUnidadMedida,
                        principalTable: "UnidadesMedida",
                        principalColumn: "IdUnidadMedida",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    IdProducto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(45)", maxLength: 45, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    Precio = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: false),
                    Merma = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdCarrito = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
                    table.ForeignKey(
                        name: "FK_Productos_Carritos_IdCarrito",
                        column: x => x.IdCarrito,
                        principalTable: "Carritos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    IdDetallePedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePedidos", x => x.IdDetallePedido);
                    table.ForeignKey(
                        name: "FK_DetallePedido_Pedido",
                        columns: x => new { x.IdPedido, x.Fecha },
                        principalTable: "Pedidos",
                        principalColumns: new[] { "IdPedido", "Fecha" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DetallePedidos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recetas", x => x.IdReceta);
                    table.ForeignKey(
                        name: "FK_Recetas_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdMateriaPrima = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => new { x.IdReceta, x.IdMateriaPrima });
                    table.ForeignKey(
                        name: "FK_Ingrediente_MateriasPrimas_IdMateriaPrima",
                        column: x => x.IdMateriaPrima,
                        principalTable: "MateriasPrimas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingrediente_Recetas_IdReceta",
                        column: x => x.IdReceta,
                        principalTable: "Recetas",
                        principalColumn: "IdReceta",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Carritos_IdCliente",
                table: "Carritos",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdUsuario",
                table: "Clientes",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdPedido_Fecha",
                table: "Compras",
                columns: new[] { "IdPedido", "Fecha" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compras_IdProveedor",
                table: "Compras",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_IdPedido_Fecha",
                table: "DetallePedidos",
                columns: new[] { "IdPedido", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_DetallePedidos_IdProducto",
                table: "DetallePedidos",
                column: "IdProducto");

            migrationBuilder.CreateIndex(
                name: "IX_Estatus_Nombre",
                table: "Estatus",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingrediente_IdMateriaPrima",
                table: "Ingrediente",
                column: "IdMateriaPrima");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasPrimas_Codigo",
                table: "MateriasPrimas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MateriasPrimas_ProveedorIdProveedor",
                table: "MateriasPrimas",
                column: "ProveedorIdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasPrimas_UnidadMedidaIdUnidadMedida",
                table: "MateriasPrimas",
                column: "UnidadMedidaIdUnidadMedida");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdStatus",
                table: "Pedidos",
                column: "IdStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_IdUsuario",
                table: "Pedidos",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_Codigo",
                table: "Productos",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_IdCarrito",
                table: "Productos",
                column: "IdCarrito");

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_IdUsuario",
                table: "Proveedores",
                column: "IdUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proveedores_RazonSocial",
                table: "Proveedores",
                column: "RazonSocial",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_Codigo",
                table: "Recetas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recetas_IdProducto",
                table: "Recetas",
                column: "IdProducto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Nombre",
                table: "Roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolesUsuarios_IdUsuario",
                table: "RolesUsuarios",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UnidadesMedida_Nombre",
                table: "UnidadesMedida",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Correo",
                table: "Usuarios",
                column: "Correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdCliente",
                table: "Ventas",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_IdPedido_Fecha",
                table: "Ventas",
                columns: new[] { "IdPedido", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "DetallePedidos");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.DropTable(
                name: "RolesUsuarios");

            migrationBuilder.DropTable(
                name: "Ventas");

            migrationBuilder.DropTable(
                name: "MateriasPrimas");

            migrationBuilder.DropTable(
                name: "Recetas");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropTable(
                name: "Proveedores");

            migrationBuilder.DropTable(
                name: "UnidadesMedida");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Estatus");

            migrationBuilder.DropTable(
                name: "Carritos");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
