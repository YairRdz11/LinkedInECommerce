using ECommerce.Api.Search.Interfaces;

namespace ECommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly ICustomerService _customerService;
        public SearchService(IOrderService orderService, IProductService productService, ICustomerService customerService)
        {
            _orderService = orderService;
            _productService = productService;
            _customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic? SearchResults)> SearchAsync(int customerId)
        {
            var customerResult = await _customerService.GetCustomerByIdAsync(customerId);
            var orderResult = await _orderService.GetOrderAsync(customerId);
            var productResult = await _productService.GetProductAsync();

            if (orderResult.IsSuccess) 
            {
                foreach (var order in orderResult.ResultObject)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = productResult.IsSuccess ?
                            productResult.ResultObject.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                            "Product Information is not available";

                    }
                }


                var result = new
                {
                    Customer = customerResult.IsSuccess ?
                        customerResult.ResultObject :
                        new Models.Customer { Name = "Customer information is not available" },
                    Orders = orderResult.ResultObject
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
