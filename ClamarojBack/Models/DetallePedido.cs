using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        [Column(TypeName = "DATETIME")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int IdProducto { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal PrecioUnitario { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Subtotal { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
    }
}