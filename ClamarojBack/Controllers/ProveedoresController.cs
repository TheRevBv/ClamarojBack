﻿using ClamarojBack.Context;
using ClamarojBack.Dtos;
using ClamarojBack.Models;
using ClamarojBack.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Controllers
{

    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProveedoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public ProveedoresController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Proveedores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedoresDto>>> GetProveedores()
        {
            var result = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetProveedores", null);

            //var proveedores = result.Select(p => new ProveedoresDto
            //{
            //    IdProveedor = Convert.ToInt32(p["IdProveedor"]),
            //    Direccion = p["Direccion"].ToString(),
            //    Telefono = p["Telefono"].ToString(),
            //    Correo = p["Correo"].ToString(),
            //    Rfc = p["Rfc"].ToString(),
            //    RazonSocial = p["RazonSocial"].ToString(),
            //    IdStatus = Convert.ToInt32(p["IdStatus"])
            //}).ToList();            

            return Ok(result);

        }

        // GET: api/Proveedores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProveedorDto>> GetProveedor(int id)
        {
            if (_context.Proveedores == null)
            {
                return NotFound();
            }
            var proveedor = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetProveedor",
            new SqlParameter[] {
                new SqlParameter("@Id", id)
            });

            //var proveedorDto = proveedor.Select(p => new ProveedorDto
            //{
            //    IdProveedor = Convert.ToInt32(p["IdProveedor"]),
            //    Direccion = p["Direccion"].ToString(),
            //    Telefono = p["Telefono"].ToString(),
            //    Rfc = p["Rfc"].ToString(),
            //    RazonSocial = p["RazonSocial"].ToString(),
            //    IdStatus = Convert.ToInt32(p["IdStatus"]),
            //    Usuario = new UsuarioDto
            //    {
            //        Id = Convert.ToInt32(p["IdUsuario"]),
            //        Nombre = p["Nombre"].ToString(),
            //        Apellido = p["Apellido"].ToString(),
            //        Correo = p["Correo"].ToString(),
            //        Password = p["Password"].ToString(),
            //        FechaNacimiento = Convert.ToDateTime(p["FechaNacimiento"]),
            //        Foto = p["Foto"].ToString(),
            //        IdStatus = Convert.ToInt32(p["IdStatus"])
            //    }
            //}).FirstOrDefault();

            if (proveedor == null)
            {
                return NotFound();
            }

            return Ok(proveedor[0]);
        }

        // PUT: api/Proveedores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProveedor(int id, Proveedor proveedor)
        {
            var encriptador = new SecurityUtil();
            if (id != proveedor.IdProveedor)
            {
                return BadRequest();
            }

            var proveedorDB = await _context.Proveedores.Include(c => c.Usuario).FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedorDB == null)
            {
                return NotFound();
            }

            DateTime fechaNacimiento = Convert.ToDateTime(proveedor.Usuario.FechaNacimiento);
            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.ProveedoresUPD",
                new SqlParameter[] {
                    new SqlParameter("@Id", proveedor.IdProveedor),
                    new SqlParameter("@Direccion", proveedor.Direccion),
                    new SqlParameter("@Telefono", proveedor.Telefono),
                    new SqlParameter("@Rfc", proveedor.Rfc),
                    new SqlParameter("@RazonSocial", proveedor.RazonSocial),
                    new SqlParameter("@Nombre", proveedor.Usuario.Nombre),
                    new SqlParameter("@Apellido", proveedor.Usuario.Apellido),
                    new SqlParameter("@Correo", proveedor.Usuario.Correo),
                    new SqlParameter("@Password", proveedor.Usuario.Password),
                    new SqlParameter("@FechaNacimiento", fechaNacimiento),
                    new SqlParameter("@Foto", proveedor.Usuario.Foto),
                    new SqlParameter("@IdStatus", proveedor.Usuario.IdStatus),
                    new SqlParameter("@IdUsuario", proveedor.Usuario.Id)
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proveedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Proveedor>> PostProveedor(Proveedor proveedor)
        {
            var encriptador = new SecurityUtil();
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'AppDbContext.Proveedores'  is null.");
            }
            // _context.Proveedores.Add(proveedor);
            DateTime fechaNac = Convert.ToDateTime(proveedor.Usuario.FechaNacimiento);
            await _sqlUtil.CallSqlProcedureAsync("dbo.ProveedoresUPD",
                new SqlParameter[] {
                    new SqlParameter("@Id", proveedor.IdProveedor),
                    new SqlParameter("@Direccion", proveedor.Direccion),
                    new SqlParameter("@Telefono", proveedor.Telefono),
                    new SqlParameter("@Rfc", proveedor.Rfc),
                    new SqlParameter("@RazonSocial", proveedor.RazonSocial),
                    new SqlParameter("@Nombre", proveedor.Usuario.Nombre),
                    new SqlParameter("@Apellido", proveedor.Usuario.Apellido),
                    new SqlParameter("@Correo", proveedor.Usuario.Correo),
                    new SqlParameter("@Password", proveedor.Usuario.Password),
                    new SqlParameter("@FechaNacimiento", fechaNac),
                    new SqlParameter("@Foto", proveedor.Usuario.Foto),
                    new SqlParameter("@IdStatus", proveedor.Usuario.IdStatus),
                    new SqlParameter("@IdUsuario", proveedor.Usuario.Id),
                });

            var prov = await _context.Proveedores
            .Where(p => p.Usuario.Correo == proveedor.Usuario.Correo)
            .Select(p => new ProveedorDto
            {
                IdProveedor = p.IdProveedor,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Rfc = p.Rfc,
                RazonSocial = p.RazonSocial,
                Usuario = new UsuarioDto
                {
                    Nombre = p.Usuario.Nombre,
                    Apellido = p.Usuario.Apellido,
                    Correo = p.Usuario.Correo,
                    Password = p.Usuario.Password,
                    FechaNacimiento = p.Usuario.FechaNacimiento,
                    Foto = p.Usuario.Foto,
                    IdStatus = p.Usuario.IdStatus,
                    // Roles = new List<RolDto>()
                }
            }).FirstOrDefaultAsync();

            return Ok(prov);
        }

        [HttpPut("prov/{id}")]
        public async Task<IActionResult> PutProvider(int id, ProviderDto proveedor)
        {
            var encriptador = new SecurityUtil();
            if (id != proveedor.IdProveedor)
            {
                return BadRequest();
            }

            var proveedorDB = await _context.Proveedores.Include(c => c.Usuario).FirstOrDefaultAsync(p => p.IdProveedor == id);

            if (proveedorDB == null)
            {
                return NotFound();
            }

            DateTime fechaNacimiento = Convert.ToDateTime(proveedor.FechaNacimiento);
            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.ProveedoresUPD",
                new SqlParameter[] {
                    new SqlParameter("@Id", proveedor.IdProveedor),
                    new SqlParameter("@Direccion", proveedor.Direccion),
                    new SqlParameter("@Telefono", proveedor.Telefono),
                    new SqlParameter("@Rfc", proveedor.Rfc),
                    new SqlParameter("@RazonSocial", proveedor.RazonSocial),
                    new SqlParameter("@Nombre", proveedor.Nombre),
                    new SqlParameter("@Apellido", proveedor.Apellido),
                    new SqlParameter("@Correo", proveedor.Correo),
                    new SqlParameter("@Password", proveedor.Password),
                    new SqlParameter("@FechaNacimiento", fechaNacimiento),
                    new SqlParameter("@Foto", proveedor.Foto),
                    new SqlParameter("@IdStatus", proveedor.IdStatus),
                    new SqlParameter("@IdUsuario", proveedor.IdUsuario)
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProveedorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var res = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetProveedor", new SqlParameter[]
            {
                new SqlParameter("@Id", proveedor.IdProveedor)
            });

            return Ok(res[0]);
        }

        // POST: api/Proveedores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("prov")]
        public async Task<ActionResult<Proveedor>> PostProvider(ProviderDto proveedor)
        {
            var encriptador = new SecurityUtil();
            if (_context.Proveedores == null)
            {
                return Problem("Entity set 'AppDbContext.Proveedores'  is null.");
            }
            // _context.Proveedores.Add(proveedor);
            DateTime fechaNac = Convert.ToDateTime(proveedor.FechaNacimiento);
            await _sqlUtil.CallSqlProcedureAsync("dbo.ProveedoresUPD",
                new SqlParameter[] {
                    new SqlParameter("@Id", proveedor.IdProveedor),
                    new SqlParameter("@Direccion", proveedor.Direccion),
                    new SqlParameter("@Telefono", proveedor.Telefono),
                    new SqlParameter("@Rfc", proveedor.Rfc),
                    new SqlParameter("@RazonSocial", proveedor.RazonSocial),
                    new SqlParameter("@Nombre", proveedor.Nombre),
                    new SqlParameter("@Apellido", proveedor.Apellido),
                    new SqlParameter("@Correo", proveedor.Correo),
                    new SqlParameter("@Password", encriptador.HashPassword(proveedor.Password!)),
                    new SqlParameter("@FechaNacimiento", fechaNac),
                    new SqlParameter("@Foto", proveedor.Foto),
                    new SqlParameter("@IdStatus", proveedor.IdStatus),
                    new SqlParameter("@IdUsuario", proveedor.IdUsuario),
                });

            var prov = await _context.Proveedores
            .Where(p => p.Usuario.Correo == proveedor.Correo)
            .Select(p => new ProveedorDto
            {
                IdProveedor = p.IdProveedor,
                Direccion = p.Direccion,
                Telefono = p.Telefono,
                Rfc = p.Rfc,
                RazonSocial = p.RazonSocial,
                Usuario = new UsuarioDto
                {
                    Nombre = p.Usuario.Nombre,
                    Apellido = p.Usuario.Apellido,
                    Correo = p.Usuario.Correo,
                    Password = p.Usuario.Password,
                    FechaNacimiento = p.Usuario.FechaNacimiento,
                    Foto = p.Usuario.Foto,
                    IdStatus = p.Usuario.IdStatus,
                    // Roles = new List<RolDto>()
                }
            }).FirstOrDefaultAsync();

            var res = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetProveedor", new SqlParameter[]
            {
                new SqlParameter("@Id", prov!.IdProveedor)
            });

            return Ok(res[0]);
        }

        // DELETE: api/Proveedores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProveedor(int id)
        {
            if (_context.Proveedores == null)
            {
                return NotFound();
            }
            var proveedor = await _context.Proveedores.FindAsync(id);
            if (proveedor == null)
            {
                return NotFound();
            }

            await _sqlUtil.CallSqlProcedureAsync("dbo.ProveedorDEL",
                new SqlParameter[] {
                    new SqlParameter("@Id", proveedor.IdProveedor)
                });

            // _context.Proveedores.Remove(proveedor);
            // await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProveedorExists(int id)
        {
            return (_context.Proveedores?.Any(e => e.IdProveedor == id)).GetValueOrDefault();
        }
    }
}
