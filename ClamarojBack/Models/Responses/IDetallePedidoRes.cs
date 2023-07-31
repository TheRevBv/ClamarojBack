namespace ClamarojBack.Models.Responses
{
    public class IDetallePedidoRes
    {
        public int IdDetallePedido { get; set; }
        public int? IdPedido { get; set; }
        public int? IdProducto { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Total { get; set; }
    }
}