using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Insumo
    {
        [Key]
        public int IdInsumo { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
        public int IdProveedor { get; set; }
        // TODO: Completar los insumos
        // public Proveedor Proveedor { get; set; } = new Proveedor();
        // [DataType(DataType.Currency)]
        // public decimal Precio { get; set; } = 0;
        // public int Stock { get; set; } = 0;
        // public int CantMinima { get; set; } = 0;
        // public int IdUnidadMedida { get; set; }
        // public UnidadMedida UnidadMedida { get; set; } = new UnidadMedida();
    }
}