using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Sculptor.BLL.Contracts;
using Sculptor.Common.Models.Product;
using Sculptor.DAL.Data;
using Sculptor.DAL.Models;

namespace Sculptor.BLL.Implementations;

internal class ProductService : IProductService
{
    private readonly SculptorDbContext dbContext;
    private readonly IMapper mapper;

    public ProductService(SculptorDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    // Add a product to the DB asyncronously
    public async Task CreateProductAsync(ProductIM productIM)
    {
        var product = this.mapper.Map<Product>(productIM);

        this.dbContext.Add(product);

        await this.dbContext.SaveChangesAsync();
    }

    // Get a list of all products asyncronously
    public async Task<List<ProductVM>> GetAllProductsAsync()
    {
        return await this.dbContext.Products.ProjectTo<ProductVM>(this.mapper.ConfigurationProvider).ToListAsync();
    }

    // Get a product by its id asyncronously
    public async Task<ProductVM?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.Where(p => p.Id == id).ProjectTo<ProductVM>(this.mapper.ConfigurationProvider).FirstOrDefaultAsync();
    }
}
