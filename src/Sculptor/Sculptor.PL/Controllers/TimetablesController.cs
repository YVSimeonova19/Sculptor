using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Timetable;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers;

public class TimetablesController : ControllerBase
{
    private readonly ITimetableService timetableService;

    // Add depndency injecions
    public TimetablesController(ITimetableService timetableService)
    {
        this.timetableService = timetableService;
    }

    // Return schedue information asyncronously
    [HttpGet("GetDailySchedule")]
    [Authorize(Roles = "Deliverer,Admin")]
    public async Task<ActionResult<TimetableVM>> DisplaySchedule()
    {
        return await timetableService.ViewDailyTimetableAsync();
    }


}
