using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.Extensions.Configuration;
using ClamarojBack.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public UsuariosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosDto>>> GetUsuarios()
        {
            /* var usuarios = await _context.Usuarios
                .Join(_context.Estatus, u => u.IdStatus, e => e.Id, (u, e) =>
                    new UsuariosDto
                    {
                        Id = u.Id,
                        NombreCompleto = $"{u.Nombre} {u.Apellido}",
                        Correo = u.Correo,
                        FechaNacimiento = u.FechaNacimiento,
                        Foto = u.Foto,
                        Estatus = e.Nombre,
                        IdStatus = u.IdStatus
                    })
                .ToListAsync(); */

            //COnsulta usando una funcion de tabla de sql server
            var result = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUsuarios", null);

            var usuarios = result.Select(r => new UsuariosDto
            {
                Id = Convert.ToInt32(r["Id"]),
                Nombre = r["Nombre"].ToString(),
                Apellido = r["Apellido"].ToString(),
                Correo = r["Correo"].ToString(),
                FechaNacimiento = Convert.ToDateTime(r["FechaNacimiento"]),
                Foto = r["Foto"].ToString(),
                Estatus = r["Estatus"].ToString(),
                IdStatus = Convert.ToInt32(r["IdStatus"]),
                Roles = new List<RolDto>()
            }).ToList();

            foreach (var usuario in usuarios)
            {
                var roles = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetRolesUsuario", new SqlParameter[] {
                    new SqlParameter("@IdUsuario", usuario.Id)
                });
                usuario.Roles = roles.Select(r => new RolDto
                {
                    Id = Convert.ToInt32(r["Id"]),
                    Nombre = r["Nombre"].ToString()
                }).ToList();
            }



            //var usuarios = await _context.Usuarios
            //    .Select(u => new UsuariosDto
            //    {
            //        Id = u.Id,
            //        NombreCompleto = $"{u.Nombre} {u.Apellido}",
            //        Correo = u.Correo,
            //        FechaNacimiento = u.FechaNacimiento,
            //        Foto = u.Foto,
            //        IdStatus = u.IdStatus,
            //        Roles = u.RolesUsuario.Select(ru => new RolDto
            //        {
            //            Id = ru.IdRol,
            //            Nombre = ru.Rol.Nombre
            //        }).ToList()
            //    })
            //    .ToListAsync();

            //return usuarios;
            //return Ok(result);
            return Ok(usuarios);
        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioDto>> GetUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Where(u => u.Id == id)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Correo = u.Correo,
                    FechaNacimiento = u.FechaNacimiento,
                    Foto = u.Foto,
                    IdStatus = u.IdStatus,
                    Roles = u.RolesUsuario.Select(ru => new RolDto
                    {
                        Id = ru.IdRol,
                        Nombre = ru.Rol.Nombre
                    }).ToList()
                })
                .FirstOrDefaultAsync();


            if (usuario == null)
            {
                return NotFound();
            }

            return usuario;
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            //_context.Entry(usuario).State = EntityState.Modified;            

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.UsuariosUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id", usuario.Id),
                    new SqlParameter("@Nombre", usuario.Nombre),
                    new SqlParameter("@Apellido", usuario.Apellido),
                    new SqlParameter("@Correo", usuario.Correo),
                    new SqlParameter("@FechaNacimiento", usuario.FechaNacimiento),
                    new SqlParameter("@Foto", usuario.Foto),
                    new SqlParameter("@IdStatus", usuario.IdStatus)
                });
                //await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(id))
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

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsuarioDto>> PostUsuario(Usuario usuario)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.Usuarios'  is null.");
            }
            //_context.Usuarios.Add(usuario);
            //await _context.SaveChangesAsync();
            await _sqlUtil.CallSqlProcedureAsync("dbo.UsuariosUPD", new SqlParameter[]
            {
                new SqlParameter("@Nombre", usuario.Nombre),
                new SqlParameter("@Apellido", usuario.Apellido),
                new SqlParameter("@Correo", usuario.Correo),
                new SqlParameter("@FechaNacimiento", usuario.FechaNacimiento),
                new SqlParameter("@Foto", usuario.Foto),
                new SqlParameter("@IdStatus", usuario.IdStatus)
            });
            //Traer el usuario recien creado
            var user = await _context.Usuarios
                .Where(u => u.Correo == usuario.Correo)
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Correo = u.Correo,
                    FechaNacimiento = u.FechaNacimiento,
                    Foto = u.Foto,
                    IdStatus = u.IdStatus,
                    Roles = u.RolesUsuario.Select(ru => new RolDto
                    {
                        Id = ru.IdRol,
                        Nombre = ru.Rol.Nombre
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            return Ok(user);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(int id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
