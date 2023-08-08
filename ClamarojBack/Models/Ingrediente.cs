using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Ingrediente
    {
        //[Key]
        //public int Id { get; set; }
        public int IdReceta { get; set; }
        public int IdMateriaPrima { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; } = 0;
        public int IdStatus { get; set; }
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public Receta? Receta { get; set; }
        public MateriaPrima? MateriaPrima { get; set; }
    }
}
