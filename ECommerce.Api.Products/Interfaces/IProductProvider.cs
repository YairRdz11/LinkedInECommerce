using ECommerce.Api.Products.Models;

namespace ECommerce.Api.Products.Interfaces
{
    public interface IProductProvider
    {
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
    }
}
