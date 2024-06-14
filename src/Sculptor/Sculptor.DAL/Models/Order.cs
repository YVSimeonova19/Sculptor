using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public ICollection<Product> Products { get; } = new List<Product>();

    [Required]
    public int TimetableId { get; set; }

    [Required]
    public Timetable Timetable { get; set; } = null!;

    [Required]
    public ClientInfo? ClientInfo { get; set; }
}
