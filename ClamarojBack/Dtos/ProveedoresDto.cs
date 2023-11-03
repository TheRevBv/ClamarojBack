namespace ClamarojBack.Dtos
{
    public class ProveedoresDto
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int IdStatus { get; set; }
    }

    public class ProveedorDto
    {
        public int IdProveedor { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string Rfc { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public int IdStatus { get; set; }
        public UsuarioDto Usuario { get; set; } = new UsuarioDto();
    }

    public class ProviderDto
    {
        public int IdProveedor { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Rfc { get; set; }
        public string? RazonSocial { get; set; }
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? Foto { get; set; }
        public int IdStatus { get; set; }
        public int IdUsuario { get; set; }
    }

}