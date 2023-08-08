using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PedidosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pedidoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            return await _context.Pedidos.ToListAsync();
        }

        // GET: api/Pedidoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);

            if (pedido == null)
            {
                return NotFound();
            }

            return pedido;
        }

        // PUT: api/Pedidoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            _context.Entry(pedido).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
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

        // POST: api/Pedidoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(Pedido pedido)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }
            _context.Pedidos.Add(pedido);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PedidoExists(pedido.IdPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPedido", new { id = pedido.IdPedido }, pedido);
        }

        // DELETE: api/Pedidoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
