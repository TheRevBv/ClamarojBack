namespace ClamarojBack.Models.Responses;

public class IPedidoRes
{
    public int IdPedido { get; set; }
    public int? IdCliente { get; set; }
    public int? IdStatus { get; set; }
    public DateTime? FechaPedido { get; set; }
    public DateTime? FechaEntrega { get; set; }
    public string? DireccionEntrega { get; set; }
    public string? TelefonoEntrega { get; set; }
    public string? Observaciones { get; set; }
    public decimal? Total { get; set; }
    public List<IDetallePedidoRes>? DetallePedidos { get; set; }
}