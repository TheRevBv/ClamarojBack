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
    public class IngredientesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public IngredientesController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
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
        [HttpGet("{idReceta}/{idMateriaPrima}")]
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

            return Ok(ingrediente[0]);
        }

        //GET: api/Ingredientes/GetIngredientesReceta/5
        [HttpGet, HttpPost]
        [Route("GetIngredientesReceta/{idReceta}")]
        public async Task<ActionResult<IEnumerable<Ingrediente>>> GetIngredientesReceta(int idReceta)
        {
            if (_context.Ingrediente == null)
            {
                return NotFound();
            }
            var ingredientes = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetIngredientesReceta", new SqlParameter[]
            {
                new SqlParameter("@IdReceta", idReceta)
            });
            if (ingredientes == null)
            {
                return NotFound();
            }
            return Ok(ingredientes);
        }

        // PUT: api/Ingredientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{idReceta}/{idMateriaPrima}")]
        public async Task<IActionResult> PutIngrediente(int idReceta, int idMateriaPrima, IngredienteDto ingrediente)
        {
            if (idReceta != ingrediente.IdReceta && idMateriaPrima != ingrediente.IdMateriaPrima)
            {
                return BadRequest();
            }

            //_context.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.IngredientesUPD", new SqlParameter[]
                {
                    new SqlParameter("@IdReceta", ingrediente.IdReceta),
                    new SqlParameter("@IdMateriaPrima", ingrediente.IdMateriaPrima),
                    new SqlParameter("@Cantidad", ingrediente.Cantidad)
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExists(idReceta, idMateriaPrima))
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
        public async Task<ActionResult<Ingrediente>> PostIngrediente(IngredienteDto ingrediente)
        {
            if (_context.Ingrediente == null)
            {
                return Problem("Entity set 'AppDbContext.Ingrediente'  is null.");
            }
            //_context.Ingrediente.Add(ingrediente);
            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.IngredientesUPD", new SqlParameter[]
                {
                    new SqlParameter("@IdReceta", ingrediente.IdReceta),
                    new SqlParameter("@IdMateriaPrima", ingrediente.IdMateriaPrima),
                    new SqlParameter("@Cantidad", ingrediente.Cantidad)
                });
            }
            catch (DbUpdateException)
            {
                if (IngredienteExists(ingrediente.IdReceta, ingrediente.IdMateriaPrima))
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
        [HttpDelete("{idReceta}/{idMateriaPrima}")]
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

        private bool IngredienteExists(int id, int idMateriaPrima)
        {
            return (_context.Ingrediente?.Any(e => e.IdReceta == id && e.IdMateriaPrima == idMateriaPrima) ?? false);
        }
    }
}
