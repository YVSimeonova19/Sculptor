using Sculptor.Common.Models.Timetable;

namespace Sculptor.BLL.Contracts;

public interface ITimetableService
{
    // Return schedule information asyncronously
    Task<TimetableVM> ViewDailyTimetableAsync();

    // Add a new item to the schedule asyncronously
    Task AddNewItemAsync();

    // Update the schedule asyncronously
    Task<TimetableVM> EditTimetableAsync();
}
