using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class DetallePedido
    {
        [Key]
        public int IdDetallePedido { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int IdMateriaPrima { get; set; }
        public int IdUnidadMedida { get; set; }
        public decimal Cantidad { get; set; }
        [DataType(DataType.Currency)]
        public decimal PrecioUnitario { get; set; }
        [DataType(DataType.Currency)]
        public decimal Subtotal { get; set; }
        public Pedido Pedido { get; set; } = null!;
        public Producto Producto { get; set; } = null!;
        public MateriaPrima MateriaPrima { get; set; } = null!;
    }
}