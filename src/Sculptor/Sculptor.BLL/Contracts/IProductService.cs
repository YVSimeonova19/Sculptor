using Sculptor.Common.Models.Product;

namespace Sculptor.BLL.Contracts;

public interface IProductService
{
    // Add a product to the DB asyncronously
    Task CreateProductAsync(ProductIM productIM);

    // Get a list of all products asyncronously
    Task<List<ProductVM>> GetAllProductsAsync();

    // Get a product by its id asyncronously
    Task<ProductVM?> GetProductByIdAsync(int id);
}
