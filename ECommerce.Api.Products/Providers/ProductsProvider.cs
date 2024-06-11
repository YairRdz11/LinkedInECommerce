using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<IEnumerable<Models.Product>>> GetProductsAsync()
        {
            try
            {
                var products = await _dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return new Result<IEnumerable<Models.Product>> { IsSuccess = true, ResultObject = result, ErrorMessage = null };
                }

                return new Result<IEnumerable<Models.Product>> { IsSuccess = false, ResultObject = null, ErrorMessage = "Not Found" };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return new Result<IEnumerable<Models.Product>> { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }

        public async Task<Result<Models.Product>> GetProductByIdAsync(int id)
        {
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product != null)
                {
                    var result = _mapper.Map<Db.Product, Models.Product>(product);
                    return new Result<Models.Product> { IsSuccess = true, ResultObject = result, ErrorMessage = null };
                }

                return new Result<Models.Product> { IsSuccess = false, ResultObject = null, ErrorMessage = "Not Found" };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return new Result<Models.Product>{ IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
