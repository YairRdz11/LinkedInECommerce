using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductProvider
    {
        private readonly ProductDbContext _dbContext;
        private readonly ILogger<ProductsProvider> _logger;
        private readonly IMapper _mapper;
        public ProductsProvider(ProductDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper) 
        { 
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Products.Any()) 
            {
                _dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 20, Inventory = 15});
                _dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Monitor", Price = 50, Inventory = 50 });
                _dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Mouse", Price = 7, Inventory = 500 });
                _dbContext.Products.Add(new Db.Product() { Id = 4, Name = "CPU", Price = 200, Inventory = 5 });
                _dbContext.SaveChanges();
            }
        }

        public Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
