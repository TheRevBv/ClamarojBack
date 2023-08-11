using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Utils;
using ClamarojBack.Dtos;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RecetasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public RecetasController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Recetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }

            var recetas = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetRecetas", null);

            return Ok(recetas);

        }

        // GET: api/Recetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }

            var receta = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetReceta", new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            });


            if (receta == null)
            {
                return NotFound();
            }

            return Ok(receta[0]);
        }

        // PUT: api/Recetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, RecetasDto receta)
        {
            if (id != receta.IdReceta)
            {
                return BadRequest();
            }

            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.RecetasUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id", receta.IdReceta),
                    new SqlParameter("@Codigo", receta.Codigo),
                    new SqlParameter("@Cantidad", receta.Cantidad),
                    new SqlParameter("@Costo", receta.Costo),
                    new SqlParameter("@IdProducto", receta.IdProducto),
                    new SqlParameter("@IdStatus", receta.IdStatus),
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
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

        // POST: api/Recetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(RecetasDto receta)
        {
            if (_context.Recetas == null)
            {
                return Problem("Entity set 'AppDbContext.Recetas'  is null.");
            }
            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.RecetasUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id", receta.IdReceta),
                    new SqlParameter("@Codigo", receta.Codigo),
                    new SqlParameter("@Cantidad", receta.Cantidad),
                    new SqlParameter("@Costo", receta.Costo),
                    new SqlParameter("@IdProducto", receta.IdProducto),
                    new SqlParameter("@IdStatus", receta.IdStatus),
                });
            }
            catch (DbUpdateException)
            {
                //TODO: La neta esto no se si jalee xD
                if (RecetaExists(receta.IdReceta))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var recetaDto = await _context.Recetas
                .OrderByDescending(r => r.IdReceta)
                .FirstAsync();

            return CreatedAtAction("GetReceta", new { id = recetaDto.IdReceta }, recetaDto);
        }

        // DELETE: api/Recetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            if (_context.Recetas == null)
            {
                return NotFound();
            }
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            await _sqlUtil.CallSqlProcedureAsync("dbo.RecetasDEL", new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            });

            return NoContent();
        }

        private bool RecetaExists(int id)
        {
            return (_context.Recetas?.Any(e => e.IdReceta == id)).GetValueOrDefault();
        }
    }
}
