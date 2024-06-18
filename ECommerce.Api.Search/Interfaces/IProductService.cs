using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IProductService
    {
        Task<Result<IEnumerable<Product>>> GetProductAsync();
    }
}
