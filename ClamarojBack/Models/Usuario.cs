using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(45)]
        public string Apellido { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;
        [Required, MinLength(8), Column(TypeName = "VARBINARY(MAX)")]
        public byte[] Password { get; set; } = Array.Empty<byte>();
        //Imagen de perfil
        [Column(TypeName = "TEXT")]
        public string Foto { get; set; } = string.Empty;
        [Column(TypeName = "DATETIME")]
        public DateTime FechaNacimiento { get; set; } = Convert.ToDateTime("01/01/2000");
        [Column(TypeName = "DATETIME")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int IdStatus { get; set; } = 1;
        public ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
        public Proveedor? Proveedor { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}

