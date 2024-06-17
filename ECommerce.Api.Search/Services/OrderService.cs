using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class OrderService : IOrderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IHttpClientFactory httpClientFactory, ILogger<OrderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;   
        }

        public async Task<Result<IEnumerable<Order>>> GetOrderAsync(int customerId)
        {
            try
            {
                var client = _httpClientFactory.CreateClient("OrdersServices");
                var response = await client.GetAsync($"api/orders/{customerId}");
                if (response.IsSuccessStatusCode) 
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var option = new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var result = JsonSerializer.Deserialize<Result<IEnumerable<Order>>>(content, option);

                    return result;
                }
                return new Result<IEnumerable<Models.Order>>() { IsSuccess = false, ResultObject = null, ErrorMessage = response.ReasonPhrase };

            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());

                return new Result<IEnumerable<Models.Order>>() { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
