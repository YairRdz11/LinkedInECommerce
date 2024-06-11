using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Orders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersProvider _orderProvider;

        public OrdersController(IOrdersProvider ordersProvider)
        {
            _orderProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var result = await _orderProvider.GetOrdersAsync(customerId);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
