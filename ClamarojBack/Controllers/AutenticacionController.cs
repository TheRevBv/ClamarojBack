using ClamarojBack.Context;
using ClamarojBack.Dtos;
using ClamarojBack.Models;
using ClamarojBack.Utils;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;
        private readonly string mySecretKey;

        public AuthController(AppDbContext context, IConfiguration _configuration)
        {
            _context = context;
            mySecretKey = _configuration.GetSection("JWT").GetSection("Key").ToString()!;
            _sqlUtil = new SqlUtil(_configuration);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login login)
        {
            IActionResult response = StatusCode(401, "Usuario o contraseña incorrectos");
            var user = await AutenticarUsuario(login);
            if (user != null)
            {
                var tokenString = GenerarToken(user);
                // var rolesUsuario = await _context.RolesUsuarios.Where(ru => ru.IdUsuario == user.Id).ToListAsync();
                // int[] roles = rolesUsuario.Select(ru => ru.IdRol).ToArray();
                var roles = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetRolesUsuario", new SqlParameter[] {
                    new("@IdUsuario", user.Id)
                });

                var rolesUsuario = Newtonsoft.Json.JsonConvert.SerializeObject(roles);
                var rolesDto = JsonConvert.DeserializeObject<RolDto[]>(rolesUsuario);

                user.Roles = rolesDto!;

                response = StatusCode(200, new { token = tokenString, usuario = user });
            }
            return response;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> RegistrarAsync([FromBody] UsuarioDto usuario)
        {
            var encriptador = new SecurityUtil();
            IActionResult response = StatusCode(401, "No se pudo registrar el usuario");
            var usuarioEncontrado = _context.Usuarios.Where(u => u.Correo == usuario.Correo).FirstOrDefault();
            if (usuarioEncontrado == null)
            {
                usuario.FechaNacimiento = Convert.ToDateTime(usuario.FechaNacimiento);
                //Convertir Arreglo de roles a cadena separada por comas
                string roles = "";
                foreach (var rol in usuario.Roles)
                {
                    roles += rol.Id + ",";
                }

                await _sqlUtil.CallSqlProcedureAsync("dbo.UsuariosUPD", new SqlParameter[]{
                    new("@Id", usuario.Id),
                    new("@Nombre", usuario.Nombre),
                    new("@Apellido", usuario.Apellido),
                    new("@Correo", usuario.Correo),
                    //new("@Password", encriptador.HashPassword(usuario.Password!)),
                    new("@Password", usuario.Password),
                    new("@FechaNacimiento", usuario.FechaNacimiento),
                    new("@Foto", usuario.Foto),
                    new("@IdRoles" , roles),
                    new("@IdStatus", usuario.IdStatus)
                });
                response = StatusCode(200, new { mensaje = "Usuario registrado correctamente", usuario });
            }
            return response;
        }

        private async Task<UsuarioDto?> AutenticarUsuario(Login login)
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
                // var contrasena = encriptador.HashPassword(login.Password);

                // var usuarioEncontrado = await _context.Usuarios.Where(u => u.Correo == login.Correo && u.Password == contrasena).FirstOrDefaultAsync();
                var usuarioEncontrado = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxLoginUsuario", new SqlParameter[]
                {
                    new("@Correo", login.Correo),
                    new("@Password", login.Password)
                });
                //Validar si la lista no esta vacia
                if (usuarioEncontrado != null && usuarioEncontrado.Count > 0)
                {
                    var usuarioDto = JsonConvert.SerializeObject(usuarioEncontrado[0]);
                    var usuario = JsonConvert.DeserializeObject<UsuarioDto>(usuarioDto);
                    return usuario;
                }
                return null;
            }
            catch (System.Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        private string GenerarToken(object usuario)
        {
            try
            {
                var user = (UsuarioDto)usuario;
                var tokenHandler = new JwtSecurityTokenHandler();
                var llave = Encoding.ASCII.GetBytes(mySecretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Correo!.ToString()),
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

