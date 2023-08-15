namespace ClamarojBack.Dtos
{
    public class DetallePedidoDto
    {
        public int IdDetallePedido { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
