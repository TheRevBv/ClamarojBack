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
        public DbSet<Insumo> Insumos { get; set; } = null!;
        public DbSet<Proveedor> Proveedores { get; set; } = null!;
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Producto> Productos { get; set; } = null!;
        public DbSet<Receta> Recetas { get; set; } = null!;
        public DbSet<UnidadMedida> UnidadesMedida { get; set; } = null!;
        // public DbSet<DetalleReceta> DetallesReceta { get; set; } = null!;        

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
            modelBuilder.Entity<Estatus>()
                .HasIndex(e => e.Nombre)
                .IsUnique();
            modelBuilder.Entity<Insumo>()
                .HasIndex(i => i.Nombre)
                .IsUnique();
            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => p.RazonSocial)
                .IsUnique();
            modelBuilder.Entity<Proveedor>()
                .HasIndex(p => p.Correo)
                .IsUnique();
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Correo)
                .IsUnique();
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.Nombre)
                .IsUnique();
            modelBuilder.Entity<Receta>()
                .HasIndex(r => r.Nombre)
                .IsUnique();
            modelBuilder.Entity<UnidadMedida>()
                .HasIndex(u => u.Nombre)
                .IsUnique();
            // modelBuilder.Entity<DetalleReceta>()
            //     .HasKey(dr => new { dr.IdReceta, dr.IdInsumo });
            // modelBuilder.Entity<DetalleReceta>()
            //     .HasOne(dr => dr.Receta)
            //     .WithMany(r => r.DetallesReceta)
            //     .HasForeignKey(dr => dr.IdReceta);
            // modelBuilder.Entity<DetalleReceta>()
            //     .HasOne(dr => dr.Insumo)
            //     .WithMany(i => i.DetallesReceta)
            //     .HasForeignKey(dr => dr.IdInsumo);
        }
    }
}