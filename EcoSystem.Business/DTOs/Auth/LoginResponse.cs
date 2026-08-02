namespace EcoSystem.Business.DTOs.Auth;

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;

    public string Nombre { get; set; } = string.Empty;

    public string Rol { get; set; } = string.Empty;

    public DateTime Expiration { get; set; }
}