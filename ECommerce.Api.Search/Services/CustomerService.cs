using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IHttpClientFactory httpClientFactory, ILogger<CustomerService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<Result<Customer>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("CustomersService");
                var response = await client.GetAsync($"api/customer/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Result<Customer>>(content, options);

                    return result;
                }
                return new Result<Customer>() { IsSuccess = false, ResultObject = null, ErrorMessage = response.ReasonPhrase };
            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return new Result<Customer>() { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
