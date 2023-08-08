using ClamarojBack.Context;
using ClamarojBack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ClamarojBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriaPrimasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MateriaPrimasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/MateriaPrimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetMateriasPrimas()
        {
            if (_context.MateriasPrimas == null)
            {
                return NotFound();
            }
            return await _context.MateriasPrimas.ToListAsync();
        }

        // GET: api/MateriaPrimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
        {
            if (_context.MateriasPrimas == null)
            {
                return NotFound();
            }

            var materiaPrima = await _context.MateriasPrimas.FindAsync(id);

            if (materiaPrima == null)
            {
                return NotFound();
            }

            return materiaPrima;
        }

        // POST: api/MateriaPrimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> PostMateriaPrima(MateriaPrima materiaPrima)
        {
            if (_context.MateriasPrimas == null)
            {
                return Problem("Entity set 'AppDbContext.MateriasPrimas'  is null.");
            }

            _context.MateriasPrimas.Add(materiaPrima);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMateriaPrima", new { id = materiaPrima.Id }, materiaPrima);
        }

        // PUT: api/MateriasPrimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriaPrima(int id, MateriaPrima materiaPrima)
        {
            if (id != materiaPrima.Id)
            {
                return BadRequest();
            }

            _context.Entry(materiaPrima).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MateriaPrimaExists(id))
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

        // DELETE: api/MateriasPrimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateriaPrima(int id)
        {
            if (_context.MateriasPrimas == null)
            {
                return NotFound();
            }
            var materiaPrima = await _context.MateriasPrimas.FindAsync(id);
            if (materiaPrima == null)
            {
                return NotFound();
            }

            materiaPrima.IdStatus = 0;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MateriaPrimaExists(int id)
        {
            return (_context.MateriasPrimas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
