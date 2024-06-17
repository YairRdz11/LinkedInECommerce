using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface IOrderService
    {
        Task<Result<IEnumerable<Order>>> GetOrderAsync(int customerId);
    }
}
