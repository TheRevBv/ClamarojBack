using System;
using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Utils;

namespace ClamarojBack
{
    public class Seeders
    {
        private readonly AppDbContext context;
        public Seeders(AppDbContext _context)
        {
            context = _context;
        }

        public void Seed()
        {
            SeedEstatus();
            SeedRoles();
            SeedUsuarios();
            SeedProveedores();
            SeedClientes();
            // SeedMateriasPrimas();
            // SeedUnidadesMedida();
            // SeedProductos();
            // SeedRecetas();
            // SeedPedidos();
            // SeedDetallePedidos();		
        }

        private void SeedEstatus()
        {
            context.Estatus.AddRange(
                new Estatus { Nombre = "Activo" },
                new Estatus { Nombre = "Inactivo" },
                new Estatus { Nombre = "Pendiente" },
                new Estatus { Nombre = "Enviado" },
                new Estatus { Nombre = "Entregado" },
                new Estatus { Nombre = "Cancelado" }
            );
            context.SaveChanges();
        }

        private void SeedRoles()
        {
            context.Roles.AddRange(
                new Rol { Nombre = "Administrador" },
                new Rol { Nombre = "Cliente" },
                new Rol { Nombre = "Proveedor" }
            );
            context.SaveChanges();
        }

        private void SeedUsuarios()
        {
            SecurityUtil security = new();

            context.Usuarios.AddRange(
                new Usuario
                {
                    Nombre = "Administrador",
                    Apellido = "Administrador",
                    Correo = "admin@clamaroj.com",
                    Password = security.HashPassword("Admin123"),
                    FechaNacimiento = Convert.ToDateTime("01/01/1900"),
                    FechaRegistro = DateTime.Now,
                    IdStatus = 1
                }
            );
            context.SaveChanges();
        }

        private void SeedProveedores()
        {
            SecurityUtil security = new();

            var proveedor = new Proveedor
            {
                Rfc = "XAXX010101000",
                RazonSocial = "Proveedor de prueba",
                Direccion = "Calle 1 # 1, Colonia 1, Ciudad 1, Estado 1",
                Telefono = "1234567890",
            };

            var proveedorUsuario = new Usuario
            {
                Nombre = "Proveedor",
                Apellido = "Proveedor",
                Correo = "proveedortest@test.com",
                Password = security.HashPassword("Proveedor123"),
                FechaNacimiento = Convert.ToDateTime("01/01/1900"),
                FechaRegistro = DateTime.Now,
                IdStatus = 1
            };

            proveedor.Usuario = proveedorUsuario;

            context.Proveedores.Add(proveedor);
            context.SaveChanges();
        }

        private void SeedClientes()
        {
            SecurityUtil security = new();

            var cliente = new Cliente
            {
                Rfc = "XAXX010101000",
                Direccion = "Calle 1 # 1, Colonia 1, Ciudad 1, Estado 1",
                Telefono = "1234567890",
            };

            var clienteUsuario = new Usuario
            {
                Nombre = "Cliente",
                Apellido = "Cliente",
                Correo = "clienteprueba@test.com",
                Password = security.HashPassword("Cliente123"),
                FechaNacimiento = Convert.ToDateTime("01/01/1900"),
                FechaRegistro = DateTime.Now,
                IdStatus = 1
            };

            cliente.Usuario = clienteUsuario;

            context.Clientes.Add(cliente);
            context.SaveChanges();

        }

    }
}

