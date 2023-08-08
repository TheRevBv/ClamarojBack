using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

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
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
    }
}