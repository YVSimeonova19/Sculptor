using System.ComponentModel.DataAnnotations;

namespace Sculptor.Common.Models.Login;

public class LoginIM
{
    [Required]
    public string Username { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
