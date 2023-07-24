using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class MateriaPrima
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Codigo { get; set; } = string.Empty;
        [MaxLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [MaxLength(100)]
        public string Descripcion { get; set; } = string.Empty;
        public int Perecedero { get; set; } = 0; // 0 = No, 1 = Si, || Dias de caducidad
        public int Stock { get; set; } = 0;
        public int CantMinima { get; set; } = 0;
        public int CantMaxima { get; set; } = 0;
        public int IdUnidadMedida { get; set; }
        public UnidadMedida UnidadMedida { get; set; } = new UnidadMedida();
        [DataType(DataType.Currency)]
        public decimal Precio { get; set; } = 0;
        public byte[] Foto { get; set; } = new byte[0];
        public int IdProveedor { get; set; }
        public Proveedor Proveedor { get; set; } = new Proveedor();
        public int IdStatus { get; set; } = 1;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}