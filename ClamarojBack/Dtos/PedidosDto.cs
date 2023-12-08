namespace ClamarojBack.Dtos
{
    public class PedidosDto
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public int IdStatus { get; set; }
        public DateTime? Fecha { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? Domicilio { get; set; }
        public string? Telefono { get; set; }
        public string? RazonSocial { get; set; }
        public string? Rfc { get; set; }
        public char? TipoPago { get; set; }
        public char? TipoEnvio { get; set; }
        public char? TipoPedido { get; set; }
        public decimal Total { get; set; }
        public ICollection<DetallePedidoDto> DetallesPedidos { get; set; } = new List<DetallePedidoDto>();
    }
}
