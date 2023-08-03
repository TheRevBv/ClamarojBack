public class UsuariosDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Correo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? Foto { get; set; }
    public string? Estatus { get; set; }
    public int IdStatus { get; set; }
    public ICollection<RolDto> Roles { get; set; } = new List<RolDto>();
}

public class RolDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}

public class UsuarioDto
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
    public string? Apellido { get; set; }
    public string? Correo { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? Foto { get; set; }
    public int IdStatus { get; set; }
    public ICollection<RolDto> Roles { get; set; } = new List<RolDto>();
    // public ProveedorDto? Proveedor { get; set; }
    // public ClienteDto? Cliente { get; set; }
}

public class ClienteDto
{
    public int IdCliente { get; set; }

}

public class ProveedorDto
{
}