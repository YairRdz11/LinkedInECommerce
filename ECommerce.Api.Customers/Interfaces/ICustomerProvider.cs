using ECommerce.Api.Customers.Models;

namespace ECommerce.Api.Customers.Interfaces
{
    public interface ICustomerProvider
    {
        Task<Result<IEnumerable<Customer>>> GetCustomersAsync();
        Task<Result<Customer>> GetCustomerByIdAsync(int id);
    }
}
