using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClamarojBack.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [StringLength(10)]
        public string Codigo { get; set; } = string.Empty;
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(120)]
        public string Descripcion { get; set; } = string.Empty;
        [Column(TypeName = "decimal(18,4)")]
        public decimal Precio { get; set; } = 0;
        public byte[] Foto { get; set; } = Array.Empty<byte>();
        [Column(TypeName = "decimal(18,4)")]
        public decimal Merma { get; set; } = 0;
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
        public ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();
    }
}