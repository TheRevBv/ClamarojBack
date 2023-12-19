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
    public class DetallePedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public DetallePedidosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/DetallePedidos/idPedido
        [HttpGet("{idPedido}")]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetallesPedido(int idPedido)
        {
            if (_context.DetallePedidos == null)
            {
                return NotFound();
            }
            var detallePedido = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetDetallesPedido", new SqlParameter[]
            {
                new SqlParameter("@IdPedido", idPedido)
            });

            return Ok(detallePedido);
        }

        // GET: api/DetallePedidos/Dto/idPedido/1
        [HttpGet("Dto/{idPedido}")]
        public async Task<ActionResult<IEnumerable<DetallePedido>>> GetDetallesPedidoDto(int idPedido)
        {
            if (_context.DetallePedidos == null)
            {
                return NotFound();
            }
            var detallePedido = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetDetallesPedidoLST", new SqlParameter[]
            {
                new SqlParameter("@IdPedido", idPedido)
            });

            return Ok(detallePedido);
        }

        //GET: api/DetallePedido/idPedido/1
        [HttpGet("{idPedido}/{id}")]
        public async Task<ActionResult> GetDetallePedido(int idPedido, int id)
        {
            if (_context.DetallePedidos == null)
            {
                return NotFound();
            }
            var detallePedido = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetDetallePedido",
            new SqlParameter[]
            {
                new SqlParameter("@IdPedido", idPedido),
                new SqlParameter("@Id", id)
            });

            if (detallePedido == null)
            {
                return NotFound();
            }

            return Ok(detallePedido[0]);
        }

        // PUT: api/DetallesPedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult> PutDetallePedido(int id, DetallePedidoDto detallePedido)
        {
            if (_context.DetallePedidos == null)
            {
                return NotFound();
            }
            if (id != detallePedido.IdDetallePedido)
            {
                return BadRequest();
            }
            //_context.Entry(detallePedido).State = EntityState.Modified;
            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.DetallePedidoUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id", detallePedido.IdDetallePedido),
                    new SqlParameter("@IdPedido", detallePedido.IdPedido),
                    new SqlParameter("@Fecha", detallePedido.Fecha),
                    new SqlParameter("@IdProducto", detallePedido.IdProducto),
                    new SqlParameter("@Cantidad", detallePedido.Cantidad),
                    new SqlParameter("@PrecioUnitario", detallePedido.PrecioUnitario),
                    new SqlParameter("@Subtotal", detallePedido.Subtotal)
                });
            }
            catch (DbUpdateConcurrencyException) when (!DetallePedidoExists(id))
            {
                return NotFound();
            }
            return NoContent();
        }

        // POST: api/DetallePedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DetallePedido>> PostDetallePedido(DetallePedidoDto detallePedido)
        {
            if (_context.DetallePedidos == null)
            {
                return Problem("Entity set 'AppDbContext.DetallePedidos'  is null.");
            }
            //_context.DetallePedidos.Add(detallePedido);
            //await _context.SaveChangesAsync();
            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.DetallePedidoUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id", detallePedido.IdDetallePedido),
                    new SqlParameter("@IdPedido", detallePedido.IdPedido),
                    new SqlParameter("@Fecha", detallePedido.Fecha),
                    new SqlParameter("@IdProducto", detallePedido.IdProducto),
                    new SqlParameter("@Cantidad", detallePedido.Cantidad),
                    new SqlParameter("@PrecioUnitario", detallePedido.PrecioUnitario),
                    new SqlParameter("@Subtotal", detallePedido.Subtotal)
                });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }

            var detallePedidoDto = await _context.DetallePedidos
                .OrderByDescending(x => x.IdDetallePedido)
                .FirstAsync();

            return CreatedAtAction("GetDetallePedido", new { id = detallePedidoDto.IdDetallePedido }, detallePedidoDto);
        }

        // DELETE: api/DetallePedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDetallePedido(int id)
        {
            if (_context.DetallePedidos == null)
            {
                return NotFound();
            }
            var detallePedido = await _context.DetallePedidos.FindAsync(id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            _context.DetallePedidos.Remove(detallePedido);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DetallePedidoExists(int id)
        {
            return (_context.DetallePedidos?.Any(e => e.IdDetallePedido == id)).GetValueOrDefault();
        }
    }
}
