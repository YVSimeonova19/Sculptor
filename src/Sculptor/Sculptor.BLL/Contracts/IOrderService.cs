using Sculptor.Common.Models.Order;

namespace Sculptor.BLL.Contracts;

public interface IOrderService
{
    // Add a new order to the DB asyncronously
    Task CreateOrderAsync(OrderIM orderIM);

    // Get the information of an order by its id asyncronously
    Task<OrderVM> GetOrderInfoByIdAsync(int orderId);

    // Update an orders information asyncronously
    Task<OrderVM> UpdateOrderAsync(int id, OrderUM orderUM);

    // Delete an order asyncronously
    Task DeleteOrderAsync(int id);

    // Check if an order exists by its id
    Task<bool> CheckIfOrderExistsById(int id);
}
