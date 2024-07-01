using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Timetable;
using Sculptor.Common.Utilities;
using System.Runtime.InteropServices;

namespace Sculptor.PL.Controllers;

[Route("api/[controller]")]
[ApiController]

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

    // Generate timetable
    [HttpPost("/generate")]
    [Authorize(Roles = "Deliverer,Admin")]
    public async Task<IActionResult> DisplaySchedule()
    {
        await this.timetableService.GenerateTimetableAsync();

        return Ok();
    }

    // Return schedule
    [HttpGet]
    [Authorize(Roles = "Deliverer,Admin")]
    public async Task<ActionResult<List<TimetableVM>>> GetAllTimetables()
    {
        var timetable = await this.timetableService.GetAllTimetablesAsync();

        foreach (var entry in timetable)
        {
            entry.Order = await this.orderService.GetOrderInfoByIdAsync(entry.Order.Id);
        }

        return timetable;
    }
}
