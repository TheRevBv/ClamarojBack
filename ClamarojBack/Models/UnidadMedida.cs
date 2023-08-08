using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class UnidadMedida
    {
        [Key]
        public int IdUnidadMedida { get; set; }
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(120)]
        public string Descripcion { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
    }
}