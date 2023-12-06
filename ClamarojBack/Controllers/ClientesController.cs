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
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public ClientesController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesDto>>> GetClientes()
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }

            var clientes = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetClientes", null);

            return Ok(clientes);
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            // var cliente = await _context.Clientes.FindAsync(id);            
            var cliente = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetCliente",
            new SqlParameter[] {
                new("@Id", id)
            });

            // var clienteDto = cliente.Select(
            //     c => new ClienteDto
            //     {
            //         IdCliente = Convert.ToInt32(c["IdCliente"]),
            //         Rfc = c["Rfc"].ToString(),
            //         Telefono = c["Telefono"].ToString(),
            //         Direccion = c["Direccion"].ToString(),
            //         Usuario = new UsuarioDto
            //         {
            //             Id = Convert.ToInt32(c["IdUsuario"]),
            //             Nombre = c["Nombre"].ToString(),
            //             Apellido = c["Apellido"].ToString(),
            //             Foto = c["Foto"].ToString(),
            //             Correo = c["Correo"].ToString(),
            //             Password = c["Password"].ToString(),
            //             IdStatus = Convert.ToInt32(c["IdStatus"]),
            //             FechaNacimiento = Convert.ToDateTime(c["FechaNacimiento"])
            //         },
            //     }).FirstOrDefault();

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente[0]);
        }

        // GET: api/Clientes/Usuario/5
        [HttpGet("Usuario/{id}")]
        public async Task<ActionResult<ClienteDto>> GetClienteByUser(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            // var cliente = await _context.Clientes.FindAsync(id);            
            var cliente = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetClienteByUsuario",
            new SqlParameter[] {
                new("@IdUsuario", id)
            });

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente[0]);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente clienteDTO)
        {
            var encriptador = new SecurityUtil();
            if (id != clienteDTO.IdCliente)
            {
                return BadRequest();
            }

            var clienteDB = await _context.Clientes.Include(c => c.Usuario).FirstOrDefaultAsync(c => c.IdCliente == id);

            if (clienteDB == null)
            {
                return NotFound();
            }

            DateTime fechaNacimiento = Convert.ToDateTime(clienteDTO.Usuario.FechaNacimiento);

            try
            {
                // await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.ClientesUPD", new SqlParameter[]{
                    new("@Id", id),
                    new("@IdUsuario", clienteDTO.Usuario.Id),
                    new("@Telefono", clienteDTO.Telefono),
                    new("@Direccion", clienteDTO.Direccion),
                    new("@Rfc", clienteDTO.Rfc),
                    new("@Nombre", clienteDTO.Usuario.Nombre),
                    new("@Apellido", clienteDTO.Usuario.Apellido),
                    new("@Correo", clienteDTO.Usuario.Correo),
                    new("@Password", clienteDTO.Usuario.Password),
                    new("@Foto", clienteDTO.Usuario.Foto),
                    new("@FechaNacimiento", fechaNacimiento),
                    new("@IdStatus", clienteDTO.Usuario.IdStatus),
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //Traer el cliente recien creado
            var clienteRes = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetCliente", new SqlParameter[]{
                new("@Id", id)
            });

            return Ok(clienteRes);
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClienteDto>> PostCliente(Cliente client)
        {
            var encriptador = new SecurityUtil();
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'AppDbContext.Clientes'  is null.");
            }
            DateTime fechaNacimiento = Convert.ToDateTime(client.Usuario.FechaNacimiento);

            await _sqlUtil.CallSqlProcedureAsync("dbo.ClientesUPD",
            new SqlParameter[] {
                new("@Id", client.IdCliente),
                new("@Telefono", client.Telefono),
                new("@Direccion", client.Direccion),
                new("@Rfc", client.Rfc),
                new("@Nombre", client.Usuario.Nombre),
                new("@Apellido", client.Usuario.Apellido),
                new("@Correo", client.Usuario.Correo),
                new("@Password", client.Usuario.Password),
                new("@Foto", client.Usuario.Foto),
                new("@FechaNacimiento", fechaNacimiento),
                new("@IdStatus", client.Usuario.IdStatus),
                new("@IdUsuario", client.Usuario.Id)
            });

            //Traer el cliente recien creado
            var clienteRes = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetCliente", new SqlParameter[]{
                new("@Id", client.IdCliente)
            });

            return Ok(clienteRes[0]);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            // _context.Clientes.Remove(cliente);
            // await _context.SaveChangesAsync();
            await _sqlUtil.CallSqlProcedureAsync("dbo.ClientesDEL",
            new SqlParameter[] {
                new("@Id", id)
                });


            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
