using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        public SearchService(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrderAsync(customerId);
            if (orderResult.IsSuccess) 
            { 
                var result = new
                {
                    Orders = orderResult.ResultObject
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
