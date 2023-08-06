using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using ClamarojBack.Context;
using ClamarojBack.Models;
using ClamarojBack.Dtos;
using ClamarojBack.Utils;

namespace ClamarojBack.Controllers
{
    [EnableCors("ReglasCorsAngular")]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UnidadMedidasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public UnidadMedidasController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/UnidadMedidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedidaDto>>> GetUnidadesMedida()
        {
            if (_context.UnidadesMedida == null)
            {
                return NotFound();
            }
            var unidadesMedida = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUnidadesMedida", null);

            return Ok(unidadesMedida);
        }

        // GET: api/UnidadMedidas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedidaDto>> GetUnidadMedida(int id)
        {
            if (_context.UnidadesMedida == null)
            {
                return NotFound();
            }
            var unidadMedida = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUnidadesMedida", new SqlParameter[] {
                new SqlParameter("@Id", id)
            });

            if (unidadMedida == null)
            {
                return NotFound();
            }

            return Ok(unidadMedida);
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
            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.UnidadMedidaUPD", new SqlParameter[] {
                    new SqlParameter("@Id", unidadMedida.IdUnidadMedida),
                    new SqlParameter("@Nombre", unidadMedida.Nombre),
                    new SqlParameter("@Descripcion", unidadMedida.Descripcion),
                });
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
        public async Task<ActionResult<UnidadMedidaDto>> PostUnidadMedida(UnidadMedida unidadMedida)
        {
            if (_context.UnidadesMedida == null)
            {
                return Problem("Entity set 'AppDbContext.UnidadesMedida'  is null.");
            }
            await _sqlUtil.CallSqlProcedureAsync("dbo.UnidadMedidaINS", new SqlParameter[] {
                new SqlParameter("@Nombre", unidadMedida.Nombre),
                new SqlParameter("@Descripcion", unidadMedida.Descripcion),
            });

            var unidadMedidaDto = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetUnidadesMedida", new SqlParameter[] {
                new SqlParameter("@Id", unidadMedida.IdUnidadMedida)
            });

            return Ok(unidadMedidaDto);
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

            await _sqlUtil.CallSqlProcedureAsync("dbo.UnidadMedidaDEL", new SqlParameter[] {
                new SqlParameter("@Id", id)
            });

            return NoContent();
        }

        private bool UnidadMedidaExists(int id)
        {
            return (_context.UnidadesMedida?.Any(e => e.IdUnidadMedida == id)).GetValueOrDefault();
        }
    }
}
