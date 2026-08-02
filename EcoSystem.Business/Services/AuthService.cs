using EcoSystem.Business.DTOs.Auth;
using EcoSystem.Business.Interfaces;
using EcoSystem.Data.Configuration;
using EcoSystem.Data.Data;
using EcoSystem.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcoSystem.Business.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        AppDbContext context,
        IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (usuario is null)
        {
            return null;
        }

        // Comparación directa para fines del laboratorio.
        // Posteriormente puede reemplazarse por BCrypt.
        if (usuario.PasswordHash != request.Password)
        {
            return null;
        }

        var expiration = DateTime.UtcNow.AddMinutes(
            _jwtSettings.ExpirationMinutes);

        var token = GenerarToken(usuario, expiration);

        return new LoginResponse
        {
            Token = token,
            Nombre = usuario.Nombre,
            Rol = usuario.Rol,
            Expiration = expiration
        };
    }

    private string GenerarToken(
        Usuario usuario,
        DateTime expiration)
    {
        var claims = new List<Claim>
        {
            new(
                JwtRegisteredClaimNames.Sub,
                usuario.Id.ToString()),

            new(
                JwtRegisteredClaimNames.Email,
                usuario.Email),

            new(
                JwtRegisteredClaimNames.UniqueName,
                usuario.Nombre),

            new(
                JwtRegisteredClaimNames.Jti,
                Guid.NewGuid().ToString()),

            new(
                ClaimTypes.Role,
                usuario.Rol),

            new(
                JwtRegisteredClaimNames.Iat,
                DateTimeOffset.UtcNow
                    .ToUnixTimeSeconds()
                    .ToString(),
                ClaimValueTypes.Integer64)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtSettings.SecretKey));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expiration,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}