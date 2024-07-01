using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Order;
using Sculptor.Common.Models.Timetable;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;
using System;

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

    // Generate a timetable with the current orders asyncronously
    public async Task GenerateTimetableAsync()
    {
        // Delete timetables for days after the current one
        await this.dbContext.Timetables
            .Where(t => t.DeliveryDateTime > DateTime.Today.AddDays(1))
            .ExecuteDeleteAsync();

        // Get the orders that have not been delivered
        var orders = await this.dbContext.Orders
            .Where(o => o.IsDelivered == false)
            .OrderBy(o => o.PlacedAt)
            .Include(o => o.ClientInfo)
            .ToListAsync();

        // Create a dictionary to store the first occurence index of each area
        Dictionary<string, int> areaIndex = new Dictionary<string, int>();

        for (int i = 0; i < orders.Count; i++)
        {
            if (!areaIndex.ContainsKey(orders[i].ClientInfo.Area))
            {
                areaIndex[orders[i].ClientInfo.Area] = i;
            }
        }

        // Sort the orders based on the first occurrence index of their area
        List<Order> sortedOrders = orders.OrderBy(o => areaIndex[o.ClientInfo.Area]).ToList();

        // Set first delivery time for the day
        TimeOnly prevDeliveryTime = new TimeOnly(8, 0);

        for (int i = 0; i < sortedOrders.Count; i++)
        {
            // Set the delivery date for the first 40 orders
            var date = DateOnly.FromDateTime(DateTime.Now).AddDays((int)Math.Floor((double)i / 40) + 1);
            TimeOnly time;

            if (i == 0)
            { // If this is the first delivery of the day
                time = prevDeliveryTime;
            }
            else
            {
                if (sortedOrders[i].ClientInfo.Area == sortedOrders[i - 1].ClientInfo.Area)
                { // Add 10 minutes to each of the next orders
                    time = prevDeliveryTime.AddMinutes(10);
                }
                else
                { // Set the time to the next round hour for orders from a different area
                    time = new TimeOnly(prevDeliveryTime.Hour + 1, 0);
                }
            }

            // Set the previous delivery time
            prevDeliveryTime = time;

            // Create the timetable with the dates and times of each delivery
            var timetable = new Timetable() { DeliveryDateTime = date.ToDateTime(time), OrderId = sortedOrders[i].Id };

            this.dbContext.Timetables.Add(timetable);
        }

        await this.dbContext.SaveChangesAsync();
    }

    // Get the whole schedule asyncronously
    public async Task<List<TimetableVM>> GetAllTimetablesAsync()
    {
        return await this.dbContext.Timetables
            .Include(t => t.Order)
            .ProjectTo<TimetableVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
