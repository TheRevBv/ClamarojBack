namespace ClamarojBack.Dtos
{
    public class ClientesDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public int IdStatus { get; set; }
    }

    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Rfc { get; set; } = string.Empty;
        // public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    }

}