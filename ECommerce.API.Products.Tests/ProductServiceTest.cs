using AutoMapper;
using ECommerce.Api.Products.Db;
using ECommerce.Api.Products.Profiles;
using ECommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var productContext = new ProductDbContext(options);
            CreateProducts(productContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(productContext, null, mapper);

            var products = await productProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.ResultObject.Any());
            Assert.Null(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var productContext = new ProductDbContext(options);
            CreateProducts(productContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(productContext, null, mapper);

            var product = await productProvider.GetProductByIdAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.ResultObject);
            Assert.True(product.ResultObject.Id == 1);
            Assert.Null(product.ErrorMessage);
        }
        [Fact]
        public async Task GetProductsReturnsProductUsingInvalidId()
        {
            var options = new DbContextOptionsBuilder<ProductDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;
            var productContext = new ProductDbContext(options);
            CreateProducts(productContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(productContext, null, mapper);

            var product = await productProvider.GetProductByIdAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.ResultObject);
            Assert.NotNull(product.ErrorMessage);
        }


        private void CreateProducts(ProductDbContext productContext)
        {
            for (int i = 1; i <= 10; i++) {
                productContext.Products.Add(new Product 
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (Decimal) (i * 3.14)
                });
            }

            productContext.SaveChanges();
        }
    }
}