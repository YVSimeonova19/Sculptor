using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Sculptor.DAL.Models;

public class Order
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public bool IsDelivered { get; set; }

    [Required]
    public DateTime PlacedAt { get; set; }

    [Required]
    public List<Product> Products { get; set; }

    [Required]
    public ClientInfo ClientInfo { get; set; }
}
