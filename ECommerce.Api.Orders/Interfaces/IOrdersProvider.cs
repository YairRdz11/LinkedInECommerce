using ECommerce.Api.Orders.Models;

namespace ECommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<Result<IEnumerable<Order>>> GetOrdersAsync(int customerId);
    }
}
