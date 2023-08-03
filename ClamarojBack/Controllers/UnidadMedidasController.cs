using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClamarojBack.Context;
using ClamarojBack.Models;

namespace ClamarojBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadMedidasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UnidadMedidasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/UnidadMedidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedida>>> GetUnidadesMedida()
        {
          if (_context.UnidadesMedida == null)
          {
              return NotFound();
          }
            return await _context.UnidadesMedida.ToListAsync();
        }

        // GET: api/UnidadMedidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedida>> GetUnidadMedida(int id)
        {
          if (_context.UnidadesMedida == null)
          {
              return NotFound();
          }
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);

            if (unidadMedida == null)
            {
                return NotFound();
            }

            return unidadMedida;
        }

        // PUT: api/UnidadMedidas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUnidadMedida(int id, UnidadMedida unidadMedida)
        {
            if (id != unidadMedida.IdUnidadMedida)
            {
                return BadRequest();
            }

            _context.Entry(unidadMedida).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UnidadMedidaExists(id))
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

        // POST: api/UnidadMedidas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UnidadMedida>> PostUnidadMedida(UnidadMedida unidadMedida)
        {
          if (_context.UnidadesMedida == null)
          {
              return Problem("Entity set 'AppDbContext.UnidadesMedida'  is null.");
          }
            _context.UnidadesMedida.Add(unidadMedida);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUnidadMedida", new { id = unidadMedida.IdUnidadMedida }, unidadMedida);
        }

        // DELETE: api/UnidadMedidas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUnidadMedida(int id)
        {
            if (_context.UnidadesMedida == null)
            {
                return NotFound();
            }
            var unidadMedida = await _context.UnidadesMedida.FindAsync(id);
            if (unidadMedida == null)
            {
                return NotFound();
            }

            _context.UnidadesMedida.Remove(unidadMedida);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UnidadMedidaExists(int id)
        {
            return (_context.UnidadesMedida?.Any(e => e.IdUnidadMedida == id)).GetValueOrDefault();
        }
    }
}
