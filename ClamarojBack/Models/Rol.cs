using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        public ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
        [StringLength(120)]
        public string Descripcion { get; set; } = string.Empty;
    }
}

