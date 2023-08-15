namespace ClamarojBack.Models
{
    public class RolUsuario
    {
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public Usuario Usuario { get; set; } = new Usuario();
        public Rol Rol { get; set; } = new Rol();
    }
}

