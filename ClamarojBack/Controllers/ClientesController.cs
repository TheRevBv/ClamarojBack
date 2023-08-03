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
using Microsoft.AspNetCore.Cors;

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
                (client, user) => new
                {
                    client.IdCliente,
                    client.Telefono,
                    client.Direccion,
                    client.Rfc,
                    usuario = new
                    {
                        user.Id,
                        user.Nombre,
                        user.Apellido,
                        user.Correo,
                        user.Password,
                        user.Foto,
                        user.FechaNacimiento,
                        user.FechaRegistro,
                        user.IdStatus
                    }
                }).FirstOrDefaultAsync(cliente => cliente.IdCliente == id);


            if (clienteRes == null)
            {
                return NotFound();
            }

            return Ok(clienteRes);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente clienteDTO)
        {
            if (id != clienteDTO.IdCliente)
            {
                return BadRequest();
            }

            var clienteDB = await _context.Clientes.FindAsync(id);

            if (clienteDB == null)
            {
                return NotFound();
            }

            clienteDB.Telefono = clienteDTO.Telefono;
            clienteDB.Direccion = clienteDTO.Direccion;
            clienteDB.Rfc = clienteDTO.Rfc;

            var usuarioDB = await _context.Usuarios.FindAsync(clienteDB.IdUsuario);

            if (usuarioDB == null)
            {
                return NotFound();
            }

            usuarioDB.Nombre = clienteDTO.Usuario.Nombre;
            usuarioDB.Apellido = clienteDTO.Usuario.Apellido;
            usuarioDB.Correo = clienteDTO.Usuario.Correo;
            usuarioDB.Foto = clienteDTO.Usuario.Foto;
            usuarioDB.FechaNacimiento = clienteDTO.Usuario.FechaNacimiento;
            usuarioDB.IdStatus = clienteDTO.Usuario.IdStatus;

            _context.Entry(clienteDB).State = EntityState.Modified;
            _context.Entry(usuarioDB).State = EntityState.Modified;

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
                }).FirstOrDefaultAsync(clienteR => clienteR.IdCliente == clienteDB.IdCliente);

            return Ok(clienteRes);
        }

        /*
        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                return BadRequest();
            }
            //Ignorar el update de la contraseña del usuario
            //var clienteDTO = new ClienteUpdateDTO
            //{
            //    IdCliente = cliente.IdCliente,
            //    IdUsuario = cliente.IdUsuario,
            //    Telefono = cliente.Telefono,
            //    Direccion = cliente.Direccion,
            //    Rfc = cliente.Rfc,
            //    Usuario = new UsuarioUpdateDTO
            //    {
            //        Id = cliente.Usuario.Id,
            //        Nombre = cliente.Usuario.Nombre,
            //        Apellido = cliente.Usuario.Apellido,
            //        Correo = cliente.Usuario.Correo,
            //        Foto = cliente.Usuario.Foto,
            //        FechaNacimiento = cliente.Usuario.FechaNacimiento,
            //        FechaRegistro = cliente.Usuario.FechaRegistro,
            //        IdStatus = cliente.Usuario.IdStatus,
            //    }
            //};
            //var clienteDTO = new
            //{
            //    cliente.IdCliente,
            //    cliente.Direccion,
            //    cliente.Telefono,
            //    cliente.Rfc,
            //    usuario = new
            //    {
            //        cliente.Usuario.Id,
            //        cliente.Usuario.Nombre,
            //        cliente.Usuario.Apellido,
            //        cliente.Usuario.Correo,
            //        cliente.Usuario.Foto,
            //        cliente.Usuario.FechaNacimiento,                    
            //        cliente.Usuario.IdStatus,
            //    }
            //};


            try
            {
                //_context.Entry(cliente).State = EntityState.Modified;
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
        */

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente client)
        {
            var encriptador = new SecurityUtil();
            if (_context.Clientes == null)
            {
                return Problem("Entity set 'AppDbContext.Clientes'  is null.");
            }
            client.Usuario.Password = encriptador.HashPassword(client.Usuario.Password);
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
