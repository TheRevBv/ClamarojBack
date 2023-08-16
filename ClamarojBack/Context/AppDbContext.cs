using ClamarojBack.Models;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<Rol> Roles { get; set; } = null!;
        public DbSet<RolUsuario> RolesUsuarios { get; set; } = null!;
        public DbSet<Estatus> Estatus { get; set; } = null!;
        public DbSet<MateriaPrima> MateriasPrimas { get; set; } = null!;
        public DbSet<Proveedor> Proveedores { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Receta> Recetas { get; set; } = null!;
        public DbSet<UnidadMedida> UnidadesMedida { get; set; } = null!;
        public DbSet<Pedido> Pedidos { get; set; } = null!;
        public DbSet<DetallePedido> DetallePedidos { get; set; } = null!;
        public DbSet<Carrito> Carritos { get; set; } = null!;
        public DbSet<Venta> Ventas { get; set; } = null!;
        public DbSet<Compra> Compras { get; set; } = null!;
        public DbSet<Ingrediente> Ingrediente { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolUsuario>()
                .HasKey(ru => new { ru.IdRol, ru.IdUsuario });
            modelBuilder.Entity<RolUsuario>()
                .HasOne(ru => ru.Rol)
                .WithMany(r => r.RolesUsuario)
                .HasForeignKey(ru => ru.IdRol);
            modelBuilder.Entity<RolUsuario>()
                .HasOne(ru => ru.Usuario)
                .WithMany(u => u.RolesUsuario)
                .HasForeignKey(ru => ru.IdUsuario);
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Correo)
                .IsUnique();
            modelBuilder.Entity<Rol>()
                .HasIndex(r => r.Nombre)
                .IsUnique();
            modelBuilder.Entity<Proveedor>()
                .HasOne(p => p.Usuario)          // Un Proveedor tiene un Usuario asociado
                .WithOne(u => u.Proveedor)      // Un Usuario está asociado a un Proveedor
                .HasForeignKey<Proveedor>(p => p.IdUsuario);  // La clave foránea está en la entidad Proveedor
            // Relación uno a uno entre Cliente y Usuario
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Usuario)          // Un Cliente tiene un Usuario asociado
                .WithOne(u => u.Cliente)        // Un Usuario está asociado a un Cliente
                .HasForeignKey<Cliente>(c => c.IdUsuario);
            modelBuilder.Entity<Estatus>()
                .HasIndex(e => e.Nombre)
                .IsUnique();
            modelBuilder.Entity<MateriaPrima>()
                .HasIndex(mp => mp.Codigo)
                .IsUnique();
            modelBuilder.Entity<MateriaPrima>()
                .HasOne(mp => mp.Proveedor)
                .WithMany(p => p.MateriasPrimas)
                .HasForeignKey(mp => mp.IdProveedor);
            modelBuilder.Entity<MateriaPrima>()
                .HasOne(mp => mp.UnidadMedida)
                .WithMany(um => um.MateriasPrimas)
                .HasForeignKey(mp => mp.IdUnidadMedida);
            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => p.RazonSocial)
                .IsUnique();
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Codigo)
                .IsUnique();
            modelBuilder.Entity<Receta>()
                .HasIndex(r => r.Codigo)
                .IsUnique();
            modelBuilder.Entity<UnidadMedida>()
                .HasIndex(um => um.Nombre)
                .IsUnique();
            modelBuilder.Entity<Pedido>()
                .HasKey(p => new { p.IdPedido, p.Fecha });
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuario)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.IdUsuario);
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Estatus)
                .WithMany(e => e.Pedidos)
                .HasForeignKey(p => p.IdStatus);
            modelBuilder.Entity<DetallePedido>()
                .HasOne(dp => dp.Pedido)
                .WithMany(p => p.DetallesPedidos)
                //Agrega la llave foranea compuesta a la tabla DetallePedido
                .HasForeignKey(dp => new { dp.IdPedido, dp.Fecha })
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_DetallePedido_Pedido");
            //modelBuilder.Entity<DetallePedido>()
            //    .HasOne(dp => dp.Producto)
            //    .WithMany(p => p.DetallePedidos)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasForeignKey(dp => dp.IdProducto);
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Cliente)
                .WithMany(c => c.Carrito)
                .HasForeignKey(c => c.IdCliente);
            modelBuilder.Entity<Carrito>()
                .HasOne(c => c.Producto)
                .WithMany(p => p.Carrito)
                .HasForeignKey(c => c.IdProducto);
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Pedido)
                .WithOne(p => p.Venta)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey<Venta>(v => new { v.IdPedido, v.Fecha });
            modelBuilder.Entity<Venta>()
                .HasOne(v => v.Cliente)
                .WithMany(u => u.Ventas)
                .HasForeignKey(v => v.IdCliente);
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Pedido)
                .WithOne(p => p.Compra)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey<Compra>(c => new { c.IdPedido, c.Fecha });
            modelBuilder.Entity<Compra>()
                .HasOne(c => c.Proveedor)
                .WithMany(p => p.Compras)
                // .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(c => c.IdProveedor);
            modelBuilder.Entity<Receta>()
                .HasOne(r => r.Producto)
                .WithOne(p => p.Receta)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey<Receta>(r => r.IdProducto);
            modelBuilder.Entity<Ingrediente>()
                .HasKey(i => new { i.IdReceta, i.IdMateriaPrima });
            modelBuilder.Entity<Ingrediente>()
                .HasOne(i => i.Receta)
                .WithMany(r => r.Ingredientes)
                .HasForeignKey(i => i.IdReceta);
            modelBuilder.Entity<Ingrediente>()
                .HasOne(i => i.MateriaPrima)
                .WithMany(mp => mp.Ingredientes)
                .HasForeignKey(i => i.IdMateriaPrima);
        }
    }

}