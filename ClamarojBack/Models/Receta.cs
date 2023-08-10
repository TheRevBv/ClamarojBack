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
        [Column(TypeName = "decimal(18,2)")]
        public decimal Costo { get; set; } = 0;
        [Column(TypeName = "decimal(18,4)")]
        public decimal Cantidad { get; set; } = 0; //Cantidad producto a producir
        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = new Producto();
        public int IdStatus { get; set; } = 1;
        [Column(TypeName = "DATETIME")]
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        [Column(TypeName = "DATETIME")]
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    }
}