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
    public ICollection<Product> Products { get; } = new List<Product>();

    [Required]
    public int TimetableId { get; set; }

    [Required]
    public Timetable Timetable { get; set; } = null!;
}
