using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; } = string.Empty;
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(120)]
        public string Descripcion { get; set; } = string.Empty;
        public int Perecedero { get; set; } = 0; // 0 = No, 1 = Si, || Dias de caducidad
        public int Stock { get; set; } = 0;
        public int CantMinima { get; set; } = 0;
        public int CantMaxima { get; set; } = 0;
        public int IdUnidadMedida { get; set; }
        public UnidadMedida UnidadMedida { get; set; } = new UnidadMedida();
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; } = 0;
        [Column(TypeName = "TEXT")]
        public string Foto { get; set; } = string.Empty;
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; } = new Proveedor();
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public ICollection<Ingrediente> Ingredientes { get; set; } = new List<Ingrediente>();
        //public ICollection<Receta> Recetas { get; set; } = new List<Receta>();
    }
}