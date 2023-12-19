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
    public class PedidosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly SqlUtil _sqlUtil;

        public PedidosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _sqlUtil = new SqlUtil(configuration);
        }

        // GET: api/Pedidos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            //return await _context.Pedidos.ToListAsync();
            var pedidos = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetPedidos", null);

            return Ok(pedidos);
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pedido>> GetPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            //var pedido = await _context.Pedidos.FindAsync(id);

            var pedido = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetPedido", new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            });

            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido[0]);
        }

        //Get: api/Pedidos/usuario/5
        [HttpGet("usuario/{id}")]
        public async Task<ActionResult<Pedido>> GetPedidosByUsuario(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _sqlUtil.CallSqlFunctionDataAsync("dbo.fxGetPedidosByUsuario", new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            });
            if (pedido == null)
            {
                return NotFound();
            }
            return Ok(pedido);
        }

        // PUT: api/Pedidos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPedido(int id, PedidosDto pedido)
        {
            if (id != pedido.IdPedido)
            {
                return BadRequest();
            }

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new("@Id",pedido.IdPedido),
                    new("@IdUsuario",pedido.IdUsuario),
                    new("@IdStatus", pedido.IdStatus),
                    new("@Fecha",pedido.Fecha),
                    new("@FechaEntrega", pedido.FechaEntrega),
                    new("@Domicilio", pedido.Domicilio),
                    new("@Telefono", pedido.Telefono),
                    new("@RazonSocial", pedido.RazonSocial),
                    new("@Rfc", pedido.Rfc),
                    new("TipoPago", pedido.TipoPago),
                    new("@TipoEnvio", pedido.TipoEnvio),
                    new("@TipoPedido", pedido.TipoPedido),
                    new("@Total",pedido.Total),
                });
                
                pedido.DetallesPedidos.ToArray().ToList().ForEach(async detallePedido =>
                {
                    await _sqlUtil.CallSqlProcedureAsync("dbo.DetallePedidosUPD", new SqlParameter[]
                    {
                        new("@Id", detallePedido.IdDetallePedido),
                        new("@IdPedido", pedido.IdPedido),
                        new("@Fecha", pedido.Fecha),
                        new("@IdProducto", detallePedido.IdProducto),
                        new("@Cantidad", detallePedido.Cantidad),
                        new("@PrecioUnitario", detallePedido.PrecioUnitario),
                        new("@Subtotal", detallePedido.Subtotal)
                    });
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw new Exception("Error al actualizar el pedido");
                }
            }

            return NoContent();
        }

        // POST: api/Pedidos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pedido>> PostPedido(PedidosDto pedido)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }

            var pedidoDto = new PedidosDto();

            try
            {
                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new("@Id",pedido.IdPedido),
                    new("@IdUsuario",pedido.IdUsuario),
                    new("@IdStatus", pedido.IdStatus),
                    new("@Fecha",pedido.Fecha),
                    new("@FechaEntrega", pedido.FechaEntrega),
                    new("@Domicilio", pedido.Domicilio),
                    new("@Telefono", pedido.Telefono),
                    new("@RazonSocial", pedido.RazonSocial),
                    new("@Rfc", pedido.Rfc),
                    new("TipoPago", pedido.TipoPago),
                    new("@TipoEnvio", pedido.TipoEnvio),
                    new("@TipoPedido", pedido.TipoPedido),
                    new("@Total",pedido.Total),
                });

                pedidoDto = await _context.Pedidos.Select((p) => new PedidosDto
                {
                    IdPedido = p.IdPedido,
                    Fecha = p.Fecha
                }).OrderByDescending(p => p.IdPedido).FirstOrDefaultAsync();

                pedido.DetallesPedidos.ToArray().ToList().ForEach(async detallePedido =>
                {
                    await _sqlUtil.CallSqlProcedureAsync("dbo.DetallePedidosUPD", new SqlParameter[]
                    {
                        new("@Id", detallePedido.IdDetallePedido),
                        new("@IdPedido", pedidoDto?.IdPedido),
                        new("@Fecha", pedidoDto?.Fecha),
                        new("@IdProducto", detallePedido.IdProducto),
                        new("@Cantidad", detallePedido.Cantidad),
                        new("@PrecioUnitario", detallePedido.PrecioUnitario),
                        new("@Subtotal", detallePedido.Subtotal)
                    });
                });
            }

            catch (DbUpdateException)
            {
                if (PedidoExists(pedido.IdPedido))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception("Error al crear el pedido");
                }
            }

            return CreatedAtAction("GetPedido", new { id = pedido.IdPedido }, pedido);
        }

        // POST: api/sendPedido/5
        [HttpPost("sendPedido/{id}")]
        public async Task<ActionResult<Pedido>> SendPedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'AppDbContext.Pedidos'  is null.");
            }
            //_context.Pedidos.Add(pedido);
            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.ActualizarPedido", new SqlParameter[]
                {
                    new("@IdPedido",id)
                });
            }
            catch (DbUpdateException)
            {
                if (PedidoExists(id))
                {
                    return Conflict();
                }
                else
                {
                    throw new Exception("Error al actualizar el pedido");
                }
            }

            return Ok();
        }

        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedido(int id)
        {
            if (_context.Pedidos == null)
            {
                return NotFound();
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }

            //_context.Pedidos.Remove(pedido);
            //await _context.SaveChangesAsync();
            await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosDEL", new SqlParameter[]
            {
                new SqlParameter("@Id",id)
            });

            return NoContent();
        }

        private bool PedidoExists(int id)
        {
            return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
