using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(45)]
        public string Nombre { get; set; } = string.Empty;
        public ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
        [MaxLength(120)]
        public string Descripcion { get; set; } = string.Empty;
    }
}

