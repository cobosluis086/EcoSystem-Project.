using System.ComponentModel.DataAnnotations;

namespace EcoSystem.Data.Models;

public class Usuario
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres.")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido.")]
    [MaxLength(120, ErrorMessage = "El correo no puede exceder 120 caracteres.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    public string PasswordHash { get; set; } = string.Empty;

    [Required(ErrorMessage = "El rol es obligatorio.")]
    [MaxLength(20, ErrorMessage = "El rol no puede exceder 20 caracteres.")]
    public string Rol { get; set; } = "Usuario";

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
}