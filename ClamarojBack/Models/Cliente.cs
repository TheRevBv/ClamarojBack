using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        [Phone]
        public string Telefono { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;
        [Required, MinLength(8), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
    }
}