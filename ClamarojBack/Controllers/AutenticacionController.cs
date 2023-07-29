using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Utils;
using ClamarojBack.Models.Responses;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
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
        public IActionResult Login([FromBody] Login login)
        {
            IActionResult response = StatusCode(401, "Usuario o contraseña incorrectos");
            var user = AutenticarUsuario(login);
            if (user != null)
            {
                var roles = context.RolesUsuarios.Where(ru => ru.IdUsuario == user.Id).ToList();
                // var rol = context.Roles.Where(r => r.Id == roles[0].IdRol).ToList();
                var listaRoles = new List<IRolRes>();
                // var listaRolId = new List<int>();
                roles.ForEach(r =>
                {
                    var rol = context.Roles.Where(i => i.Id == r.IdRol).FirstOrDefault();
                    listaRoles.Add(new IRolRes
                    {
                        Id = rol.Id,
                        Nombre = rol.Nombre,
                        Descripcion = rol.Descripcion
                    });
                });
                var usuario = new IUsuarioRes
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Correo = user.Correo,
                    Apellido = user.Apellido,
                    FechaNacimiento = user.FechaNacimiento,
                    FechaRegistro = user.FechaRegistro,
                    Foto = user.Foto,
                    IdStatus = user.IdStatus,
                    Roles = listaRoles
                };
                var tokenString = GenerarToken(user);
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

        private Usuario AutenticarUsuario(Login login)
        {
            Usuario? usuario = null;
            var encriptador = new SecurityUtil();
            if (login.Correo == "" || login.Password == "" || login.Correo == null || login.Password == null)
            {
                return usuario;
            }
            //Encriptar password
            var contrasena = encriptador.HashPassword(login.Password);

            var usuarioEncontrado = context.Usuarios.Where(u => u.Correo == login.Correo && u.Password == contrasena).FirstOrDefault();

            if (usuarioEncontrado != null)
            {
                usuario = usuarioEncontrado;
            }
            return usuario;
        }

        private string GenerarToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(mySecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Correo),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

