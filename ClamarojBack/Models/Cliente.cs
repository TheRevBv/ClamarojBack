using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        [StringLength(45)]
        public string Direccion { get; set; } = string.Empty;
        [Phone, StringLength(10)]
        public string Telefono { get; set; } = string.Empty;
        [StringLength(13)]
        public string Rfc { get; set; } = string.Empty;
        public ICollection<Carrito> Carrito { get; set; } = new List<Carrito>();
        public ICollection<Venta> Ventas { get; set; } = new List<Venta>();
    }
}