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

    public async Task DeleteOrderAsync(int id)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // TODO: Delete the order from the database
        

        await this.dbContext.SaveChangesAsync();
    }

    public async Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // Update status if changed
        if (orderUM.IsDelivered != null)
            order.IsDelivered = (bool)orderUM.IsDelivered;

        this.dbContext.Orders.Update(order);
        await this.dbContext.SaveChangesAsync();

        return this.mapper.Map<OrderVM>(order);
    }
}
