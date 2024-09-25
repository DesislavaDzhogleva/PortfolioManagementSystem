using Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Prices.Contracts.Repostories;
using Prices.Contracts.Services;
using Prices.Data;
using Prices.Repositories;
using Prices.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddMemoryCache();

builder.Services.AddLogging();
// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddDbContext<PriceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfolioManagementSystemStorage")));
builder.Services.AddScoped<IStockRepository, StockRepository>();
builder.Services.AddScoped<IStockService, StockService>();

builder.Services.AddHostedService<PricePublisher>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
