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
        [StringLength(45)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(45)]
        public string Apellido { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Correo { get; set; } = string.Empty;
        [Required, MinLength(8), RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$"), DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        //Imagen de perfil
        public byte[] Foto { get; set; } = Array.Empty<byte>();
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; } = Convert.ToDateTime("01/01/1900");
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public int IdStatus { get; set; } = 1;
        public ICollection<RolUsuario> RolesUsuario { get; set; } = new List<RolUsuario>();
        public Proveedor? Proveedor { get; set; }
        public Cliente? Cliente { get; set; }
        public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();        
    }
}

