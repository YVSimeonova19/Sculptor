using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Timetable;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers;

public class TimetablesController : ControllerBase
{
    private readonly ITimetableService timetableService;
    private readonly IOrderService orderService;

    // Add depndency injecions
    public TimetablesController(ITimetableService timetableService, IOrderService orderService)
    {
        this.timetableService = timetableService;
        this.orderService = orderService;
    }

    // Return schedue information asyncronously
    [HttpGet("GetDailySchedule")]
    [Authorize(Roles = "Deliverer,Admin")]
    public async Task<ActionResult<TimetableVM>> DisplaySchedule()
    {
        return await timetableService.ViewDailyTimetableAsync();
    }

    // Edit order delivery datetime asyncronously
    [HttpPatch("{orderId}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<TimetableVM>> EditTimetable([FromBody] TimetableUM timetableUM, int orderId)
    {
        if (!this.orderService.CheckIfOrderExistsById(orderId))
            return NotFound();

        return await timetableService.EditTimetableAsync(orderId, timetableUM);
    }
}
