﻿using ClamarojBack.Context;
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
    public class EstatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public EstatusController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Estatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstatusDto>>> GetEstatus()
        {
            if (_context.Estatus == null)
            {
                return NotFound();
            }
            var estatus = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetEstatuses", null);

            return Ok(estatus);
        }

        // GET: api/Estatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstatusDto>> GetEstatus(int id)
        {
            if (_context.Estatus == null)
            {
                return NotFound();
            }
            var estatus = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetEstatus", new SqlParameter[] {
                new("@Id", id)
            });

            if (estatus == null)
            {
                return NotFound();
            }

            return Ok(estatus[0]);
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

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.EstatusUPD",
                new SqlParameter[] {
                    new("@Id", estatus.Id),
                    new("@Nombre", estatus.Nombre)
                });
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
        public async Task<ActionResult<EstatusDto>> PostEstatus(Estatus estatus)
        {
            if (_context.Estatus == null)
            {
                return Problem("Entity set 'AppDbContext.Estatus'  is null.");
            }
            await _sqlUtil.CallSqlProcedureAsync("dbo.EstatusUPD",
                new SqlParameter[] {
                    new("@Id", estatus.Id),
                    new("@Nombre", estatus.Nombre)
                });
            var status = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetEstatus", new SqlParameter[] {
                new("@Id", estatus.Id)
            });

            return Ok(status[0]);
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

            await _sqlUtil.CallSqlProcedureAsync("dbo.EstatusDEL",
                new SqlParameter[] {
                    new("@Id", estatus.Id)
                });

            return NoContent();
        }

        private bool EstatusExists(int id)
        {
            return (_context.Estatus?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
