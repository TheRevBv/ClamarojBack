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
using Newtonsoft.Json;
using System.Linq;
using ClamarojBack.Utils;

namespace ClamarojBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        //private readonly SqlUtil sqlUtil;

        public ClientesController(AppDbContext context)//, SqlUtil _sqlUtil
        {
            _context = context;
            //sqlUtil = _sqlUtil;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }

            //var clientes = JsonConvert.SerializeObject(sqlUtil.CallSqlFunctionData("GetClientes", null));
            //Consulta usando LINQ para traer los datos de la tabla Clientes y Usuarios
            var clientes = await _context.Clientes.Join(_context.Usuarios,
                client => client.IdUsuario,
                user => user.Id,
                (client, user) => new IClientesRes
                {
                    IdCliente = client.IdCliente,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Correo = user.Correo,
                    Foto = user.Foto,
                    FechaNacimiento = user.FechaNacimiento,
                    FechaRegistro = user.FechaRegistro,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion,
                    Rfc = client.Rfc,
                    IdStatus = user.IdStatus,
                }).ToListAsync();

            return Ok(clientes);


        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IClientesRes>> GetCliente(int id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            // var cliente = await _context.Clientes.FindAsync(id);
            //Consulta de datos usando LINQ
            var clienteRes = await _context.Clientes.Join(_context.Usuarios,
                client => client.IdUsuario,
                user => user.Id,
                (client, user) => new IClientesRes
                {
                    IdCliente = client.IdCliente,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Correo = user.Correo,
                    Foto = user.Foto,
                    FechaNacimiento = user.FechaNacimiento,
                    FechaRegistro = user.FechaRegistro,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion,
                    Rfc = client.Rfc,
                    IdStatus = user.IdStatus,
                }).FirstOrDefaultAsync(cliente => cliente.IdCliente == id);

            if (clienteRes == null)
            {
                return NotFound();
            }

            return Ok(clienteRes);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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

            return NoContent();
        }

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente client)
        {
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'AppDbContext.Clientes'  is null.");
            }
            _context.Clientes.Add(client);
            await _context.SaveChangesAsync();

            //Traer el cliente recien creado
            var clienteRes = await _context.Clientes.Join(_context.Usuarios,
                client => client.IdUsuario,
                user => user.Id,
                (client, user) => new IClientesRes
                {
                    IdCliente = client.IdCliente,
                    Nombre = user.Nombre,
                    Apellido = user.Apellido,
                    Correo = user.Correo,
                    Foto = user.Foto,
                    FechaNacimiento = user.FechaNacimiento,
                    FechaRegistro = user.FechaRegistro,
                    Telefono = client.Telefono,
                    Direccion = client.Direccion,
                    Rfc = client.Rfc,
                    IdStatus = user.IdStatus,
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

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Clientes?.Any(e => e.IdCliente == id)).GetValueOrDefault();
        }
    }
}
