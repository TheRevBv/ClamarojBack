namespace ClamarojBack.Models.Responses
{
    public class IClientesRes
    {
        public int IdCliente { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        // public string Password { get; set; }
        public byte[]? Foto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? IdStatus { get; set; }
        // public List<int> Roles { get; set; }
        public List<IRolRes>? Roles { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Rfc { get; set; }
        // public string? Status { get; set; }
    }
}
