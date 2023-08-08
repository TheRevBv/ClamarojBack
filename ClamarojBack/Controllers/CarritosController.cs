using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CarritosController(AppDbContext context)
        {
            _context = context;
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

        // PUT: api/Carritoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarrito(int id, Carrito carrito)
        {
            if (id != carrito.Id)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
        public async Task<ActionResult<Carrito>> PostCarrito(Carrito carrito)
        {
            if (_context.Carritos == null)
            {
                return Problem("Entity set 'AppDbContext.Carritos'  is null.");
            }
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarrito", new { id = carrito.Id }, carrito);
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
            return (_context.Carritos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
