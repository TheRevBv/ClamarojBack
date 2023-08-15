using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Pedido
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public int IdStatus { get; set; } = 1;
        public Estatus Estatus { get; set; } = new Estatus();
        [Column(TypeName = "DATETIME")]
        public DateTime Fecha { get; set; } = DateTime.Now;
        [Column(TypeName = "DATETIME")]
        public DateTime FechaEntrega { get; set; } = DateTime.Now + TimeSpan.FromDays(7);
        [StringLength(45)]
        public string Domicilio { get; set; } = string.Empty;
        [Phone, StringLength(10)]
        public string Telefono { get; set; } = string.Empty;
        [StringLength(45)]
        public string? RazonSocial { get; set; }
        [StringLength(13)]
        public string? Rfc { get; set; }
        [StringLength(4)]
        public char TipoPago { get; set; } = ' ';
        [StringLength(4)]
        public char TipoEnvio { get; set; } = ' ';
        [StringLength(4)]
        public char TipoPedido { get; set; } = ' '; // 'C' = Compra, 'V' = Venta || 'C' = Cliente, 'P' = Proveedor
        [Column(TypeName = "decimal(18,4)")]
        public decimal Total { get; set; } = 0;
        public ICollection<DetallePedido> DetallesPedidos { get; set; } = new List<DetallePedido>();
        public Venta Venta { get; set; } = new Venta();
        public Compra Compra { get; set; } = new Compra();
    }
}