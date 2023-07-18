using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacionController : Controller
    {
        private readonly AppDbContext context;
        private readonly string mySecretKey;

        public AutenticacionController(AppDbContext _context, IConfiguration _configuration)
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
                var tokenString = GenerarToken(user);
                response = StatusCode(200, new { token = tokenString });
            }
            return response;
        }

        [HttpPost("registrar")]
        public IActionResult Registrar([FromBody] Usuario usuario)
        {
            IActionResult response = StatusCode(401, "No se pudo registrar el usuario");
            var usuarioEncontrado = context.Usuarios.Where(u => u.Correo == usuario.Correo).FirstOrDefault();
            if (usuarioEncontrado == null)
            {
                context.Usuarios.Add(usuario);
                context.SaveChanges();
                response = StatusCode(200, new { mensaje = "Usuario registrado correctamente" });
            }
            return response;
        }

        private Usuario AutenticarUsuario(Login login)
        {
            Usuario? usuario = null;
            var usuarioEncontrado = context.Usuarios.Where(u => u.Correo == login.Correo && u.Password == login.Password).FirstOrDefault();
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
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}

