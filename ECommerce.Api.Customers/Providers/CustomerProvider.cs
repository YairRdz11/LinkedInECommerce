using AutoMapper;
using ECommerce.Api.Customers.Db;
using ECommerce.Api.Customers.Interfaces;
using ECommerce.Api.Customers.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Customers.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        private readonly CustomerDbContext _dbContext;
        private readonly ILogger<CustomerProvider> _logger;
        private readonly IMapper _mapper;
        public CustomerProvider(CustomerDbContext dbContext, ILogger<CustomerProvider> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if (!_dbContext.Customers.Any()) 
            {
                _dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Yair Rodriguez", Address = "18 de marzo #6" });
                _dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Angel Coronado", Address = "Direccion 2" });
                _dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Laura Coronado", Address = "Direccion 3" });
                _dbContext.SaveChanges();
            }
        }

        public async Task<Result<IEnumerable<Models.Customer>>> GetCustomersAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.ToListAsync();

                if (customers != null || customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return new Result<IEnumerable<Models.Customer>> { IsSuccess = true, ResultObject = result, ErrorMessage = null };
                }
                return new Result<IEnumerable<Models.Customer>> { IsSuccess = false, ResultObject = null, ErrorMessage = "Not Found" };
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return new Result<IEnumerable<Models.Customer>> { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }

        public async Task<Result<Models.Customer>> GetCustomerByIdAsync(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
                if (customer != null)
                {
                    var result = _mapper.Map<Db.Customer, Models.Customer>(customer);
                    return new Result<Models.Customer> { IsSuccess = true, ResultObject = result, ErrorMessage = null };
                }
                return new Result<Models.Customer> { IsSuccess = false, ResultObject = null, ErrorMessage = "Not found" };
            }
            catch (Exception ex) 
            {
                _logger?.LogError(ex.ToString());
                return new Result<Models.Customer> { IsSuccess = false, ResultObject = null, ErrorMessage = ex.ToString() };
            }
        }
    }
}
