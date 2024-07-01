using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Order;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;
using System.Runtime.InteropServices;
using Sculptor.Common.Models.Product;

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

        order.PlacedAt = DateTime.Now;

        // Add the order to the orders dbSet
        this.dbContext.Orders.Add(order);

        await this.dbContext.SaveChangesAsync();

        var orderId = this.dbContext.Orders.Where(o => o.PlacedAt == order.PlacedAt).First().Id;

        foreach(var productId in orderIM.ProductsIds)
        {
            var productOrder = new ProductOrder() { 
                ProductId = productId,
                OrderId = orderId
            };

            await this.dbContext.AddAsync(productOrder);
        }

        await this.dbContext.SaveChangesAsync();
    }

    // Check if an order exists by its id
    public bool CheckIfOrderExistsById(int id)
    {
        return this.dbContext.Orders.Where(o => o.Id == id).Count() != 0;
    }

    // Delete an order asyncronously
    public async Task DeleteOrderAsync(int id)
    {
        // Get the order
        var order = await this.dbContext.Orders.FindAsync(id);

        // Delete the order from the database
        if (order != null)
        {
            this.dbContext.Remove(order);
        }

        await this.dbContext.SaveChangesAsync();
    }

    // Get the information of an order by its id asyncronously
    public async Task<OrderVM> GetOrderInfoByIdAsync(int orderId)
    {
        var order = await dbContext.Orders
            .Where(o => o.Id == orderId)
            .ProjectTo<OrderVM>(this.mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        var clientInfo = await this.dbContext.ClientInfo
            .FirstOrDefaultAsync(ci => ci.OrderId == orderId);

        order.ClientFirstName = clientInfo.FirstName;
        order.ClientLastName = clientInfo.LastName;
        order.ClientEmail = clientInfo.Email;
        order.ClientAddress = clientInfo.Address;
        order.ClientArea = clientInfo.Area;

        var products = await this.dbContext.ProductsOrders
            .Where(po => po.OrderId == orderId)
            .Select(po => po.Product)
            .ProjectTo<ProductVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();

        order.Products = products;

        return order;
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

        return await this.GetOrderInfoByIdAsync(id);
    }
}
