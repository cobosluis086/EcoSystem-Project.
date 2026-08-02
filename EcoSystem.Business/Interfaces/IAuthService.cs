using EcoSystem.Business.DTOs.Auth;

namespace EcoSystem.Business.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> LoginAsync(LoginRequest request);
}