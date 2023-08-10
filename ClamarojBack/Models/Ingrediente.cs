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
        public decimal Cantidad { get; set; } = 0; //Cantidad de materia prima
        public Receta Receta { get; set; } = new Receta();
        public MateriaPrima MateriaPrima { get; set; } = new MateriaPrima();
    }
}
