using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Receta
    {
        [Key]
        public int IdReceta { get; set; }
        [MaxLength(10)]
        public string Codigo { get; set; } = string.Empty;
        [MaxLength(45)]
        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = new Producto();
        public int IdMateriaPrima { get; set; }
        public MateriaPrima MateriaPrima { get; set; } = new MateriaPrima();
        public decimal Cantidad { get; set; } = 0;
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}