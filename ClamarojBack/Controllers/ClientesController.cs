using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.AspNetCore.Authorization;
using ClamarojBack.Models.Responses;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System.Linq;
using ClamarojBack.Utils;
using Microsoft.AspNetCore.Cors;
//using ClamarojBack.Dtos;

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
            var clientesRes = clientes.Select(
                c => new ClientesDto
                {
                    IdCliente = Convert.ToInt32(c["IdCliente"]),
                    Nombre = c["Nombre"].ToString(),
                    Apellido = c["Apellido"].ToString(),
                    Rfc = c["Rfc"].ToString(),
                    Telefono = c["Telefono"].ToString(),
                    Direccion = c["Direccion"].ToString(),
                    IdStatus = Convert.ToInt32(c["IdStatus"])
                }).ToList();


            return Ok(clientesRes);


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
                new SqlParameter("@Id", id)
            });

            var clienteDto = cliente.Select(
                c => new ClienteDto
                {
                    IdCliente = Convert.ToInt32(c["IdCliente"]),
                    Rfc = c["Rfc"].ToString(),
                    Telefono = c["Telefono"].ToString(),
                    Direccion = c["Direccion"].ToString(),
                    Usuario = new UsuarioDto
                    {
                        Id = Convert.ToInt32(c["IdUsuario"]),
                        Nombre = c["Nombre"].ToString(),
                        Apellido = c["Apellido"].ToString(),
                        Foto = c["Foto"].ToString(),
                        Correo = c["Correo"].ToString(),
                        Password = c["Password"].ToString(),
                        IdStatus = Convert.ToInt32(c["IdStatus"]),
                        FechaNacimiento = Convert.ToDateTime(c["FechaNacimiento"])
                    },
                }).FirstOrDefault();

            if (clienteDto == null)
            {
                return NotFound();
            }

            return Ok(clienteDto);
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

            if (clienteDTO.Usuario.Password != clienteDB.Usuario.Password)
            {
                clienteDTO.Usuario.Password = encriptador.HashPassword(clienteDTO.Usuario.Password);
            }
            else
            {
                clienteDTO.Usuario.Password = clienteDB.Usuario.Password;
            }
            DateTime fechaNacimiento = Convert.ToDateTime(clienteDTO.Usuario.FechaNacimiento);

            try
            {
                // await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.ClientesUPD", new SqlParameter[]{
                    new SqlParameter("@Id", id),
                    new SqlParameter("@IdUsuario", clienteDTO.Usuario.Id),
                    new SqlParameter("@Telefono", clienteDTO.Telefono),
                    new SqlParameter("@Direccion", clienteDTO.Direccion),
                    new SqlParameter("@Rfc", clienteDTO.Rfc),
                    new SqlParameter("@Nombre", clienteDTO.Usuario.Nombre),
                    new SqlParameter("@Apellido", clienteDTO.Usuario.Apellido),
                    new SqlParameter("@Correo", clienteDTO.Usuario.Correo),
                    new SqlParameter("@Password", clienteDTO.Usuario.Password),
                    new SqlParameter("@Foto", clienteDTO.Usuario.Foto),
                    new SqlParameter("@FechaNacimiento", fechaNacimiento),
                    new SqlParameter("@IdStatus", clienteDTO.Usuario.IdStatus),
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
                new SqlParameter("@Id", id)
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
                new SqlParameter("@Id", client.IdCliente),
                new SqlParameter("@Telefono", client.Telefono),
                new SqlParameter("@Direccion", client.Direccion),
                new SqlParameter("@Rfc", client.Rfc),
                new SqlParameter("@Nombre", client.Usuario.Nombre),
                new SqlParameter("@Apellido", client.Usuario.Apellido),
                new SqlParameter("@Correo", client.Usuario.Correo),
                new SqlParameter("@Password", encriptador.HashPassword(client.Usuario.Password)),
                new SqlParameter("@Foto", client.Usuario.Foto),
                new SqlParameter("@FechaNacimiento", fechaNacimiento),
                new SqlParameter("@IdStatus", client.Usuario.IdStatus),
                new SqlParameter("@IdUsuario", client.Usuario.Id)
            });

            var clienteRes = await _context.Clientes
            .Where(cliente => cliente.Usuario.Correo == client.Usuario.Correo)
            .Join(_context.Usuarios,
                client => client.IdUsuario,
                user => user.Id,
                (client, user) => new ClienteDto
                {
                    IdCliente = client.IdCliente,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion,
                    Rfc = client.Rfc,
                    Usuario = new UsuarioDto
                    {
                        Id = user.Id,
                        Nombre = user.Nombre,
                        Apellido = user.Apellido,
                        Correo = user.Correo,
                        Password = user.Password,
                        Foto = user.Foto,
                        FechaNacimiento = user.FechaNacimiento,
                        FechaRegistro = user.FechaRegistro,
                        IdStatus = user.IdStatus
                    }
                }).FirstOrDefaultAsync(cliente => cliente.IdCliente == client.IdCliente);

            return Ok(clienteRes);
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
            await _sqlUtil.CallSqlProcedureAsync("dbo.ClienteDEL",
            new SqlParameter[] {
                new SqlParameter("@Id", id)
                });


            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
