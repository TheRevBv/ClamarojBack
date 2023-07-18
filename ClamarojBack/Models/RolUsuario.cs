using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class RolUsuario
    {
        [Key]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public Rol Rol { get; set; } = new Rol();
    }
}

