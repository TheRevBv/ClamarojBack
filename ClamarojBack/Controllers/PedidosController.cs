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
            //var pedido = await _context.Pedidos.FindAsync(id);
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

            //_context.Entry(pedido).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id",pedido.IdPedido),
                    new SqlParameter("@IdUsuario",pedido.IdUsuario),
                    new SqlParameter("@IdStatus", pedido.IdStatus),
                    new SqlParameter("@Fecha",pedido.Fecha),
                    new SqlParameter("@FechaEntrega", pedido.FechaEntrega),
                    new SqlParameter("@Direccion", pedido.Domicilio),
                    new SqlParameter("@Telefono", pedido.Telefono),
                    new SqlParameter("@RazonSocial", pedido.RazonSocial),
                    new SqlParameter("@Rfc", pedido.Rfc),
                    new SqlParameter("TipoPago", pedido.TipoPago),
                    new SqlParameter("@TipoEnvio", pedido.TipoEnvio),
                    new SqlParameter("@TipoPedido", pedido.TipoPedido),
                    new SqlParameter("@Total",pedido.Total),
                });

                //var pedidoINS = _context.Pedidos.OrderByDescending(p => p.IdPedido).FirstOrDefault();
                decimal total = 0;

                pedido.DetallesPedidos.ToArray().ToList().ForEach(async detallePedido =>
                {
                    await _sqlUtil.CallSqlProcedureAsync("dbo.DetallesPedidosUPD", new SqlParameter[]
                    {
                        new SqlParameter("@Id", detallePedido.IdDetallePedido),
                        new SqlParameter("@IdPedido", pedido.IdPedido),
                        new SqlParameter("@Fecha", detallePedido.Fecha),
                        new SqlParameter("@IdProducto", detallePedido.IdProducto),
                        new SqlParameter("@Cantidad", detallePedido.Cantidad),
                        new SqlParameter("@PrecioUnitario", detallePedido.PrecioUnitario),
                        new SqlParameter("@Subtotal", detallePedido.Subtotal)
                    });
                    total += detallePedido.Subtotal;
                });

                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id",pedido!.IdPedido),
                    new SqlParameter("@IdUsuario",pedido.IdUsuario),
                    new SqlParameter("@IdStatus", pedido.IdStatus),
                    new SqlParameter("@Fecha", pedido.Fecha),
                    new SqlParameter("@FechaEntrega", pedido.FechaEntrega),
                    new SqlParameter("@Direccion", pedido.Domicilio),
                    new SqlParameter("@Telefono", pedido.Telefono),
                    new SqlParameter("@RazonSocial", pedido.RazonSocial),
                    new SqlParameter("@Rfc", pedido.Rfc),
                    new SqlParameter("TipoPago", pedido.TipoPago),
                    new SqlParameter("@TipoEnvio", pedido.TipoEnvio),
                    new SqlParameter("@TipoPedido", pedido.TipoPedido),
                    new SqlParameter("@Total",total),
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
                    throw;
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
            //_context.Pedidos.Add(pedido);
            try
            {
                //await _context.SaveChangesAsync();
                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id",pedido.IdPedido),
                    new SqlParameter("@IdUsuario",pedido.IdUsuario),
                    new SqlParameter("@IdStatus", pedido.IdStatus),
                    new SqlParameter("@Fecha",pedido.Fecha),
                    new SqlParameter("@FechaEntrega", pedido.FechaEntrega),
                    new SqlParameter("@Domicilio", pedido.Domicilio),
                    new SqlParameter("@Telefono", pedido.Telefono),
                    new SqlParameter("@RazonSocial", pedido.RazonSocial),
                    new SqlParameter("@Rfc", pedido.Rfc),
                    new SqlParameter("TipoPago", pedido.TipoPago),
                    new SqlParameter("@TipoEnvio", pedido.TipoEnvio),
                    new SqlParameter("@TipoPedido", pedido.TipoPedido),
                    new SqlParameter("@Total",pedido.Total),
                });

                var pedidoINS = _context.Pedidos.OrderByDescending(p => p.IdPedido).FirstOrDefault();
                decimal total = 0;

                pedido.DetallesPedidos.ToArray().ToList().ForEach(async detallePedido =>
                {
                    await _sqlUtil.CallSqlProcedureAsync("dbo.DetallesPedidosUPD", new SqlParameter[]
                    {
                        new SqlParameter("@Id", detallePedido.IdDetallePedido),
                        new SqlParameter("@IdPedido", pedidoINS!.IdPedido),
                        new SqlParameter("@Fecha", detallePedido.Fecha),
                        new SqlParameter("@IdProducto", detallePedido.IdProducto),
                        new SqlParameter("@Cantidad", detallePedido.Cantidad),
                        new SqlParameter("@PrecioUnitario", detallePedido.PrecioUnitario),
                        new SqlParameter("@Subtotal", detallePedido.Subtotal)
                    });
                    total += detallePedido.Subtotal;
                });

                await _sqlUtil.CallSqlProcedureAsync("dbo.PedidosUPD", new SqlParameter[]
                {
                    new SqlParameter("@Id",pedidoINS!.IdPedido),
                    new SqlParameter("@IdUsuario",pedidoINS.IdUsuario),
                    new SqlParameter("@IdStatus", pedidoINS.IdStatus),
                    new SqlParameter("@Fecha", pedidoINS.Fecha),
                    new SqlParameter("@FechaEntrega", pedidoINS.FechaEntrega),
                    new SqlParameter("@Domicilio", pedidoINS.Domicilio),
                    new SqlParameter("@Telefono", pedidoINS.Telefono),
                    new SqlParameter("@RazonSocial", pedidoINS.RazonSocial),
                    new SqlParameter("@Rfc", pedidoINS.Rfc),
                    new SqlParameter("TipoPago", pedidoINS.TipoPago),
                    new SqlParameter("@TipoEnvio", pedidoINS.TipoEnvio),
                    new SqlParameter("@TipoPedido", pedidoINS.TipoPedido),
                    new SqlParameter("@Total",total),
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
                    throw;
                }
            }

            var pedidoDto = await _context.Pedidos.Where(p => p.IdPedido == pedido.IdPedido).FirstOrDefaultAsync();

            return CreatedAtAction("GetPedido", new { id = pedidoDto!.IdPedido }, pedidoDto);
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
