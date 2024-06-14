using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.Timetable;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Contracts;

public interface ITimetableService
{
    // Return schedule information asyncronously
    Task<TimetableVM> ViewDailyTimetableAsync();

    // Add a new item to the schedule asyncronously
    Task AddNewItemAsync(Order order);

    // Update the schedule asyncronously
    Task<TimetableVM> EditTimetableAsync(int orderId, OrderUM orderUM);
}
