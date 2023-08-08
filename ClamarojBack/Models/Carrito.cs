using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Carrito
    {
        [Key]
        public int IdCarrito { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public Cliente? Cliente { get; set; }
        public Producto? Producto { get; set; }
        public int Cantidad { get; set; } = 0;
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public DateTime FechaModificacion { get; set; } = DateTime.Now;
    }
}
