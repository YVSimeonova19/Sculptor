using Sculptor.Common.Models.Order;

namespace Sculptor.Common.Models.Timetable;

public class TimetableVM
{
    public DateTime DeliveryDateTime { get; set; }

    public OrderVM? Order { get; set; }
}
