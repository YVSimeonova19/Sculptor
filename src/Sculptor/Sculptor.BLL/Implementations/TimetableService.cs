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

    public async Task GenerateTimetableAsync()
    {
        await this.dbContext.Timetables
            .Where(t => t.DeliveryDateTime > DateTime.Today.AddDays(1))
            .ExecuteDeleteAsync();

        var orders = await this.dbContext.Orders
            .Where(o => o.IsDelivered == false)
            .OrderBy(o => o.PlacedAt)
            .Include(o => o.ClientInfo)
            .ToListAsync();

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

        TimeOnly prevDeliveryTime = new TimeOnly(8, 0);

        for (int i = 0; i < sortedOrders.Count; i++)
        {
            var date = DateOnly.FromDateTime(DateTime.Now).AddDays((int)Math.Floor((double)i / 40) + 1);
            TimeOnly time;

            if (i == 0)
            {
                time = prevDeliveryTime;
            }
            else
            {
                if (sortedOrders[i].ClientInfo.Area == sortedOrders[i - 1].ClientInfo.Area)
                {
                    time = prevDeliveryTime.AddMinutes(10);
                }
                else
                {
                    time = new TimeOnly(prevDeliveryTime.Hour + 1, 0);
                }
            }

            prevDeliveryTime = time;

            var timetable = new Timetable() { DeliveryDateTime = date.ToDateTime(time), OrderId = sortedOrders[i].Id };

            this.dbContext.Timetables.Add(timetable);
        }

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<List<TimetableVM>> GetAllTimetablesAsync()
    {
        return await this.dbContext.Timetables
            .Include(t => t.Order)
            .ProjectTo<TimetableVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
