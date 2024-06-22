using Sculptor.Common.Models.Product;

namespace Sculptor.BLL.Contracts;

public interface IProductService
{
    Task CreateProductAsync(ProductIM productIM);

    Task<List<ProductVM>> GetAllProductsAsync();

    Task<ProductVM?> GetProductByIdAsync(int id);
}
