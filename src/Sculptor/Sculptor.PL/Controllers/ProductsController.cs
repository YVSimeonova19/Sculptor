using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Product;

namespace Sculptor.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Retailer")]
    public async Task<ActionResult<List<ProductVM>>> GetAllProductsAsync()
    {
        return this.Ok(await productService.GetAllProductsAsync());
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> CreateProductAsync([FromBody] ProductIM productIM)
    {
        await this.productService.CreateProductAsync(productIM);
        return this.Ok();
    }
}
