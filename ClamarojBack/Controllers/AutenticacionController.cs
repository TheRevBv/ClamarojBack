using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Models.Responses;
using ClamarojBack.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClamarojBack.Controllers
{
    [EnableCors("AllowFlutterApp")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext context;
        private readonly string mySecretKey;

        public AuthController(AppDbContext _context, IConfiguration _configuration)
        {
            context = _context;
            mySecretKey = _configuration.GetSection("JWT").GetSection("Key").ToString()!;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            IActionResult response = StatusCode(401, "Usuario o contraseña incorrectos");
            var user = await AutenticarUsuario(login);
            if (user != null)
            {
                var tokenString = GenerarToken(user);
                // var rolesUsuario = await context.RolesUsuarios.Where(ru => ru.IdUsuario == user.Id).ToListAsync();
                // int[] roles = rolesUsuario.Select(ru => ru.IdRol).ToArray();
                var roles2 = await context.RolesUsuarios
                .Where(ru => ru.IdUsuario == user.Id)
                .Join(context.Roles, ru => ru.IdRol, r => r.Id, (ru, r) => new IRolRes { Id = r.Id, Nombre = r.Nombre, Descripcion = r.Descripcion })
                .ToListAsync();

                var usuario = new IUsuarioRes
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Correo = user.Correo,
                    Foto = user.Foto,
                    FechaNacimiento = user.FechaNacimiento,
                    FechaRegistro = user.FechaRegistro,
                    IdStatus = user.IdStatus,
                    // Roles = roles
                    Roles = roles2
                };

                response = StatusCode(200, new { token = tokenString, usuario });
            }
            return response;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            var encriptador = new SecurityUtil();
            IActionResult response = StatusCode(401, "No se pudo registrar el usuario");
            var usuarioEncontrado = context.Usuarios.Where(u => u.Correo == usuario.Correo).FirstOrDefault();
            if (usuarioEncontrado == null)
            {
                //Encriptar password
                var contrasena = encriptador.HashPassword(usuario.Password);
                usuario.Password = contrasena;
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                response = StatusCode(200, new { mensaje = "Usuario registrado correctamente" });
            }
            return response;
        }

        private async Task<Usuario?> AutenticarUsuario(Login login)
        {
            try
            {
                // IUsuarioRes? usuario = null;
                var encriptador = new SecurityUtil();
                if (login.Correo == "" || login.Password == "" || login.Correo == null || login.Password == null)
                {
                    return null;
                }
                //Encriptar password
                var contrasena = encriptador.HashPassword(login.Password);

                var usuarioEncontrado = await context.Usuarios.Where(u => u.Correo == login.Correo && u.Password == contrasena).FirstOrDefaultAsync();


                if (usuarioEncontrado != null)
                {
                    return usuarioEncontrado;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private string GenerarToken(Usuario usuario)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var llave = Encoding.ASCII.GetBytes(mySecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Correo!),
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

    }
}

