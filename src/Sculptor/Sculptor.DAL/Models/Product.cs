using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sculptor.DAL.Models;

public class Product
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string EAN { get; set; } = string.Empty;

    [Required]
    public int OrderId { get; set; }

    [Required]
    public Order Order { get; set; } = null!;
}
