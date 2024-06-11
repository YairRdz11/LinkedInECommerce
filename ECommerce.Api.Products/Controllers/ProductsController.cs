using ECommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Api.Products.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductProvider _productProvider;
        public ProductsController(IProductProvider productProvider)
        {
            _productProvider = productProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductAsync()
        {
            var result = await _productProvider.GetProductsAsync();
            if (result.IsSuccess) 
            {
                return Ok(result);
            }
            return NotFound();
        }
    }
}
