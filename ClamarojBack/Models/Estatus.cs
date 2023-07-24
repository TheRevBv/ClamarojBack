using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Estatus
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(45)]
        public string Nombre { get; set; } = string.Empty;
    }
}