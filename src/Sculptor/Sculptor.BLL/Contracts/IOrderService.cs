using Sculptor.Common.Models.Order;

namespace Sculptor.BLL.Contracts;

public interface IOrderService
{
    // Add a new order to the DB asyncronously
    Task CreateOrderAsync(OrderIM orderIM);

    // Update an orders information asyncronously
    Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM);

    // Delete an order asyncronously
    Task DeleteOrderAsync(int id);
}
