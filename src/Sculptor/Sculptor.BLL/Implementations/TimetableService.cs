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
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
    }

    // Update the schedule asyncronously
    public async Task<TimetableVM> EditTimetableAsync(int orderId, OrderUM orderUM)
    {
        //Retrieve the orde from the DB
        var order = await dbContext.Orders
            .Where(o => o.Id == orderId)
            .FirstAsync();

        // Update the order status
        if (orderUM.IsDelivered != null)
            order.IsDelivered = (bool)orderUM.IsDelivered;

        await this.dbContext.SaveChangesAsync();

        return this.mapper.Map<TimetableVM>(order);
    }

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
