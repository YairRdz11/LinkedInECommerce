using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<Result<IEnumerable<Product>>> GetProductsAsync();
        Task<Result<Product>> GetProductByIdAsync(int id);
    }
}
