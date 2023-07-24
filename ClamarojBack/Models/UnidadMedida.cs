using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class UnidadMedida
    {
        [Key]
        public int IdUnidadMedida { get; set; }
        [MaxLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(45)]
        public string Descripcion { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
    }
}