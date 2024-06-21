using ECommerce.Api.Search.Models;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        Task<Result<Customer>> GetCustomerByIdAsync(int id);
    }
}
