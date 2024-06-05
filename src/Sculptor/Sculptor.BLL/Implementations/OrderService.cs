using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Order;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Implementations;

internal class OrderService : IOrderService
{
    private readonly IMapper mapper;
    private readonly SculptorDbContext dbContext;

    // Add dependency injections
    public OrderService(IMapper mapper, SculptorDbContext dbContext)
    {
        this.mapper = mapper;
        this.dbContext = dbContext;
    }

    // Add a new order to the DB asyncronously
    public async Task CreateOrderAsync(OrderIM orderIM)
    {
        // Create a new order
        var order = this.mapper.Map<Order>(orderIM);

        // Add the order to the orders dbSet
        this.dbContext.Orders.Add(order);

        await this.dbContext.SaveChangesAsync();
    }

    // Delete an order asyncronously
    public async Task DeleteOrderAsync(int id)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // Get client information
        var clientInfo = await this.dbContext.ClientInfo
            .Where(ci => ci.Id == order.ClientInfo.Id)
            .FirstAsync();

        // Delete the order from the database
        if (order != null)
        {
            this.dbContext.Remove(clientInfo);
            this.dbContext.Remove(order);
        }

        await this.dbContext.SaveChangesAsync();
    }

    // Get the information of an order by its id asyncronously
    public async Task<OrderVM> GetOrderInfoByIdAsync(int orderId)
    {
        return await dbContext.Orders
            .Where(o => o.Id == orderId)
            .ProjectTo<OrderVM>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }

    public async Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // Update status if changed
        if (orderUM.IsDelivered != null && order != null)
        {
            order.IsDelivered = (bool)orderUM.IsDelivered;

            this.dbContext.Orders.Update(order);
            await this.dbContext.SaveChangesAsync();
        }

        return this.mapper.Map<OrderVM>(order);
    }
}
