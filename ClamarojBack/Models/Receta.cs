using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Receta
    {
        [Key]
        public int IdReceta { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; } = string.Empty;        
        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = new Producto();
        public int IdMateriaPrima { get; set; }
        public MateriaPrima MateriaPrima { get; set; } = new MateriaPrima();
        [Column(TypeName = "decimal(10,2)")]
        public decimal Cantidad { get; set; } = 0;
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}