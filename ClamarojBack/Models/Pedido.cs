using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Pedido
    {
        [Key]
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public int IdStatus { get; set; } = 1;
        public Estatus Estatus { get; set; } = new Estatus();
        public DateTime Fecha { get; set; } = DateTime.Now;
        [MaxLength(45)]
        public string Domicilio { get; set; } = string.Empty;
        [Phone, MaxLength(13)]
        public string Telefono { get; set; } = string.Empty;
        [MaxLength(45)]
        public string? RazonSocial { get; set; }
        [MaxLength(13)]
        public string? Rfc { get; set; }
        [MaxLength(3)]
        public char TipoPago { get; set; } = ' ';
        [MaxLength(3)]
        public char TipoEnvio { get; set; } = ' ';
        [MaxLength(3)]
        public char TipoPedido { get; set; } = ' '; // 'C' = Compra, 'V' = Venta || 'C' = Cliente, 'P' = Proveedor
        [DataType(DataType.Currency)]
        public decimal Total { get; set; } = 0;
    }
}