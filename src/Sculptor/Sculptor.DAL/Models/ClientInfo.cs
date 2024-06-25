using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sculptor.DAL.Models;

public class ClientInfo
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Area { get; set; } = string.Empty;

    [Required]
    public int OrderId { get; set; }

    [Required]
    public Order Order { get; set; } = null!;
}
