namespace ClamarojBack.Models.Requests;

public class AuthRequest
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}