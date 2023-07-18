using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Receta
    {
        [Key]
        public int IdReceta { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int IdStatus { get; set; } = 1;
        //TODO: Completar las recetas
        // public int IdInsumo { get; set; }
        // public Insumo Insumo { get; set; } = new Insumo();
        // [DataType(DataType.Currency)]
        // public decimal Precio { get; set; } = 0;

    }
}