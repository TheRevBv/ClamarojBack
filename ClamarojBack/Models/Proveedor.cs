using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        [StringLength(45)]
        public string Direccion { get; set; } = string.Empty;
        [Phone, StringLength(10)]
        public string Telefono { get; set; } = string.Empty;
        [StringLength(13)]
        public string Rfc { get; set; } = string.Empty;
        [StringLength(45)]
        public string RazonSocial { get; set; } = string.Empty;
        public ICollection<Compra> Compras { get; set; } = new List<Compra>();
    }
}