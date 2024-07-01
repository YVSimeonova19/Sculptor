using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.Timetable;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Contracts;

public interface ITimetableService
{
    Task GenerateTimetableAsync();

    Task<List<TimetableVM>> GetAllTimetablesAsync();
}
