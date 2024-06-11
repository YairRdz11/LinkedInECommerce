using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _ordersDbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<OrdersProvider> _logger;

        public OrdersProvider(OrdersDbContext ordersDbContext, IMapper mapper, ILogger<OrdersProvider> logger)
        {
            _ordersDbContext = ordersDbContext;
            _mapper = mapper;
            _logger = logger;
            SeedData();
        }

        private void SeedData()
        {
            if (!_ordersDbContext.Orders.Any())
            {
                _ordersDbContext.Orders.Add(new Db.Order
                {
                    Id = 1,
                    CustomerId = 1,
                    OrderDate = DateTime.Today,
                    Total = 100,
                    Items = new List<Db.OrderItem> { new Db.OrderItem { Id = 1, ProductId = 1, Quantity = 2, UnitPrice = 50 } }
                });
                _ordersDbContext.Orders.Add(new Db.Order
                {
                    Id = 2,
                    CustomerId = 3,
                    OrderDate = DateTime.Today,
                    Total = 150,
                    Items = new List<Db.OrderItem> { new Db.OrderItem { Id = 2, ProductId = 1, Quantity = 3, UnitPrice = 50 } }
                });

                _ordersDbContext.SaveChanges();
            }
        }

        public async Task<Result<IEnumerable<Models.Order>>> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await _ordersDbContext.Orders.Where(x=> x.CustomerId == customerId).ToListAsync();
                if (orders == null || !orders.Any())
                {
                    return new Result<IEnumerable<Models.Order>>() { IsSuccess = false, ResultObject = null, ErrorMessage = "Not found" };
                }
                var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                return new Result<IEnumerable<Models.Order>>() { IsSuccess = true, ResultObject = result, ErrorMessage = null };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return new Result<IEnumerable<Models.Order>>() { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
