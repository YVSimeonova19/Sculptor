using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.Timetable;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Implementations;

internal class TimetableService : ITimetableService
{
    private readonly IMapper mapper;
    private readonly SculptorDbContext dbContext;

    // Add dependency injections
    public TimetableService(IMapper mapper, SculptorDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    // Add a new item to the schedule asyncronously
    public async Task AddNewItemAsync(Order order)
    {
        // TODO
        throw new NotImplementedException();
    }

    // Update the schedule asyncronously
    public async Task<TimetableVM> EditTimetableAsync(int orderId, TimetableUM timetableUM)
    {
        var timetable = await this.dbContext.Orders
            .Where(o => o.Id == orderId)
            .Select(o => o.Timetable)
            .FirstAsync();

        if (timetableUM.DeliveryDateTime != null)
            timetable.DeliveryDateTime = (DateTime)timetableUM.DeliveryDateTime;

        this.dbContext.Timetables.Update(timetable);
        await this.dbContext.SaveChangesAsync();

        return this.mapper.Map<TimetableVM>(timetable);
    }

    // TODO: Update when done with the AddNewItemAsync method
    // Return schedule information asyncronously
    public async Task<TimetableVM> ViewDailyTimetableAsync()
    {
        var orders = await dbContext.Orders
            .Where(o => o.IsDelivered != true)
            .OrderBy(o => o.ClientInfo.ClientArea)
            .ThenBy(o => o.PlacedAt)
            .Take(40)
            .ProjectTo<OrderVM>(mapper.ConfigurationProvider)
            .ToListAsync();

        return new TimetableVM { Orders = orders };
    }
}
