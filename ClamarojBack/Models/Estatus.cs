using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Estatus
    {
        [Key]
        public int Id { get; set; }
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
    }
}