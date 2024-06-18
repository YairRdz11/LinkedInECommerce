using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        public SearchService(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var orderResult = await _orderService.GetOrderAsync(customerId);
            var productResult = await _productService.GetProductAsync();
            if (orderResult.IsSuccess) 
            {
                foreach (var order in orderResult.ResultObject)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult
                            .ResultObject
                            .FirstOrDefault(p => p.Id == item.ProductId)?.Name;
                    }
                }


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
