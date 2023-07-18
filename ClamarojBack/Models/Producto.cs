using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public string Foto { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
        //TODO: Completar los productos
        // public int IdProveedor { get; set; }
        // public Proveedor Proveedor { get; set; } = new Proveedor();
        // public int IdUnidadMedida { get; set; }
        // public UnidadMedida UnidadMedida { get; set; } = new UnidadMedida();
        // public int IdReceta { get; set; }
        // public Receta Receta { get; set; } = new Receta();
        // public int Stock { get; set; } = 0;
        // public int CantMinima { get; set; } = 0;
        // public int CantMaxima { get; set; } = 0;

    }
}