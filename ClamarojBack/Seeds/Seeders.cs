using System;
using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Utils;
using System.Linq; // Add this namespace for querying the database.

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
            SeedRolesUsuarios();
            SeedUnidadesMedida();
            SeedMateriasPrimas();
            SeedProductos();
            // SeedRecetas();
            // SeedPedidos();
            // SeedDetallePedidos();		
        }

        private void SeedEstatus()
        {
            if (!context.Estatus.Any())
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
        }

        private void SeedRoles()
        {
            if (!context.Roles.Any())
            {
                context.Roles.AddRange(
                    new Rol { Nombre = "Administrador" },
                    new Rol { Nombre = "Cliente" },
                    new Rol { Nombre = "Proveedor" }
                );
                context.SaveChanges();
            }
        }

        private void SeedUsuarios()
        {
            if (!context.Usuarios.Any(u => u.Correo == "admin@clamaroj.com"))
            {
                SecurityUtil security = new();

                context.Usuarios.Add(
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
        }

        private void SeedProveedores()
        {
            if (!context.Usuarios.Any(u => u.Correo == "proveedortest@test.com"))
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
        }

        private void SeedClientes()
        {
            if (!context.Usuarios.Any(u => u.Correo == "clienteprueba@test.com"))
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

        private void SeedRolesUsuarios()
        {
            // Verificar si la tabla RolesUsuarios ya tiene registros
            if (!context.RolesUsuarios.Any())
            {
                // Obtener los roles y usuarios existentes en la base de datos
                var roles = context.Roles.ToList();
                var usuarios = context.Usuarios.ToList();

                // Asignar roles a usuarios si existen en la base de datos
                var rolUsuarioList = new List<RolUsuario>();

                // Verificar si los roles y usuarios están presentes en la base de datos
                var adminRole = roles.FirstOrDefault(r => r.Nombre == "Administrador");
                var clienteRole = roles.FirstOrDefault(r => r.Nombre == "Cliente");
                var proveedorRole = roles.FirstOrDefault(r => r.Nombre == "Proveedor");

                var adminUsuario = usuarios.FirstOrDefault(u => u.Correo == "admin@clamaroj.com");
                var clienteUsuario = usuarios.FirstOrDefault(u => u.Correo == "clienteprueba@test.com");
                var proveedorUsuario = usuarios.FirstOrDefault(u => u.Correo == "proveedortest@test.com");

                if (adminRole != null && adminUsuario != null)
                {
                    rolUsuarioList.Add(new RolUsuario { IdRol = adminRole.Id, IdUsuario = adminUsuario.Id, Rol = adminRole, Usuario = adminUsuario });
                }

                if (clienteRole != null && clienteUsuario != null)
                {
                    rolUsuarioList.Add(new RolUsuario { IdRol = clienteRole.Id, IdUsuario = clienteUsuario.Id, Rol = clienteRole, Usuario = clienteUsuario });
                }

                if (proveedorRole != null && proveedorUsuario != null)
                {
                    rolUsuarioList.Add(new RolUsuario { IdRol = proveedorRole.Id, IdUsuario = proveedorUsuario.Id, Rol = proveedorRole, Usuario = proveedorUsuario });
                }

                // Agregar los registros a la tabla RolesUsuarios y guardar cambios
                context.RolesUsuarios.AddRange(rolUsuarioList);
                context.SaveChanges();
            }
        }

        private void SeedMateriasPrimas()
        {
            if (!context.MateriasPrimas.Any())
            {
                var unidadesMedida = context.UnidadesMedida.ToList();
                var proveedores = context.Proveedores.ToList();

                var unidadMedida = unidadesMedida.FirstOrDefault(r => r.IdUnidadMedida == 1);
                var proveedor = proveedores.FirstOrDefault(r => r.IdProveedor == 1);

                context.MateriasPrimas.Add(
                new MateriaPrima
                {
                    Codigo = "EJEMPLOM",
                    Nombre = "Materia Prima Test",
                    Descripcion = "Ejemplo",
                    Perecedero = 10,
                    Stock = 10,
                    CantMinima = 5,
                    CantMaxima = 100,
                    IdUnidadMedida = 1, //KG
                    UnidadMedida = unidadMedida,
                    Precio = 50,
                    IdProveedor = 1,
                    Proveedor = proveedor,
                    FechaRegistro = DateTime.Now,
                    IdStatus = 1
                }
            );
                context.SaveChanges();
            }
        }

        private void SeedUnidadesMedida()
        {
            if (!context.UnidadesMedida.Any())
            {
                context.UnidadesMedida.AddRange(
                    new UnidadMedida { Nombre = "Kilogramo", Descripcion = "kg" },
                    new UnidadMedida { Nombre = "Gramo", Descripcion = "g" },
                    new UnidadMedida { Nombre = "Litro", Descripcion = "l" },
                    new UnidadMedida { Nombre = "Mililitro", Descripcion = "ml" },
                    new UnidadMedida { Nombre = "Pieza", Descripcion = "pz" },
                    new UnidadMedida { Nombre = "Caja", Descripcion = "caja" }
                );
                context.SaveChanges();
            }
        }

        private void SeedProductos()
        {
            if (!context.Productos.Any())
            {

                context.Productos.Add(
                new Producto
                {
                    Codigo = "EJEMPLOP",
                    Nombre = "Producto Test",
                    Descripcion = "Ejemplo",
                    Precio = 50,
                    Merma = 4,
                    FechaRegistro = DateTime.Now,
                    IdStatus = 1
                }
            );
                context.SaveChanges();
            }
        }

    }
}
