using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClamarojBack.Migrations
{
    /// <inheritdoc />
    public partial class Final : Migration
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
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.IdProducto);
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
                    Password = table.Column<byte[]>(type: "VARBINARY(MAX)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recetas",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "DATETIME", nullable: false)
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
                    Fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaEntrega = table.Column<DateTime>(type: "DATETIME", nullable: false),
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
                    IdCarrito = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCliente = table.Column<int>(type: "int", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carritos", x => x.IdCarrito);
                    table.ForeignKey(
                        name: "FK_Carritos_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Carritos_Productos_IdProducto",
                        column: x => x.IdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DetallePedidos",
                columns: table => new
                {
                    IdDetallePedido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPedido = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    IdProducto = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Subtotal = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    ProductoIdProducto = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_DetallePedidos_Productos_ProductoIdProducto",
                        column: x => x.ProductoIdProducto,
                        principalTable: "Productos",
                        principalColumn: "IdProducto");
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
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
                    Fecha = table.Column<DateTime>(type: "DATETIME", nullable: false),
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
                    Precio = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Foto = table.Column<string>(type: "TEXT", nullable: false),
                    IdProveedor = table.Column<int>(type: "int", nullable: false),
                    IdStatus = table.Column<int>(type: "int", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    FechaModificacion = table.Column<DateTime>(type: "DATETIME", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriasPrimas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MateriasPrimas_Proveedores_IdProveedor",
                        column: x => x.IdProveedor,
                        principalTable: "Proveedores",
                        principalColumn: "IdProveedor",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MateriasPrimas_UnidadesMedida_IdUnidadMedida",
                        column: x => x.IdUnidadMedida,
                        principalTable: "UnidadesMedida",
                        principalColumn: "IdUnidadMedida",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    IdReceta = table.Column<int>(type: "int", nullable: false),
                    IdMateriaPrima = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
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
                name: "IX_Carritos_IdProducto",
                table: "Carritos",
                column: "IdProducto");

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
                name: "IX_DetallePedidos_ProductoIdProducto",
                table: "DetallePedidos",
                column: "ProductoIdProducto");

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
                name: "IX_MateriasPrimas_IdProveedor",
                table: "MateriasPrimas",
                column: "IdProveedor");

            migrationBuilder.CreateIndex(
                name: "IX_MateriasPrimas_IdUnidadMedida",
                table: "MateriasPrimas",
                column: "IdUnidadMedida");

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

            AgregarStoredProcedures(migrationBuilder);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Carritos");

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
                name: "Clientes");

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
                name: "Usuarios");

            EliminarStoredProcedures(migrationBuilder);
        }

        protected void AgregarStoredProcedures(MigrationBuilder migrationBuilder)
        {
            //Function to Ids to table
            migrationBuilder.Sql("create FUNCTION [dbo].[fxConvertIDsToTable](@Indices AS VARCHAR(MAX))\r\nRETURNS @rTable TABLE (IdReturn int)\r\nBEGIN\r\n\tDECLARE @Count as int\r\n\tDECLARE @Number as VarChar(128)\r\n\tDECLARE @Length as int\r\n\tDECLARE @Start as int\r\n\tSET @Length=0\r\n\tSET @Start=1\t\t\r\n\tSET @Count = 1\r\n\tSET @Number = ''\r\n\t\r\n\tWHILE @Count <=Len(@Indices)  \r\n\tBEGIN\t\t\r\n\t\tIF SUBSTRING(@Indices,@Count,1) = ',' AND @Number <> ''\r\n\t\t\tBEGIN                \r\n\t\t\t\tINSERT INTO @rTable VALUES(CAST(@Number AS INT))\r\n\t\t\t\tSET @Length=0\r\n\t\t\t\tSET @Start = @Count+1\r\n\t\t\t\tSET @Number=''\r\n\t\t\tEND\r\n\t\tELSE\r\n\t\t\tBEGIN                \r\n\t\t\t\tSET @Length=@Length+1\r\n\t\t\t\tSET @Number = SUBSTRING(@Indices,@Start,@Length)\t\t\t\t\t\r\n\t\t\tEND\r\n\t\tSET @Count = @Count + 1\r\n    END \r\n\r\n\tIF @Number <> ''\r\n    BEGIN\r\n\t\tINSERT INTO @rTable VALUES(CAST(@Number AS INT))\r\n\tEND   \r\n\t\t\r\n\tRETURN  \r\nEND\r\n");

            //Estatus stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.EstatusUPD\r\n    @Id int,\r\n    @Nombre varchar(50)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    IF EXISTS(SELECT * FROM dbo.Estatus WHERE Id = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Estatus\r\n        SET Nombre = @Nombre\r\n        WHERE Id = @Id\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.Estatus(Nombre)\r\n        VALUES(@Nombre)    \r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.EstatusDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    IF EXISTS(SELECT * FROM dbo.Estatus WHERE Id = @Id)\r\n    BEGIN\r\n        DELETE FROM dbo.Estatus\r\n        WHERE Id = @Id\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetEstatuses()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT Id as idStatus, Nombre as nombre\r\n    FROM dbo.Estatus\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetEstatus\r\n(\r\n    @Id int\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT Id as idStatus, Nombre as nombre\r\n    FROM dbo.Estatus WHERE Id = @Id\r\n)");

            //Usuarios stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.UsuariosUPD\r\n\t@Id int out,\r\n\t@Nombre varchar(50),\r\n\t@Apellido varchar(50),\r\n\t@Correo varchar(50),\r\n\t@FechaNacimiento datetime,\r\n\t@Foto TEXT,\r\n\t@IdStatus int,\r\n\t@Password varbinary(MAX),\r\n\t@IdRoles varchar(max) \r\nAS\r\nBEGIN\r\n\tSET NOCOUNT ON;\r\n\r\n\t-- Eliminar registros asociados al usuario en la tabla RolesUsuario\r\n\tDELETE FROM dbo.RolesUsuarios WHERE IdUsuario = @Id\r\n\tDECLARE @PassAntigua varbinary(MAX), @PasswordEncrypt varbinary(MAX)\r\n\t\r\n\r\n\tIF EXISTS(SELECT Id FROM dbo.Usuarios WHERE Id = @Id)\r\n\tBEGIN\r\n\t\tSELECT @PassAntigua = [Password] FROM dbo.Usuarios WHERE Id = @Id\r\n\t\tSET @PasswordEncrypt = HASHBYTES('SHA2_256', @Password)\r\n\r\n\t\tSET @Password = IIF(@PassAntigua = @Password, @Password, @PasswordEncrypt)\r\n\r\n\t\tUPDATE dbo.Usuarios\r\n\t\tSET Nombre = @Nombre,\r\n\t\t\tApellido = @Apellido,\r\n\t\t\tCorreo = @Correo,\r\n\t\t\tFechaNacimiento = @FechaNacimiento,\r\n\t\t\tFoto = @Foto,\r\n\t\t\tIdStatus = @IdStatus,\r\n\t\t\t[Password] = @Password\r\n\t\tWHERE Id = @Id\r\n\tEND\r\n\tELSE\r\n\tBEGIN\r\n\t\tINSERT INTO dbo.Usuarios(Nombre, Apellido, Correo, FechaNacimiento, Foto, IdStatus, FechaRegistro, [Password])\r\n\t\tVALUES (@Nombre, @Apellido, @Correo, @FechaNacimiento, @Foto, @IdStatus, GETDATE(), HASHBYTES('SHA2_256', @Password))\r\n\r\n\t\t-- Obtener el ID del usuario recién insertado\r\n\t\tSET @Id = SCOPE_IDENTITY()\r\n\tEND\r\n\tIF @IdRoles <> ''\r\n\tBEGIN\r\n\t\t-- Insertar los registros en la tabla RolesUsuario\r\n\t\tINSERT INTO dbo.RolesUsuarios (IdRol, IdUsuario)\r\n\t\tSELECT IdReturn, @Id FROM dbo.fxConvertIDsToTable(@IdRoles)\r\n\tEND\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.UsuarioDEL\r\n\t@Id int\r\nAS\r\nBEGIN\r\n\tSET NOCOUNT ON;\r\n\tDELETE FROM dbo.Usuarios WHERE Id = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetUsuarios()\r\nRETURNS TABLE \r\nAS\r\nRETURN \r\n(\r\n\tSELECT U.Id as id,\r\n\tU.Nombre as nombre,\r\n\tU.Apellido as apellido, \r\n\tU.Correo as correo, \r\n\tU.FechaNacimiento as fechaNacimiento, \r\n\tU.Foto as foto, \r\n\tE.Nombre AS estatus, \r\n\tU.idStatus--, R.Id as IdRol, R.Nombre AS Roles\r\n\tFROM dbo.Usuarios U\r\n\t--JOIN dbo.RolesUsuarios RU\r\n\t--\tON RU.IdUsuario = U.Id\r\n\t--JOIN dbo.Roles R\r\n\t--\tON R.Id = RU.IdRol\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = U.IdStatus\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetUsuario(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT U.id,\r\n\tU.Nombre as nombre,\r\n\tU.Apellido as apellido, \r\n\tU.Correo as correo, \r\n\tU.FechaNacimiento as fechaNacimiento, \r\n\tU.Foto as foto, \r\n\tE.Nombre AS estatus, \r\n\tU.IdStatus as idEstatus--, R.Id as IdRol, R.Nombre AS Roles\r\n\tFROM dbo.Usuarios U\r\n\t--JOIN dbo.RolesUsuarios RU\r\n\t--\tON RU.IdUsuario = U.Id\r\n\t--JOIN dbo.Roles R\r\n\t--\tON R.Id = RU.IdRol\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = U.IdStatus\r\n\tWHERE U.Id = @Id\r\n)");

            migrationBuilder.Sql("CREATE function dbo.fxGetRolesUsuario(@IdUsuario int)\r\nreturns table\r\nas\r\nreturn\r\n(\r\n\tselect R.Id as id, R.Nombre as nombre\r\n\tfrom dbo.RolesUsuarios RU\r\n\tjoin dbo.Roles R\r\n\t\ton R.Id = RU.IdRol\r\n\tjoin dbo.Usuarios U\r\n\t\ton U.Id = RU.IdUsuario\r\n\twhere U.Id = @IdUsuario\r\n)");

            migrationBuilder.Sql("CREATE FUNCTION dbo.fxLoginUsuario(@Correo varchar(120), @Password varchar(200))\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT U.id,\r\n\tU.Nombre as nombre,\r\n\tU.Apellido as apellido, \r\n\tU.Correo as correo, \r\n\tU.FechaNacimiento as fechaNacimiento, \r\n\tU.Foto as foto, \r\n\tE.Nombre AS estatus, \r\n\tU.IdStatus as idEstatus--, R.Id as IdRol, R.Nombre AS Roles\r\n\tFROM dbo.Usuarios U\r\n\t--JOIN dbo.RolesUsuarios RU\r\n\t--\tON RU.IdUsuario = U.Id\r\n\t--JOIN dbo.Roles R\r\n\t--\tON R.Id = RU.IdRol\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = U.IdStatus\r\n\tWHERE Correo = @Correo and [Password] = HASHBYTES('SHA2_512', @Password)\r\n)");
            ////Roles stored procedures and functions
            //migrationBuilder.Sql("CREATE PROCEDURE dbo.RolesUPD\r\n\t@Id int out,\r\n\t@Nombre varchar(50),\r\n\t@Descripcion varchar(50),\r\n\t@IdStatus int\r\nAS\r\nBEGIN\r\n\tSET NOCOUNT ON;\r\n\r\n\tIF EXISTS(SELECT * FROM dbo.Roles WHERE Id = @Id)\r\n\tBEGIN\r\n\t\tUPDATE dbo.Roles\r\n\t\tSET Nombre = @Nombre,\r\n\t\t\tDescripcion = @Descripcion,\r\n\t\t\tIdStatus = @IdStatus\r\n\t\tWHERE Id = @Id\r\n\tEND\r\n\tELSE\r\n\tBEGIN\r\n\t\tINSERT INTO dbo.Roles(Nombre, Descripcion, IdStatus)\r\n\t\tVALUES (@Nombre, @Descripcion, @IdStatus)\r\n\r\n\t\t-- Obtener el ID del rol recién insertado\r\n\t\tSET @Id = SCOPE_IDENTITY()\r\n\tEND\r\nEND");
            //migrationBuilder.Sql("CREATE PROCEDURE dbo.RolesDEL\r\n\t@Id int\r\nAS\r\nBEGIN\r\n\tSET NOCOUNT ON;\r\n\tDELETE FROM dbo.Roles WHERE Id = @Id\r\nEND");
            //migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetRoles()\r\nRETURNS TABLE \r\nAS\r\nRETURN \r\n(\r\n\tSELECT R.Id, R.Nombre, R.Descripcion, E.Nombre AS Estatus, R.IdStatus\r\n\tFROM dbo.Roles R\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = R.IdStatus\r\n)");
            //migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetRol(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT R.Id, R.Nombre, R.Descripcion, E.Nombre AS Estatus, R.IdStatus\r\n\tFROM dbo.Roles R\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = R.IdStatus\r\n\tWHERE R.Id = @Id\r\n)");

            //Unidades Medida stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.UnidadesMedidaUPD\r\n    @IdUnidadMedida int out,\r\n    @Nombre varchar(45),\r\n    @Descripcion varchar(120),\r\n    @IdStatus int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    \r\n    IF EXISTS(SELECT * FROM dbo.UnidadesMedida WHERE IdUnidadMedida = @IdUnidadMedida)\r\n    BEGIN\r\n        UPDATE dbo.UnidadesMedida\r\n        SET Nombre = @Nombre,\r\n            Descripcion = @Descripcion,\r\n            IdStatus = @IdStatus\r\n        WHERE IdUnidadMedida = @IdUnidadMedida\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.UnidadesMedida(IdUnidadMedida, Nombre, Descripcion, IdStatus)\r\n        VALUES (@IdUnidadMedida, @Nombre, @Descripcion, @IdStatus)\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.UnidadesMedidaDEL\r\n    @IdUnidadMedida int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DELETE FROM dbo.UnidadesMedida WHERE IdUnidadMedida = @IdUnidadMedida\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetUnidadesMedida()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdUnidadMedida AS idUnidadMedida, \r\n    Nombre AS nombre,\r\n    Descripcion AS descripcion,\r\n    IdStatus AS idStatus\r\n    FROM dbo.UnidadesMedida\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetUnidadMedida(@IdUnidadMedida int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdUnidadMedida AS idUnidadMedida, \r\n    Nombre AS nombre,\r\n    Descripcion AS descripcion,\r\n    IdStatus AS idStatus\r\n    FROM dbo.UnidadesMedida\r\n    WHERE IdUnidadMedida = @IdUnidadMedida\r\n)");

            //Pedidos stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.PedidosUPD\r\n    @Id int out,\r\n    @IdUsuario int,\r\n    @IdStatus int,\r\n    @Fecha datetime,\r\n    @FechaEntrega datetime,\r\n    @Domicilio varchar(255),\r\n    @Telefono varchar(10),\r\n    @RazonSocial varchar(45),\r\n    @Rfc varchar(13),\r\n    @TipoPago char(4),\r\n    @TipoEnvio char(4),\r\n    @TipoPedido char(4),\r\n    @Total decimal(18,4)\r\n\t--,\r\n\t--@IdsProductos\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\tDECLARE @IdIns INT, @IdProveedor int, @IdCliente int \r\n\tSET @Total = 0\r\n\r\n\tDELETE FROM dbo.DetallePedidos WHERE IdPedido = @Id AND Fecha = @Fecha\r\n\r\n    IF EXISTS(SELECT * FROM dbo.Pedidos WHERE IdPedido = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Pedidos\r\n        SET IdUsuario = @IdUsuario,\r\n            IdStatus = @IdStatus,\r\n            Fecha = @Fecha,\r\n            FechaEntrega = @FechaEntrega,\r\n            Domicilio = @Domicilio,\r\n            Telefono = @Telefono,\r\n            RazonSocial = @RazonSocial,\r\n            Rfc = @Rfc,\r\n            TipoPago = @TipoPago,\r\n            TipoEnvio = @TipoEnvio,\r\n            TipoPedido = @TipoPedido,\r\n            Total = @Total\r\n        WHERE IdPedido = @Id\r\n\r\n\t\tIF @TipoPedido = 'C'\r\n\t\tBEGIN\r\n\t\t\tSELECT @IdProveedor = IdProveedor FROM dbo.Proveedores WHERE IdUsuario = @IdUsuario\r\n\t\t\tSET @IdProveedor = ISNULL(@IdProveedor, @IdUsuario)\r\n\r\n\t\t\tSELECT @IdIns = Id FROM dbo.Compras WHERE IdPedido = @Id\r\n\r\n\t\t\tEXEC dbo.ComprasUPD @IdIns,@Id,@Fecha,@IdProveedor, @Total\r\n\t\tEND\r\n\t\tIF @TipoPedido = 'V'\r\n\t\tBEGIN\r\n\t\t\tSELECT @IdCliente = IdCliente FROM dbo.Clientes WHERE IdUsuario = @IdUsuario\r\n\t\t\tSET @IdCliente = ISNULL(@IdCliente, @IdUsuario)\r\n\r\n\t\t\tSELECT @IdIns = Id FROM dbo.Ventas WHERE IdPedido = @Id\r\n\r\n\t\t\tEXEC dbo.VentasUPD @IdIns, @Id, @Fecha, @IdCliente, @Total\r\n\t\tEND\r\n    END\r\n    ELSE\r\n    BEGIN\r\n\t\tSET @Id = (SELECT ISNULL(MAX(IdPedido),0) FROM Pedidos) + 1\r\n\r\n        INSERT INTO dbo.Pedidos(IdPedido,IdUsuario, IdStatus, Fecha, FechaEntrega, Domicilio, Telefono, RazonSocial, Rfc, TipoPago, TipoEnvio, TipoPedido, Total)\r\n        VALUES (@Id,@IdUsuario, @IdStatus, @Fecha, @FechaEntrega, @Domicilio, @Telefono, @RazonSocial, @Rfc, @TipoPago, @TipoEnvio, @TipoPedido, @Total)\r\n\r\n        --SET @Id = SCOPE_IDENTITY()\t\r\n\t\tSET @IdIns = 0\r\n\r\n\t\tIF @TipoPedido = 'C'\r\n\t\tBEGIN\r\n\t\t\tSELECT @IdProveedor = IdProveedor FROM dbo.Proveedores WHERE IdUsuario = @IdUsuario\r\n\t\t\tSET @IdProveedor = ISNULL(@IdProveedor, @IdUsuario)\r\n\r\n\t\t\tEXEC dbo.ComprasUPD @IdIns,@Id,@Fecha,@IdProveedor, @Total\r\n\t\tEND\r\n\t\tIF @TipoPedido = 'V'\r\n\t\tBEGIN\r\n\t\t\tSELECT @IdCliente = IdCliente FROM dbo.Clientes WHERE IdUsuario = @IdUsuario\r\n\t\t\tSET @IdCliente = ISNULL(@IdCliente, @IdUsuario)\r\n\r\n\t\t\tEXEC dbo.VentasUPD @IdIns, @Id, @Fecha, @IdCliente, @Total\r\n\t\tEND\r\n    END\r\n\t--EXEC dbo.DetallePedidosUPD @Id,@Id\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.PedidosDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\tDELETE FROM dbo.DetallePedidos WHERE IdPedido = @Id\r\n\tDELETE FROM dbo.Pedidos WHERE IdPedido = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetPedidos()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT P.IdPedido AS idPedido, \r\n           P.IdUsuario AS idUsuario,\r\n           U.Nombre AS usuarioNombre,\r\n           E.Nombre AS estatus,\r\n           P.Fecha AS fecha, \r\n\t\t   P.FechaEntrega as fechaEntrega,\r\n           P.Domicilio as domicilio, \r\n\t\t   P.Telefono as telefono,\r\n           P.RazonSocial as razonSocial, \r\n\t\t   P.Rfc as rfc,\r\n           P.TipoPago as tipoPago, \r\n\t\t   P.TipoEnvio as tipoEnvio, \r\n\t\t   P.TipoPedido as tipoPedido,\r\n           P.Total as total\r\n    FROM dbo.Pedidos P\r\n    JOIN dbo.Usuarios U ON U.Id = P.IdUsuario\r\n    JOIN dbo.Estatus E ON E.Id = P.IdStatus\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetPedido(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT P.IdPedido AS idPedido, \r\n           P.IdUsuario AS idUsuario,\r\n\t\t   P.IdStatus as idStatus,\r\n           P.Fecha as fecha, \r\n\t\t   P.FechaEntrega as fechaEntrega,\r\n           P.Domicilio as domicilio, \r\n\t\t   P.Telefono as telefono,\r\n           P.RazonSocial as razonSocial, \r\n\t\t   P.Rfc as rfc,\r\n           TRIM(P.TipoPago) as tipoPago, \r\n\t\t   TRIM(P.TipoEnvio) as tipoEnvio, \r\n\t\t   TRIM(P.TipoPedido) as tipoPedido,\r\n           P.Total as total\r\n    FROM dbo.Pedidos P\r\n    --JOIN dbo.Usuarios U ON U.Id = P.IdUsuario\r\n    --JOIN dbo.Estatus E ON E.Id = P.IdStatus\r\n    WHERE P.IdPedido = @Id\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetPedidosByUsuario(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT P.IdPedido AS idPedido, \r\n           P.IdUsuario AS idUsuario,\r\n           P.IdStatus as idStatus,\r\n           P.Fecha as fecha, \r\n           P.FechaEntrega as fechaEntrega,\r\n           P.Domicilio as domicilio, \r\n           P.Telefono as telefono,\r\n           P.RazonSocial as razonSocial, \r\n           P.Rfc as rfc,\r\n           TRIM(P.TipoPago) as tipoPago, \r\n           TRIM(P.TipoEnvio) as tipoEnvio, \r\n           TRIM(P.TipoPedido) as tipoPedido,\r\n           P.Total as total,\r\n\t\t   E.Nombre as estatus\r\n    FROM dbo.Pedidos P\r\n    --JOIN dbo.Usuarios U ON U.Id = P.IdUsuario\r\n    JOIN dbo.Estatus E ON E.Id = P.IdStatus\r\n    WHERE P.IdUsuario = @Id\r\n\tAND P.TipoPedido = 'C'\r\n)");

            //DetallePedidos stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.DetallePedidosUPD\r\n    @Id int out,\r\n    @Fecha datetime,\r\n    @IdPedido int,\r\n    @IdProducto int,\r\n    @Cantidad int,\r\n    @PrecioUnitario decimal(18,4),\r\n    @Subtotal decimal(18,4)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    IF EXISTS(SELECT * FROM dbo.DetallePedidos WHERE IdDetallePedido = @Id)\r\n    BEGIN\r\n        UPDATE dbo.DetallePedidos\r\n        SET IdProducto = @IdProducto,\r\n            Cantidad = @Cantidad,\r\n            PrecioUnitario = @PrecioUnitario,\r\n            Subtotal = @Subtotal\r\n        WHERE IdPedido = @IdPedido\r\n\t\tAND Fecha = @Fecha\t\t\r\n\t\tAND IdDetallePedido = @Id\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.DetallePedidos(Fecha, IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal)\r\n        VALUES (@Fecha, @IdPedido, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)\r\n\r\n        SET @Id = SCOPE_IDENTITY()\r\n    END\r\n\r\n\tUPDATE dbo.Pedidos \r\n\tSET Total = Total + @Subtotal\r\n\tWHERE IdPedido = @IdPedido\r\n\tAND Fecha = @Fecha\r\n\r\n\tUPDATE dbo.Ventas\r\n\tSET Total = Total + @Subtotal\r\n\tWHERE IdPedido = @IdPedido\r\n\tAND Fecha = @Fecha\r\n\r\n\tUPDATE dbo.Compras\r\n\tSET Total = Total + @Subtotal\r\n\tWHERE IdPedido = @IdPedido\r\n\tAND Fecha = @Fecha\r\n\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.DetallePedidoDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DELETE FROM dbo.DetallePedidos WHERE IdDetallePedido = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetDetallesPedido(@IdPedido int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdDetallePedido AS idDetallePedido, \r\n           Fecha, \r\n           IdPedido AS idPedido, \r\n           IdProducto AS idProducto, \r\n           Cantidad, \r\n           PrecioUnitario AS precioUnitario, \r\n           Subtotal\r\n    FROM dbo.DetallePedidos\r\n\tWHERE IdPedido = @IdPedido\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetDetallePedido(@IdPedido int, @Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdDetallePedido AS idDetallePedido, \r\n           Fecha, \r\n           IdPedido AS idPedido, \r\n           IdProducto AS idProducto, \r\n           Cantidad, \r\n           PrecioUnitario AS precioUnitario, \r\n           Subtotal\r\n    FROM dbo.DetallePedidos\r\n    WHERE IdPedido = @IdPedido\r\n\tAND IdDetallePedido = @Id\r\n)");

            //Ventas stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.VentasUPD\r\n\t@Id int out,\r\n\t@IdPedido int,\r\n\t@Fecha DATETIME,\r\n\t@IdCliente int,\r\n\t@Total decimal(18,4)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON\r\n\r\n\tIF EXISTS(SELECT * FROM dbo.Ventas WHERE Id = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Ventas\r\n        SET IdPedido = @IdPedido,\r\n            Fecha = @Fecha,\r\n\t\t\tIdCliente = @IdCliente,\r\n            Total = @Total\r\n        WHERE Id = @Id\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.Ventas(IdPedido,IdCliente, Fecha, Total)\r\n        VALUES (@IdPedido, @IdCliente, @Fecha, @Total)\r\n\r\n        SET @Id = SCOPE_IDENTITY()\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.VentasDEL\r\n\t@Id int\r\nAS\r\nBEGIN\r\n\tSET NOCOUNT ON;\r\n\tDELETE FROM dbo.Ventas WHERE Id = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetVentas()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT \r\n\tV.Id as id,\r\n\tFecha as fecha,\r\n\tCONCAT(U.Nombre,' ',U.Apellido) as usuarioNombre,\r\n\tIdPedido as idPedido,\r\n\tTotal as total\r\n\tFROM dbo.Ventas\tV\r\n\tLEFT JOIN dbo.Clientes C\r\n\t\tON C.IdCliente = V.IdCliente\r\n\tJOIN dbo.Usuarios U\r\n\t\tON U.Id = C.IdUsuario\r\n)\r\n");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetVenta(\r\n\t@Id int\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT \r\n\tId as id,\r\n\tFecha as fecha,\r\n\tIdCliente as idCliente,\r\n\tIdPedido as idPedido,\r\n\tTotal as total\r\n\tFROM dbo.Ventas\r\n\tWHERE Id = @Id\r\n)");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.MermasUPD\r\n\t@IdPedido INT\r\nAS\r\nBEGIN\r\n\tIF EXISTS(SELECT * FROM dbo.Pedidos WHERE IdPedido = @IdPedido)\r\n\tBEGIN TRANSACTION;\r\n\r\n\tDECLARE @idProducto INT, @cantidadVendida INT;\r\n\r\n\t-- Obtener los detalles del pedido\r\n\tDECLARE detalles_cursor CURSOR FOR\r\n\tSELECT idProducto, cantidad FROM dbo.DetallePedidos WHERE idPedido = @idPedido;\r\n\r\n\tOPEN detalles_cursor;\r\n\r\n\tFETCH NEXT FROM detalles_cursor INTO @idProducto, @cantidadVendida;\r\n\r\n\tWHILE @@FETCH_STATUS = 0\r\n\tBEGIN\r\n\t\t-- Reducir las existencias de cada producto\r\n\t\tUPDATE dbo.Productos SET merma = merma + @cantidadVendida WHERE idProducto = @idProducto;\r\n\r\n\t\tFETCH NEXT FROM detalles_cursor INTO @idProducto, @cantidadVendida;\r\n\tEND\r\n\r\n\tCLOSE detalles_cursor;\r\n\tDEALLOCATE detalles_cursor;\r\n\r\n\tCOMMIT TRANSACTION;\r\nEND");

            //Compras stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ComprasUPD\r\n    @Id int OUTPUT,\r\n    @IdPedido int,\r\n    @Fecha DATETIME,\r\n    @IdProveedor int,\r\n    @Total decimal(18, 4)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    IF EXISTS(SELECT * FROM dbo.Compras WHERE Id = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Compras\r\n        SET IdPedido = @IdPedido,\r\n            Fecha = @Fecha,\r\n            IdProveedor = @IdProveedor,\r\n            Total = @Total\r\n        WHERE Id = @Id;\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.Compras(IdPedido, IdProveedor, Fecha, Total)\r\n        VALUES (@IdPedido, @IdProveedor, @Fecha, @Total);\r\n\r\n        SET @Id = SCOPE_IDENTITY();\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ComprasDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\tDELETE FROM dbo.Compras WHERE Id = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetCompras()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\tC.Id as id,\r\n\tFecha as fecha,\r\n\tCONCAT(U.Nombre,' ',U.Apellido) as usuarioNombre,\r\n\tIdPedido as idPedido,\r\n\tTotal as total\r\n\tFROM dbo.Compras C\r\n\tLEFT JOIN dbo.Proveedores P\r\n\t\tON P.IdProveedor = C.IdProveedor\r\n\tJOIN dbo.Usuarios U\r\n\t\tON U.Id = P.IdUsuario\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetCompra(\r\n    @Id int\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n        Id AS id,\r\n        Fecha AS fecha,\r\n        IdProveedor AS idProveedor,\r\n        IdPedido AS idPedido,\r\n        Total AS total\r\n    FROM dbo.Compras\r\n    WHERE Id = @Id\r\n)");

            //Productos stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ProductosUPD\r\n    @IdProducto int out,\r\n    @Codigo varchar(10),\r\n    @Nombre varchar(45),\r\n    @Descripcion varchar(120),\r\n    @Precio decimal(18,4),\r\n    @Foto text,\r\n    @Merma decimal(18,4),\r\n    @IdStatus int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    IF EXISTS(SELECT * FROM dbo.Productos WHERE IdProducto = @IdProducto)\r\n    BEGIN\r\n        UPDATE dbo.Productos\r\n        SET Codigo = @Codigo,\r\n            Nombre = @Nombre,\r\n            Descripcion = @Descripcion,\r\n            Precio = @Precio,\r\n            Foto = @Foto,\r\n            Merma = @Merma,\r\n            IdStatus = @IdStatus,\r\n            FechaModificacion = GETDATE()\r\n        WHERE IdProducto = @IdProducto\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.Productos(Codigo, Nombre, Descripcion, Precio, Foto, Merma, IdStatus, FechaRegistro, FechaModificacion)\r\n        VALUES (@Codigo, @Nombre, @Descripcion, @Precio, @Foto, @Merma, @IdStatus, GETDATE(), GETDATE())\r\n\r\n        SET @IdProducto = SCOPE_IDENTITY()\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ProductosDEL\r\n    @IdProducto int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DELETE FROM dbo.Productos WHERE IdProducto = @IdProducto\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetProductos()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdProducto AS idProducto, \r\n           Codigo AS codigo, \r\n           P.Nombre AS nombre, \r\n           Descripcion AS descripcion, \r\n           Precio AS precio, \r\n           Foto AS foto, \r\n           Merma AS merma, \r\n           E.Nombre as estatus\r\n    FROM dbo.Productos P\r\n\tJOIN dbo.Estatus E ON E.Id = P.IdStatus\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetProducto(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT IdProducto AS idProducto, \r\n           Codigo AS codigo, \r\n           Nombre AS nombre, \r\n           Descripcion AS descripcion, \r\n           Precio AS precio, \r\n           Foto AS foto, \r\n           Merma AS merma, \r\n           IdStatus AS idStatus, \r\n           FechaRegistro AS fechaRegistro, \r\n           FechaModificacion AS fechaModificacion\r\n    FROM dbo.Productos\r\n    WHERE IdProducto = @Id\r\n)");

            //Materia prima stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.MateriasPrimasUPD\r\n    @Id int out,\r\n    @Codigo varchar(10),\r\n    @Nombre varchar(45),\r\n    @Descripcion varchar(120),\r\n    @Perecedero int,\r\n    @Stock int,\r\n    @CantMinima int,\r\n    @CantMaxima int,\r\n    @IdUnidadMedida int,\r\n    @Precio decimal(18,4),\r\n    @Foto nvarchar(max),\r\n    @IdProveedor int,\r\n    @IdStatus int--,\r\n    --@FechaRegistro datetime,\r\n    --@FechaModificacion datetime--,\r\n    -- @Ingredientes nvarchar(max)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\r\n    IF EXISTS(SELECT * FROM dbo.MateriasPrimas WHERE Id = @Id)\r\n    BEGIN\r\n        UPDATE dbo.MateriasPrimas\r\n        SET Codigo = @Codigo,\r\n            Nombre = @Nombre,\r\n            Descripcion = @Descripcion,\r\n            Perecedero = @Perecedero,\r\n            Stock = @Stock,\r\n            CantMinima = @CantMinima,\r\n            CantMaxima = @CantMaxima,\r\n            IdUnidadMedida = @IdUnidadMedida,\r\n            Precio = @Precio,\r\n            Foto = @Foto,\r\n            IdProveedor = @IdProveedor,\r\n            IdStatus = @IdStatus,\r\n            FechaModificacion = GETDATE()\r\n        WHERE Id = @Id\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.MateriasPrimas(Codigo, Nombre, Descripcion, Perecedero, Stock,\r\n                                     CantMinima, CantMaxima, IdUnidadMedida, Precio,\r\n                                     Foto, IdProveedor, IdStatus, FechaRegistro, FechaModificacion)\r\n        VALUES (@Codigo, @Nombre, @Descripcion, @Perecedero, @Stock,\r\n                @CantMinima, @CantMaxima, @IdUnidadMedida, @Precio,\r\n                @Foto, @IdProveedor, @IdStatus, GETDATE(), GETDATE())\r\n\r\n\t\tSET @Id = SCOPE_IDENTITY()\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.MateriasPrimasDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DELETE FROM dbo.MateriasPrimas WHERE Id = @Id\r\n    DELETE FROM dbo.Ingrediente WHERE IdMateriaPrima = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetMateriasPrimas()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\t\tMP.Id as id, \r\n\t\tMP.Codigo as codigo, \r\n\t\tMP.Nombre as nombre, \r\n\t\tP.RazonSocial as razonSocial,\r\n\t\tS.Nombre as estatus,\r\n\t\tMP.Descripcion as descripcion,\r\n\t\tMP.Stock as stock,\r\n\t\tMP.Precio as precio,\r\n\t\tMP.Foto as foto,\r\n\t\t0 as cantidad\r\n    FROM dbo.MateriasPrimas as MP\r\n    JOIN dbo.Proveedores as P\r\n\t\tON MP.IdProveedor = P.IdProveedor\r\n    JOIN dbo.Estatus as S\r\n\t\tON MP.IdStatus = S.Id\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetMateriaPrima(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\t\tId as id, \r\n\t\tCodigo as codigo, \r\n\t\tNombre as nombre, \r\n\t\tDescripcion as descripcion, \r\n\t\tPerecedero as perecedero, \r\n\t\tStock as stock, \r\n\t\tCantMinima as cantMinima, \r\n\t\tCantMaxima as cantMaxima,\r\n\t\tIdUnidadMedida as idUnidadMedida, \r\n\t\tPrecio as precio, \r\n\t\tFoto as foto, \r\n\t\tIdProveedor as idProveedor, \r\n\t\tIdStatus as idStatus, \r\n\t\tFechaRegistro as fechaRegistro, \r\n\t\tFechaModificacion as fechaModificacion\r\n\tFROM dbo.MateriasPrimas\r\n\tWHERE Id = @Id\r\n\r\n)");

            //Proveedores stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ProveedoresUPD\r\n    @Id int out,\r\n    @IdUsuario int,\r\n    @Nombre varchar(50),\r\n    @Apellido varchar(50),\r\n    @Correo varchar(50),\r\n    @FechaNacimiento datetime,\r\n    @Foto TEXT,\r\n    @IdStatus int,\r\n    @Rfc varchar(50),\r\n    @Direccion varchar(255),\r\n    @Telefono varchar(13),\r\n    @RazonSocial varchar(120),\r\n    @Password varbinary(max)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\tDECLARE @IdRoles int = 3\r\n    IF EXISTS(SELECT * FROM dbo.Proveedores WHERE IdProveedor = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Proveedores\r\n        SET Rfc = @Rfc,\r\n            Direccion = @Direccion,\r\n            Telefono = @Telefono,\r\n            RazonSocial = @RazonSocial\r\n        WHERE IdProveedor = @Id\r\n\r\n        EXEC dbo.UsuariosUPD @IdUsuario, @Nombre, @Apellido, @Correo, @FechaNacimiento, @Foto, @IdStatus, @Password, @IdRoles\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        EXEC dbo.UsuariosUPD @IdUsuario out, @Nombre, @Apellido, @Correo, @FechaNacimiento, @Foto, @IdStatus, @Password, @IdRoles\r\n\r\n\t\tDECLARE @IdUser int = (select MAX(Id) from dbo.Usuarios)\r\n\r\n        INSERT INTO dbo.Proveedores(Rfc,Direccion,Telefono,IdUsuario,RazonSocial)\r\n        VALUES(@Rfc,@Direccion,@Telefono,@IdUser,@RazonSocial)\r\n\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ProveedorDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DECLARE @IdUsuario int\r\n    SELECT @IdUsuario = IdUsuario FROM dbo.Proveedores WHERE IdProveedor = @Id\r\n    UPDATE dbo.Usuarios SET IdStatus = 2 WHERE Id = @IdUsuario\r\n\r\n    DELETE FROM dbo.Proveedores WHERE IdProveedor = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetProveedores()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\t\tP.IdProveedor AS idProveedor,\r\n\t\tU.Nombre AS nombre, \r\n\t\tU.Apellido AS apellido, \r\n\t\tU.Correo AS correo, \r\n\t\tU.FechaNacimiento AS fechaNacimiento, \r\n\t\tU.Foto AS foto, \r\n\t\tU.IdStatus AS idStatus, \r\n\t\tP.Rfc AS rfc, \r\n\t\tP.Direccion AS direccion, \r\n\t\tP.Telefono AS telefono, \r\n\t\tP.RazonSocial AS razonSocial, \r\n\t\tU.Id AS idUsuario,\r\n\t\tU.Password as password\r\n\tFROM dbo.Proveedores P\r\n\tINNER JOIN dbo.Usuarios U ON P.IdUsuario = U.Id\r\n   \r\n)");
            //migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetProveedores()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT P.IdProveedor AS idProveedor, \r\n           P.IdUsuario AS idUsuario,\r\n           U.Nombre AS usuarioNombre,\r\n           U.Apellido AS usuarioApellido,\r\n           U.Correo AS usuarioCorreo,\r\n           U.FechaNacimiento AS usuarioFechaNacimiento,\r\n           U.Foto AS usuarioFoto,\r\n           U.IdStatus AS usuarioIdStatus,\r\n           U.Password AS usuarioPassword,\r\n           P.Rfc AS rfc,\r\n           P.Direccion AS direccion,\r\n           P.Telefono AS telefono,\r\n           P.RazonSocial AS razonSocial\r\n    FROM dbo.Proveedores P\r\n    JOIN dbo.Usuarios U ON U.Id = P.IdUsuario\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetProveedor(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\t\tP.IdProveedor AS idProveedor, \r\n\t\tU.Nombre AS nombre, \r\n\t\tU.Apellido AS apellido, \r\n\t\tU.Correo AS correo, \r\n\t\tU.FechaNacimiento AS fechaNacimiento, \r\n\t\tU.Foto AS foto, \r\n\t\tU.IdStatus AS idStatus, \r\n\t\tP.Rfc AS rfc, \r\n\t\tP.Direccion AS direccion, \r\n\t\tP.Telefono AS telefono, \r\n\t\tP.RazonSocial AS razonSocial, \r\n\t\tU.Id AS idUsuario, \r\n\t\tU.Password as password\r\n\tFROM dbo.Proveedores P\r\n\tINNER JOIN dbo.Usuarios U ON P.IdUsuario = U.Id\r\n\tWHERE P.IdProveedor = @Id\r\n\r\n)");

            //Clientes stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ClientesUPD\r\n    @Id int out,\r\n    @IdUsuario int,\r\n    @Nombre varchar(50),\r\n    @Apellido varchar(50),\r\n    @Correo varchar(50),\r\n    @FechaNacimiento datetime,\r\n    @Foto TEXT,\r\n    @Password varbinary(MAX),\r\n    @IdStatus int,\r\n    @Rfc varchar(13),\r\n    @Direccion varchar(255),\r\n    @Telefono varchar(10)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n\tDECLARE @IdRoles int = 2\r\n    IF EXISTS(SELECT * FROM dbo.Clientes WHERE IdCliente = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Clientes\r\n        SET Rfc = @Rfc,\r\n            Direccion = @Direccion,\r\n            Telefono = @Telefono\r\n        WHERE IdCliente = @Id\r\n\r\n        EXEC dbo.UsuariosUPD @IdUsuario, @Nombre, @Apellido, @Correo, @FechaNacimiento, @Foto, @IdStatus, @Password, @IdRoles\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        EXEC dbo.UsuariosUPD @IdUsuario out, @Nombre, @Apellido, @Correo, @FechaNacimiento, @Foto, @IdStatus, @Password, @IdRoles\r\n\r\n\t\tDECLARE @IdUser int = (select MAX(Id) from dbo.Usuarios)\r\n\r\n        INSERT INTO dbo.Clientes(Rfc,Direccion,Telefono,IdUsuario)\r\n        VALUES(@Rfc,@Direccion,@Telefono,@IdUser)\r\n    END\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.ClientesDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    DECLARE @IdUsuario int\r\n    SELECT @IdUsuario = IdUsuario FROM dbo.Clientes WHERE IdCliente = @Id\r\n    UPDATE dbo.Usuarios SET IdStatus = 2 WHERE Id = @IdUsuario\r\n\r\n    DELETE FROM dbo.Clientes WHERE IdCliente = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetClientes()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT\r\n        C.IdCliente as idCliente,\r\n        U.Nombre as nombre,\r\n        U.Apellido as apellido,\r\n        U.Correo as correo,\r\n        U.FechaNacimiento as fechaNacimiento,\r\n        U.Foto as foto,\r\n        U.IdStatus as idStatus,\r\n        C.Rfc as rfc,\r\n        C.Direccion as direccion,\r\n        C.Telefono as telefono\r\n    FROM\r\n        dbo.Clientes C\r\n        INNER JOIN dbo.Usuarios U ON C.IdUsuario = U.Id    \r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetCliente(@Id int)\r\n RETURNS TABLE \r\n AS \r\n RETURN \r\n ( \r\n SELECT C.IdCliente as idCliente, \r\n U.Nombre as nombre, \r\n U.Apellido as apellido, \r\n U.Correo as correo, \r\n U.FechaNacimiento as fechaNacimiento, \r\n U.Foto as foto, \r\n U.IdStatus as idStatus, \r\n C.Rfc as rfc, \r\n C.Direccion as direccion, \r\n C.Telefono as telefono, \r\n U.Id as idUsuario, \r\n U.Password as password\r\n FROM dbo.Clientes C\r\n INNER JOIN dbo.Usuarios U ON C.IdUsuario = U.Id\r\n WHERE C.IdCliente = @Id\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetClienteByUsuario(@IdUsuario int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT\r\n    C.IdCliente as idCliente,\r\n    U.Nombre as nombre,\r\n    U.Apellido as apellido,\r\n    U.Correo as correo,\r\n    U.FechaNacimiento as fechaNacimiento,\r\n    U.Foto as foto,\r\n    U.IdStatus as idStatus,\r\n    C.Rfc as rfc,\r\n    C.Direccion as direccion,\r\n    C.Telefono as telefono\r\nFROM\r\n    dbo.Clientes C with(readuncommitted)\r\n    INNER JOIN dbo.Usuarios U ON C.IdUsuario = U.Id\r\nWHERE\r\n    C.IdUsuario = @IdUsuario\r\n)");

            //Ingredientes stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.IngredientesUPD\r\n@IdReceta int,\r\n@IdMateriaPrima INT,\r\n@Cantidad decimal(18,4)\r\nAS\r\nBEGIN\r\n\t--DELETE FROM dbo.Ingrediente WHERE IdReceta = @IdReceta\r\n\r\n\tIF EXISTS(SELECT * FROM dbo.Ingrediente WHERE IdReceta = @IdReceta \r\n\tAND IdMateriaPrima = @IdMateriaPrima)\r\n\t\tUPDATE dbo.Ingrediente\r\n\t\tSET Cantidad = @Cantidad\r\n\t\tWHERE IdReceta = @IdReceta\r\n\t\tAND IdMateriaPrima = @IdMateriaPrima\r\n\tELSE\r\n\t\tINSERT INTO dbo.Ingrediente(IdReceta, IdMateriaPrima, Cantidad)\r\n\t\tVALUES(@IdReceta,@IdMateriaPrima,@Cantidad)\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.IngredientesDEL\r\n@IdReceta int,\r\n@IdMateriaPrima int\r\nAS\r\nBEGIN\r\n\tDELETE FROM dbo.Ingrediente \r\n\tWHERE IdReceta = @IdReceta AND IdMateriaPrima = @IdMateriaPrima\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetIngredientesReceta\r\n(\r\n    @IdReceta int\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\t\tMP.Codigo as codigo, \r\n\t\tMP.Nombre as nombre, \r\n\t\tI.Cantidad as cantidad\r\n    FROM dbo.MateriasPrimas as MP\r\n    JOIN dbo.Ingrediente I\r\n\tON I.IdMateriaPrima = MP.Id\r\n    WHERE I.IdReceta = @IdReceta\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetIngrediente\r\n(\r\n    @IdReceta int,\r\n    @IdMateriaPrima int\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\tI.IdReceta AS idReceta, \r\n\tI.IdMateriaPrima AS idMateriaPrima, \r\n\tI.Cantidad AS cantidad \r\n    FROM dbo.Ingrediente I    \r\n    WHERE I.IdReceta = @IdReceta \r\n\tAND I.IdMateriaPrima = @IdMateriaPrima\r\n)");

            //Recetas stored procedures and functions
            migrationBuilder.Sql("CREATE PROCEDURE dbo.RecetasUPD\r\n    @Id int out,\r\n    @Codigo varchar(25),\r\n\t@Costo decimal(18,4),\r\n\t@Cantidad decimal(18,4),\r\n    @IdProducto int,\r\n    @IdStatus int--,\r\n    --@FechaRegistro datetime,\r\n    -- @FechaModificacion datetime = GETDATE(),\r\n    --@IdMateriasPrimas nvarchar(max)\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\t\r\n\r\n    IF EXISTS(SELECT * FROM dbo.Recetas WHERE IdReceta = @Id)\r\n    BEGIN\r\n        UPDATE dbo.Recetas\r\n        SET Codigo = @Codigo,\r\n            IdProducto = @IdProducto,\r\n\t\t\tCosto = @Costo,\r\n\t\t\tCantidad = @Cantidad,\r\n            IdStatus = @IdStatus,\r\n            FechaRegistro = GETDATE(),\r\n            FechaModificacion = GETDATE()\r\n        WHERE IdReceta = @Id\r\n    END\r\n    ELSE\r\n    BEGIN\r\n        INSERT INTO dbo.Recetas(Codigo, IdProducto, Cantidad, Costo, IdStatus, FechaRegistro, FechaModificacion)\r\n        VALUES (@Codigo, @IdProducto, @Cantidad, @Costo, @IdStatus, GETDATE(), GETDATE())\r\n\r\n\t\t-- Obtener el ID del usuario recién insertado\r\n\t\tSET @Id = SCOPE_IDENTITY()\r\n    END\r\n    -- Actualizar ingredientes    \r\n\t--Se hara en otro stored para configurar las recetas\r\n\t\r\nEND");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.RecetasDEL\r\n    @Id int\r\nAS\r\nBEGIN\r\n    SET NOCOUNT ON;\r\n    --DELETE FROM dbo.Recetas WHERE IdReceta = @Id\r\n\tUPDATE dbo.Recetas\r\n\tSET IdStatus = 2\r\n\tWHERE IdReceta = @Id\r\n    DELETE FROM dbo.Ingrediente WHERE IdReceta = @Id\r\nEND");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetRecetas()\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT R.IdReceta idReceta, \r\n\tR.Codigo codigo, \r\n\tP.Nombre producto,\r\n\tE.Nombre estatus,\r\n\tR.Costo costo,\r\n\tR.Cantidad cantidad\r\n\t--IdProducto, \r\n\t--IdStatus, \t\r\n    FROM dbo.Recetas R\r\n\tJOIN dbo.Productos P\r\n\t\tON P.IdProducto = R.IdProducto\r\n\tJOIN dbo.Estatus E\r\n\t\tON E.Id = R.IdStatus\r\n)");
            migrationBuilder.Sql("CREATE FUNCTION dbo.fxGetReceta(@Id int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    SELECT \r\n\tIdReceta as idReceta, \r\n\tCodigo as codigo,\r\n\tCosto as costo,\r\n\tCantidad as cantidad,\r\n\tIdProducto as idProducto, \r\n\tIdStatus as idStatus\r\n\tFROM dbo.Recetas\r\n\tWHERE IdReceta = @Id\r\n\r\n)");

            //Carrito stored procedures and functions
            migrationBuilder.Sql("CREATE FUNCTION [dbo].[fxGetCarritoProductos](@IdCliente int)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n\tSELECT \r\n\tT1.IdCarrito as idCarrito,\r\n\tT1.Cantidad as cantidad,\r\n\tT1.IdCliente as idCliente,\r\n\tT1.IdProducto as idProducto,\r\n\tT1.FechaModificacion as fechaModificacion,\r\n\tT1.FechaRegistro as fechaRegistro,\r\n\tT2.Foto as foto , \r\n\tT2.Codigo as codigo, \r\n\tT2.Nombre as nombre, \r\n\tT2.Descripcion as descripcion, \r\n\tT2.Precio as precio\r\n\tFROM [Clamaroj].[dbo].[Carritos] T1\r\n\tINNER JOIN [Clamaroj].[dbo].[Productos] T2 ON T1.IdProducto = T2.IdProducto  \r\n\tWHERE T1.IdCliente = @IdCliente\r\n)");
            migrationBuilder.Sql("CREATE PROCEDURE dbo.CarritoUPD\r\n    @IdCarrito int out,\r\n    @IdCliente int,\r\n    @IdProducto int,\r\n    @Cantidad int\r\nAS\r\nBEGIN\r\n\tDELETE FROM dbo.Carritos where IdCliente = @IdCliente and IdProducto = @IdProducto\r\n\t--IF EXISTS (SELECT IdCarrito FROM Carritos WHERE IdCarrito = @IdCarrito and IdCliente = @IdCliente and IdProducto = @IdProducto)\r\n\t--BEGIN\r\n\t--\tUPDATE dbo.Carritos\r\n\t--\tSET Cantidad = @Cantidad \r\n\t--\t, FechaModificacion = GETDATE()\r\n\t--\tWHERE IdCarrito = @IdCarrito\r\n\t--END\r\n\t--ELSE BEGIN\r\n\t\tINSERT INTO dbo.Carritos(IdCliente, IdProducto, Cantidad, FechaRegistro, FechaModificacion)\r\n\t\tVALUES(@IdCliente, @IdProducto, @Cantidad, GETDATE(), GETDATE())\r\n\t--END\r\nEND");
            //ETL stored procedures and functions (Dashboard)
            //migrationBuilder.Sql("create FUNCTION GetTopClientes\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select Cliente, pedidos from topClientes where mes=@mes and anio=@anio\r\n);");
            ////migrationBuilder.Sql("create FUNCTION GetTopProductos\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select Producto, pedidos from topProductos where mes=@mes and anio=@anio\r\n);");
            ////migrationBuilder.Sql("create FUNCTION GetTopVendedores\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select Vendedor, pedidos from topVendedores where mes=@mes and anio=@anio\r\n);");
            //migrationBuilder.Sql("create FUNCTION FiltrarVentasPorMesYFecha\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select producto, cantProductos from productosVendidos where mes=@mes and anio=@anio\r\n);");
            //migrationBuilder.Sql("create FUNCTION GetSumVentas\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select suma from sumVentas where mes=@mes and anio=@anio\r\n);");
            ////migrationBuilder.Sql("create FUNCTION GetSumGanancias\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select suma from sumGanancias where mes=@mes and anio=@anio\r\n);");
            //migrationBuilder.Sql("create FUNCTION GetGanancias\r\n(\r\n    @mes int,\r\n    @anio varchar(50)\r\n)\r\nRETURNS TABLE\r\nAS\r\nRETURN\r\n(\r\n    select ganancia from ganancias where mes=@mes and anio=@anio\r\n);");

        }

        protected void EliminarStoredProcedures(MigrationBuilder migrationBuilder)
        {
            // Delete all stored procedures and functions created in function AgregarStoredProcedures
            migrationBuilder.Sql("DROP FUNCTION dbo.fxConvertIDsToTable");
            migrationBuilder.Sql("DROP PROCEDURE dbo.EstatusUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.EstatusDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetEstatuses");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetEstatus");
            migrationBuilder.Sql("DROP PROCEDURE dbo.UsuariosUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.UsuarioDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetUsuarios");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetUsuario");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxLoginUsuario");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetRolesUsuario");
            //migrationBuilder.Sql("DROP PROCEDURE dbo.RolesUPD");
            //migrationBuilder.Sql("DROP PROCEDURE dbo.RolesDEL");
            //migrationBuilder.Sql("DROP FUNCTION dbo.fxGetRoles");
            //migrationBuilder.Sql("DROP FUNCTION dbo.fxGetRol");
            migrationBuilder.Sql("DROP PROCEDURE dbo.UnidadesMedidaUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.UnidadesMedidaDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetUnidadesMedida");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetUnidadMedida");
            migrationBuilder.Sql("DROP PROCEDURE dbo.PedidosUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.PedidosDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetPedidos");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetPedido");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetPedidosByUsuario");
            migrationBuilder.Sql("DROP PROCEDURE dbo.DetallePedidosUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.DetallePedidoDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetDetallesPedido");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetDetallePedido");
            migrationBuilder.Sql("DROP PROCEDURE dbo.VentasUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.VentasDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetVentas");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetVenta");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ComprasUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ComprasDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetCompras");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetCompra");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ProductosUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ProductosDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetProductos");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetProducto");
            migrationBuilder.Sql("DROP PROCEDURE dbo.MateriasPrimasUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.MateriasPrimasDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetMateriasPrimas");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetMateriaPrima");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ProveedoresUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ProveedorDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetProveedores");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetProveedor");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ClientesUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ClientesDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetClientes");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetCliente");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetClienteByUsuario");
            migrationBuilder.Sql("DROP PROCEDURE dbo.IngredientesUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.IngredientesDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetIngredientesReceta");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetIngrediente");
            migrationBuilder.Sql("DROP PROCEDURE dbo.RecetasUPD");
            migrationBuilder.Sql("DROP PROCEDURE dbo.RecetasDEL");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetRecetas");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetReceta");
            migrationBuilder.Sql("DROP FUNCTION dbo.fxGetCarritoProductos");
            migrationBuilder.Sql("DROP PROCEDURE dbo.CarritoUPD");
            //migrationBuilder.Sql("DROP FUNCTION dbo.GetTopClientes");
            ////migrationBuilder.Sql("DROP FUNCTION dbo.GetTopProductos");
            ////migrationBuilder.Sql("DROP FUNCTION dbo.GetTopVendedores");
            //migrationBuilder.Sql("DROP FUNCTION dbo.FiltrarVentasPorMesYFecha");
            //migrationBuilder.Sql("DROP FUNCTION dbo.GetSumVentas");
            ////migrationBuilder.Sql("DROP FUNCTION dbo.GetSumGanancias");
            //migrationBuilder.Sql("DROP FUNCTION dbo.GetGanancias");

        }


    }
}
