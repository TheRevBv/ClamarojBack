﻿// <auto-generated />
using System;
using ClamarojBack.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClamarojBack.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ClamarojBack.Models.Cliente", b =>
                {
                    b.Property<int>("IdCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCliente"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("IdCliente");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("ClamarojBack.Models.DetallePedido", b =>
                {
                    b.Property<int>("IdDetallePedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDetallePedido"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdMateriaPrima")
                        .HasColumnType("int");

                    b.Property<int>("IdPedido")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasColumnType("int");

                    b.Property<int>("IdUnidadMedida")
                        .HasColumnType("int");

                    b.Property<int>("MateriaPrimaId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoIdPedido")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductoIdProducto")
                        .HasColumnType("int");

                    b.Property<decimal>("Subtotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdDetallePedido");

                    b.HasIndex("MateriaPrimaId");

                    b.HasIndex("PedidoIdPedido");

                    b.HasIndex("ProductoIdProducto");

                    b.ToTable("DetallePedidos");
                });

            modelBuilder.Entity("ClamarojBack.Models.Estatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Estatus");
                });

            modelBuilder.Entity("ClamarojBack.Models.MateriaPrima", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantMaxima")
                        .HasColumnType("int");

                    b.Property<int>("CantMinima")
                        .HasColumnType("int");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdProveedor")
                        .HasColumnType("int");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<int>("IdUnidadMedida")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("Perecedero")
                        .HasColumnType("int");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProveedorIdProveedor")
                        .HasColumnType("int");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.Property<int>("UnidadMedidaIdUnidadMedida")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("ProveedorIdProveedor");

                    b.HasIndex("UnidadMedidaIdUnidadMedida");

                    b.ToTable("MateriasPrimas");
                });

            modelBuilder.Entity("ClamarojBack.Models.Pedido", b =>
                {
                    b.Property<int>("IdPedido")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPedido"));

                    b.Property<string>("Domicilio")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("EstatusId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("RazonSocial")
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Rfc")
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("TipoEnvio")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("TipoPago")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<string>("TipoPedido")
                        .IsRequired()
                        .HasMaxLength(3)
                        .HasColumnType("nvarchar(3)");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdPedido");

                    b.HasIndex("EstatusId");

                    b.HasIndex("IdUsuario");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("ClamarojBack.Models.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<decimal>("Merma")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("IdProducto");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("ClamarojBack.Models.Proveedor", b =>
                {
                    b.Property<int>("IdProveedor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProveedor"));

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<string>("RazonSocial")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Rfc")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.HasKey("IdProveedor");

                    b.HasIndex("IdUsuario")
                        .IsUnique();

                    b.HasIndex("RazonSocial")
                        .IsUnique();

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("ClamarojBack.Models.Receta", b =>
                {
                    b.Property<int>("IdReceta")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdReceta"));

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("FechaModificacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdMateriaPrima")
                        .HasColumnType("int");

                    b.Property<int>("IdProducto")
                        .HasMaxLength(45)
                        .HasColumnType("int");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<int>("MateriaPrimaId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoIdProducto")
                        .HasColumnType("int");

                    b.HasKey("IdReceta");

                    b.HasIndex("Codigo")
                        .IsUnique();

                    b.HasIndex("MateriaPrimaId");

                    b.HasIndex("ProductoIdProducto");

                    b.ToTable("Recetas");
                });

            modelBuilder.Entity("ClamarojBack.Models.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("Id");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ClamarojBack.Models.RolUsuario", b =>
                {
                    b.Property<int>("IdRol")
                        .HasColumnType("int");

                    b.Property<int>("IdUsuario")
                        .HasColumnType("int");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.HasKey("IdRol", "IdUsuario");

                    b.HasIndex("IdUsuario");

                    b.ToTable("RolesUsuarios");
                });

            modelBuilder.Entity("ClamarojBack.Models.UnidadMedida", b =>
                {
                    b.Property<int>("IdUnidadMedida")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdUnidadMedida"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.HasKey("IdUnidadMedida");

                    b.HasIndex("Nombre")
                        .IsUnique();

                    b.ToTable("UnidadesMedida");
                });

            modelBuilder.Entity("ClamarojBack.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Foto")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdStatus")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(45)
                        .HasColumnType("nvarchar(45)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PedidoIdPedido")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Correo")
                        .IsUnique();

                    b.HasIndex("PedidoIdPedido");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ClamarojBack.Models.Cliente", b =>
                {
                    b.HasOne("ClamarojBack.Models.Usuario", "Usuario")
                        .WithOne("Cliente")
                        .HasForeignKey("ClamarojBack.Models.Cliente", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClamarojBack.Models.DetallePedido", b =>
                {
                    b.HasOne("ClamarojBack.Models.MateriaPrima", "MateriaPrima")
                        .WithMany()
                        .HasForeignKey("MateriaPrimaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoIdPedido")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoIdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MateriaPrima");

                    b.Navigation("Pedido");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("ClamarojBack.Models.MateriaPrima", b =>
                {
                    b.HasOne("ClamarojBack.Models.Proveedor", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorIdProveedor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.UnidadMedida", "UnidadMedida")
                        .WithMany()
                        .HasForeignKey("UnidadMedidaIdUnidadMedida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Proveedor");

                    b.Navigation("UnidadMedida");
                });

            modelBuilder.Entity("ClamarojBack.Models.Pedido", b =>
                {
                    b.HasOne("ClamarojBack.Models.Estatus", "Estatus")
                        .WithMany()
                        .HasForeignKey("EstatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.Usuario", "Usuario")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Estatus");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClamarojBack.Models.Proveedor", b =>
                {
                    b.HasOne("ClamarojBack.Models.Usuario", "Usuario")
                        .WithOne("Proveedor")
                        .HasForeignKey("ClamarojBack.Models.Proveedor", "IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClamarojBack.Models.Receta", b =>
                {
                    b.HasOne("ClamarojBack.Models.MateriaPrima", "MateriaPrima")
                        .WithMany()
                        .HasForeignKey("MateriaPrimaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoIdProducto")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MateriaPrima");

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("ClamarojBack.Models.RolUsuario", b =>
                {
                    b.HasOne("ClamarojBack.Models.Rol", "Rol")
                        .WithMany("RolesUsuario")
                        .HasForeignKey("IdRol")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ClamarojBack.Models.Usuario", "Usuario")
                        .WithMany("RolesUsuario")
                        .HasForeignKey("IdUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("ClamarojBack.Models.Usuario", b =>
                {
                    b.HasOne("ClamarojBack.Models.Pedido", "Pedido")
                        .WithMany()
                        .HasForeignKey("PedidoIdPedido");

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("ClamarojBack.Models.Rol", b =>
                {
                    b.Navigation("RolesUsuario");
                });

            modelBuilder.Entity("ClamarojBack.Models.Usuario", b =>
                {
                    b.Navigation("Cliente");

                    b.Navigation("Pedidos");

                    b.Navigation("Proveedor");

                    b.Navigation("RolesUsuario");
                });
#pragma warning restore 612, 618
        }
    }
}
