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
    public class EstatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstatusController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estatus>>> GetEstatus()
        {
          if (_context.Estatus == null)
          {
              return NotFound();
          }
            return await _context.Estatus.ToListAsync();
        }

        // GET: api/Estatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estatus>> GetEstatus(int id)
        {
          if (_context.Estatus == null)
          {
              return NotFound();
          }
            var estatus = await _context.Estatus.FindAsync(id);

            if (estatus == null)
            {
                return NotFound();
            }

            return estatus;
        }

        // PUT: api/Estatus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstatus(int id, Estatus estatus)
        {
            if (id != estatus.Id)
            {
                return BadRequest();
            }

            _context.Entry(estatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstatusExists(id))
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

        // POST: api/Estatus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estatus>> PostEstatus(Estatus estatus)
        {
          if (_context.Estatus == null)
          {
              return Problem("Entity set 'AppDbContext.Estatus'  is null.");
          }
            _context.Estatus.Add(estatus);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstatus", new { id = estatus.Id }, estatus);
        }

        // DELETE: api/Estatus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstatus(int id)
        {
            if (_context.Estatus == null)
            {
                return NotFound();
            }
            var estatus = await _context.Estatus.FindAsync(id);
            if (estatus == null)
            {
                return NotFound();
            }

            _context.Estatus.Remove(estatus);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstatusExists(int id)
        {
            return (_context.Estatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
