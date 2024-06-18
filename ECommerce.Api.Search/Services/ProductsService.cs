using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Models;
using System.Text.Json;

namespace ECommerce.Api.Search.Services
{
    public class ProductsService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        ILogger<ProductsService> _logger;
        public ProductsService(IHttpClientFactory httpClientFactory, ILogger<ProductsService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }
        public async Task<Result<IEnumerable<Product>>> GetProductAsync()
        {
            try
            {
                var client = _httpClientFactory.CreateClient("ProductsServices");
                var response = await client.GetAsync("api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result = JsonSerializer.Deserialize<Result<IEnumerable<Product>>>(content, options);

                    return result;
                }
                return new Result<IEnumerable<Models.Product>>() { IsSuccess = false, ResultObject = null, ErrorMessage = response.ReasonPhrase };
            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return new Result<IEnumerable<Models.Product>>() { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
