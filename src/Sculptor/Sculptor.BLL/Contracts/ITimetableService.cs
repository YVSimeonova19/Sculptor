using Sculptor.Common.Models.Timetable;

namespace Sculptor.BLL.Contracts;

public interface ITimetableService
{
    // Generate a timetable with the current orders asyncronously
    Task GenerateTimetableAsync();

    // Get the whole schedule asyncronously
    Task<List<TimetableVM>> GetAllTimetablesAsync();
}
