using ClamarojBack.Context;
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
            //COnsulta usando una funcion de tabla de sql server
            var result = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUsuarios", null);

            foreach (var usuario in result)
            {
                var roles = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetRolesUsuario", new SqlParameter[] {
                    new("@IdUsuario", usuario["id"])
                });
                usuario["roles"] = roles;
            }

            return Ok(result);
        }

        // GET: api/UsuariosP
        [HttpGet("usuariosp")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuariosP()
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuarios = await _context.Usuarios
                //.Include(u => u.RolesUsuario)
                //.ThenInclude(ru => ru.Rol)
                .ToListAsync();

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

            var usuario = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUsuario", new SqlParameter[] {
                new("@Id", id)
            });


            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario[0]);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, UsuarioDto usuario)
        {
            var encriptador = new SecurityUtil();
            if (id != usuario.Id)
            {
                return BadRequest();
            }

            DateTime fechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
            //Convertir Arreglo de roles a cadena separada por comas
            string roles = "";
            foreach (var rol in usuario.Roles)
            {
                roles += rol.Id + ",";
            }
            roles = roles != "" ? roles.TrimEnd(',') : roles;

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.UsuariosUPD", new SqlParameter[]
                {
                    new("@Id", usuario.Id),
                    new("@Nombre", usuario.Nombre),
                    new("@Apellido", usuario.Apellido),
                    new("@Correo", usuario.Correo),
                    new("@Password", usuario.Password),
                    new("@FechaNacimiento", fechaNacimiento),
                    new("@Foto", usuario.Foto),
                    new("@IdStatus", usuario.IdStatus),
                    new("@IdRoles", roles)
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
        public async Task<ActionResult<UsuarioDto>> PostUsuario(UsuarioDto usuario)
        {
            var encriptador = new SecurityUtil();
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'AppDbContext.Usuarios'  is null.");
            }
            DateTime fechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
            //Convertir Arreglo de roles a cadena separada por comas
            string roles = "";
            foreach (var rol in usuario.Roles)
            {
                roles += rol.Id + ",";
            }

            await _sqlUtil.CallSqlProcedureAsync("dbo.UsuariosUPD", new SqlParameter[]
            {
                new("@Id", usuario.Id),
                new("@Nombre", usuario.Nombre),
                new("@Apellido", usuario.Apellido),
                new("@Correo", usuario.Correo),
                //new("@Password", encriptador.HashPassword(usuario.Password!)),
                new("@Password", usuario.Password),
                new("@FechaNacimiento", fechaNacimiento),
                new("@Foto", usuario.Foto),
                new("@IdRoles" , roles),
                new("@IdStatus", usuario.IdStatus)
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
                    Password = u.Password,
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
