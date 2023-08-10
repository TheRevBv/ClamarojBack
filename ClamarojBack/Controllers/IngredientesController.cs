using ClamarojBack.Context;
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
    public class IngredientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public IngredientesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingredientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingrediente>>> GetIngrediente()
        {
            if (_context.Ingrediente == null)
            {
                return NotFound();
            }
            //return await _context.Ingrediente.ToListAsync();
            var ingredientes = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetIngredientes", null);

            return Ok(ingredientes);
        }

        // GET: api/Ingredientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingrediente>> GetIngrediente(int idReceta, int idMateriaPrima)
        {
            if (_context.Ingrediente == null)
            {
                return NotFound();
            }
            var ingrediente = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetIngrediente", new SqlParameter[]
            {
                new SqlParameter("@IdReceta", idReceta),
                new SqlParameter("@IdMateriaPrima", idMateriaPrima)
            });

            if (ingrediente == null)
            {
                return NotFound();
            }

            return Ok(ingrediente);
        }

        // PUT: api/Ingredientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngrediente(int id, Ingrediente ingrediente)
        {
            if (id != ingrediente.IdReceta)
            {
                return BadRequest();
            }

            _context.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(id))
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

        // POST: api/Ingredientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ingrediente>> PostIngrediente(Ingrediente ingrediente)
        {
            if (_context.Ingrediente == null)
            {
                return Problem("Entity set 'AppDbContext.Ingrediente'  is null.");
            }
            //_context.Ingrediente.Add(ingrediente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngredienteExists(ingrediente.IdReceta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIngrediente", new { id = ingrediente.IdReceta }, ingrediente);
        }

        // DELETE: api/Ingredientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngrediente(int idReceta, int idMateriaPrima)
        {
            if (_context.Ingrediente == null)
            {
                return NotFound();
            }

            var ingrediente = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetIngrediente", new SqlParameter[]
            {
                new SqlParameter("@IdReceta", idReceta),
                new SqlParameter("@IdMateriaPrima", idMateriaPrima)
            });

            if (ingrediente == null)
            {
                return NotFound();
            }

            await _sqlUtil.CallSqlProcedureAsync("dbo.IngredientesDEL", new SqlParameter[]
            {
                new SqlParameter("@IdReceta", idReceta),
                new SqlParameter("@IdMateriaPrima", idMateriaPrima)
            });

            return NoContent();
        }

        private bool IngredienteExists(int id)
        {
            return (_context.Ingrediente?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
    }
}
