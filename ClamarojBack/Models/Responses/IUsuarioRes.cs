namespace ClamarojBack.Models.Responses
{
    public class IUsuarioRes
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        // public string Password { get; set; }
        public string? Foto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdStatus { get; set; }
        public List<IRolRes>? Roles { get; set; }
        // public int[]? Roles { get; set; }
    }
}
