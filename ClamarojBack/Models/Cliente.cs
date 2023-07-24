using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        [MaxLength(45)]
        public string Direccion { get; set; } = string.Empty;
        [Phone, MaxLength(13)]
        public string Telefono { get; set; } = string.Empty;

        [MaxLength(13)]
        public string Rfc { get; set; } = string.Empty;
    }
}