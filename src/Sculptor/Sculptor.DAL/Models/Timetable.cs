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
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }

    [Required]
    public int OrderId { get; set; }
}
