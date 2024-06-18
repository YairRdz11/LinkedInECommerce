using ECommerce.Api.Search.Interfaces;
using ECommerce.Api.Search.Services;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IProductService, ProductsService>();
builder.Services.AddHttpClient("OrdersServices", config =>
{
    string orderUrl = builder.Configuration.GetSection("Services:Orders").Value;

    config.BaseAddress = new Uri(orderUrl);
});
builder.Services.AddHttpClient("ProductsServices", config =>
{
    string productUrl = builder.Configuration.GetSection("Services:Products").Value;
    config.BaseAddress = new Uri(productUrl);
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
