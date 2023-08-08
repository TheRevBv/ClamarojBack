namespace ClamarojBack.Models
{
    public class Carrito
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<Producto> Productos { get; set; } = new List<Producto>();
    }
}
