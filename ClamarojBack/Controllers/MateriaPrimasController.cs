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
    public class MateriaPrimasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public MateriaPrimasController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/MateriaPrimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MateriaPrima>>> GetMateriasPrimas()
        {
            if (_context.MateriasPrimas == null)
            {
                return NotFound();
            }
            //return await _context.MateriasPrimas.ToListAsync();
            var materiasPrimas = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetMateriasPrimas", null);

            return Ok(materiasPrimas);
        }

        // GET: api/MateriaPrimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MateriaPrima>> GetMateriaPrima(int id)
        {
            if (_context.MateriasPrimas == null)
            {
                return NotFound();
            }

            var materiaPrima = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetMateriaPrima",
                new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                });

            if (materiaPrima == null)
            {
                return NotFound();
            }

            return Ok(materiaPrima[0]);
        }

        // POST: api/MateriaPrimas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<MateriaPrima>> PostMateriaPrima(MateriasPrimasDto materiaPrima)
        {
            if (_context.MateriasPrimas == null)
            {
                return Problem("Entity set 'AppDbContext.MateriasPrimas'  is null.");
            }

            //_context.MateriasPrimas.Add(materiaPrima);
            //await _context.SaveChangesAsync();
            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.MateriasPrimasUPD",
                new SqlParameter[]
                {
                    new SqlParameter("@Id", materiaPrima.Id),
                    new SqlParameter("@Codigo", materiaPrima.Codigo),
                    new SqlParameter("@Nombre", materiaPrima.Nombre),
                    new SqlParameter("@Descripcion", materiaPrima.Descripcion),
                    new SqlParameter("@Perecedero", materiaPrima.Perecedero),
                    new SqlParameter("@Stock",materiaPrima.Stock),
                    new SqlParameter("@CantMinima",materiaPrima.CantMinima),
                    new SqlParameter("@CantMaxima",materiaPrima.CantMaxima),
                    new SqlParameter("@IdUnidadMedida", materiaPrima.IdUnidadMedida),
                    new SqlParameter("@Precio",materiaPrima.Precio),
                    new SqlParameter("@Foto", materiaPrima.Foto),
                    new SqlParameter("@IdProveedor", materiaPrima.IdProveedor),
                    new SqlParameter("@IdStatus", materiaPrima.IdStatus),
                });
            }
            catch (DbUpdateException)
            {
                //TODO: La neta esto no se si jalee xD
                if (MateriaPrimaExists(materiaPrima.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            var materiaPrimaDto = await _context.MateriasPrimas
                .OrderByDescending(x => x.Id)
                .FirstAsync();

            return CreatedAtAction("GetMateriaPrima", new { id = materiaPrimaDto.Id }, materiaPrimaDto);
        }

        // PUT: api/MateriasPrimas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateriaPrima(int id, MateriasPrimasDto materiaPrima)
        {
            if (id != materiaPrima.Id)
            {
                return BadRequest();
            }            

            try
            {                
                await _sqlUtil.CallSqlProcedureAsync("dbo.MateriasPrimasUPD",
                new SqlParameter[]
                {
                    new SqlParameter("@Id", materiaPrima.Id),
                    new SqlParameter("@Codigo", materiaPrima.Codigo),
                    new SqlParameter("@Nombre", materiaPrima.Nombre),
                    new SqlParameter("@Descripcion", materiaPrima.Descripcion),
                    new SqlParameter("@Perecedero", materiaPrima.Perecedero),
                    new SqlParameter("@Stock",materiaPrima.Stock),
                    new SqlParameter("@CantMinima",materiaPrima.CantMinima),
                    new SqlParameter("@CantMaxima",materiaPrima.CantMaxima),
                    new SqlParameter("@IdUnidadMedida", materiaPrima.IdUnidadMedida),
                    new SqlParameter("@Precio",materiaPrima.Precio),
                    new SqlParameter("@Foto", materiaPrima.Foto),
                    new SqlParameter("@IdProveedor", materiaPrima.IdProveedor),
                    new SqlParameter("@IdStatus", materiaPrima.IdStatus),
                });
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

            await _sqlUtil.CallSqlProcedureAsync("dbo.MateriasPrimasDEL", new SqlParameter[]
            {
                new SqlParameter("@Id", id)
            });

            return NoContent();
        }

        private bool MateriaPrimaExists(int id)
        {
            return (_context.MateriasPrimas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
