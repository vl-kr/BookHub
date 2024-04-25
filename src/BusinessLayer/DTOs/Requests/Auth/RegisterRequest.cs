using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLayer.DTOs.Requests.Auth;

public class RegisterRequest
{
    [Required]
    [NotNull]
    [DataType(DataType.EmailAddress)]
    public string? Email { get; set; }

    [Required]
    [NotNull]
    [DataType(DataType.Text)]
    public string? UserName { get; set; }

    [Required]
    [NotNull]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}
