using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sculptor.DAL.Models;

public class Timetable
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateTime DeliveryDateTime { get; set; }

    [Required]
    public ICollection<Order> Orders { get; } = new List<Order>();
}
