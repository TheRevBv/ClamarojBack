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
    public class CarritosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public CarritosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Carritoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritos()
        {
            if (_context.Carritos == null)
            {
                return NotFound();
            }
            return await _context.Carritos.ToListAsync();
        }

        // GET: api/Carritoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carrito>> GetCarrito(int id)
        {
            if (_context.Carritos == null)
            {
                return NotFound();
            }
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        [HttpGet("cliente/{idCliente}")]
        public async Task<ActionResult<IEnumerable<Carrito>>> GetCarritoCliente(int idCliente)
        {
            if (idCliente == 0)
            {
                return NotFound();
            }
            var carrito = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetCarritoProductos",
            new SqlParameter[]
            {
                new("@IdCliente", idCliente)
            });

            return Ok(carrito);
        }

        // PUT: api/Carritoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.IdCarrito)
            {
                return BadRequest();
            }

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.CarritoUPD", new SqlParameter[]
                {
                    new("@IdCarrito", carrito.IdCarrito),
                    new("@IdCliente", carrito.IdCliente),
                    new("@IdProducto", carrito.IdProducto),
                    new("@Cantidad", carrito.Cantidad)
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritoExists(id))
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

        // POST: api/Carritoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CarritoDto>> PostCarrito(CarritoDto carrito)
        {
            if (_context.Carritos == null)
            {
                return Problem("Entity set 'AppDbContext.Carritos'  is null.");
            }

            await _sqlUtil.CallSqlProcedureAsync("dbo.CarritoUPD", new SqlParameter[]
            {
                new("@IdCarrito", carrito.IdCarrito),
                new("@IdCliente", carrito.IdCliente),
                new("@IdProducto", carrito.IdProducto),
                new("@Cantidad", carrito.Cantidad)
            });

            var res = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetCarritoProductos",
            new SqlParameter[]
            {
                new("@IdCliente", carrito.IdCliente)
            });

            return Ok(res);
        }

        // DELETE: api/Carritoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrito(int id)
        {
            if (_context.Carritos == null)
            {
                return NotFound();
            }
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritoExists(int id)
        {
            return (_context.Carritos?.Any(e => e.IdCarrito == id)).GetValueOrDefault();
        }
    }
}
