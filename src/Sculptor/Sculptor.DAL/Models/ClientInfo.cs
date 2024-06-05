using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sculptor.DAL.Models;

public class ClientInfo
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string ClientFirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ClientLastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ClientEmail { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ClientAddress { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string ClientArea { get; set; } = string.Empty;

    [Required]
    public int OrderId { get; set; }

    [Required]
    public Order Order { get; set; } = null!;
}
