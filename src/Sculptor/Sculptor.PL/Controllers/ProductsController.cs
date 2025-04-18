﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Product;
using Sculptor.Common.Utilities;

namespace Sculptor.PL.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    // Add dependency injections
    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    // Get a list of all products
    [HttpGet]
    [Authorize(Roles = "Admin,Retailer")]
    public async Task<ActionResult<List<ProductVM>>> GetAllProductsAsync()
    {
        return this.Ok(await productService.GetAllProductsAsync());
    }

    // Create a new product
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<Response>> CreateProductAsync([FromBody] ProductIM productIM)
    {
        await this.productService.CreateProductAsync(productIM);
        return this.Ok(
            new Response
            {
                Status = "Product created successfully",
                Message = "This product has been created successfully!"
            });
    }
}
