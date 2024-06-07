using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Order;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;

    // Add dependency injections
    public OrdersController(IOrderService orderService, ICurrentUser currentUser)
    {
        this.orderService = orderService;
    }

    // Place an order asyncronously (Only available for retailers and admins)
    [HttpPost]
    [Authorize(Roles = "Retailer,Admin")]
    public async Task<ActionResult<Response>> PlaceOrderAsync([FromBody] OrderIM orderIM)
    {
        // Create the order
        await orderService.CreateOrderAsync(orderIM);

        // Confirm the order creation
        return this.Ok(
            new Response
            {
                Status = "Order placed successfully",
                Message = "The order has been placed successfully!"
            });
    }

    // Delete an order asyncronously (Only available for admins)
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response>> DeleteOrderAsync(int id)
    {
        if (!await orderService.CheckIfOrderExistsById(id))
            return NotFound();

        // Delete the order
        await orderService.DeleteOrderAsync(id);

        // Confirm the order deletion
        return this.Ok(
            new Response
            {
                Status = "Order deleted successfully",
                Message = "The order has been deleted successfully!"
            });
    }
}