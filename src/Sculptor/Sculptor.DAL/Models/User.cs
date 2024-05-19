using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Sculptor.DAL.Models;

public class User : IdentityUser
{
    [Required]
    [MaxLength(30)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(30)]
    public string LastName { get; set; } = string.Empty;
}
