using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [MaxLength(10)]
        public string Codigo { get; set; } = string.Empty;
        [MaxLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(45)]
        public string Descripcion { get; set; } = string.Empty;
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; } = 0;
        public byte[] Foto { get; set; } = new byte[0];
        public decimal Merma { get; set; } = 0;
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}