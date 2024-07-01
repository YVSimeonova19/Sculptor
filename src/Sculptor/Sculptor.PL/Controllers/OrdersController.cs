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
    private readonly IProductService productService;

    // Add dependency injections
    public OrdersController(IOrderService orderService, IProductService productService)
    {
        this.orderService = orderService;
        this.productService = productService;
    }

    // Place an order (Only available for retailers and admins)
    [HttpPost]
    [Authorize(Roles = "Retailer,Admin")]
    public async Task<ActionResult<Response>> PlaceOrderAsync([FromBody] OrderIM orderIM)
    {
        foreach(var productId in orderIM.ProductsIds)
        {
            var product = await this.productService.GetProductByIdAsync(productId);

            if(product is null)
            {
                return BadRequest();
            }
        }

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

    // Get the information of an order by its id (Only available for admins)
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<OrderVM>> GetOrderInfo(int id)
    {
        if (!orderService.CheckIfOrderExistsById(id))
            return NotFound(
                new Response
                {
                    Status = "Order does not exist",
                    Message = "An order with this id does not exist!"
                });

        return await this.orderService.GetOrderInfoByIdAsync(id);
    }

    // Update the delivery status of an order (Available for deliverers and admins)
    [HttpPatch("{id}")]
    [Authorize(Roles = "Deliverer,Admin")]
    public async Task<ActionResult<OrderVM>> UpdateOrderStatus([FromBody] OrderUM orderUM, int id)
    {
        return await this.orderService.UpdateOrderAsync(id, orderUM);
    }

    // Delete an order (Only available for admins)
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response>> DeleteOrderAsync(int id)
    {
        if (!orderService.CheckIfOrderExistsById(id))
            return NotFound(
                new Response
                {
                    Status = "Order does not exist",
                    Message = "An order with this id does not exist!"
                });

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