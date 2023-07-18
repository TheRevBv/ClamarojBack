using System.Reflection.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClamarojBack.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        //Imagen en base64
        public string Foto { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; } = DateTime.Now;
        public int IdStatus { get; set; }
        public ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();

    }
}

