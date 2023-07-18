using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class UnidadMedida
    {
        [Key]
        public int IdUnidadMedida { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
    }
}