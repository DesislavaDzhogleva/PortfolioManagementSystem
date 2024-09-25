using Common.Configuration;
using Microsoft.EntityFrameworkCore;
using Portfolios.Contracts;
using Portfolios.Contracts.Repositories;
using Portfolios.Contracts.Strategies;
using Portfolios.Data;
using Portfolios.Repositories;
using Portfolios.Services;
using Portfolios.Strategies;
using Portfolios.Strategies.Factory;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
builder.Services.AddLogging();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PortfolioDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PortfolioManagementSystemStorage")));
builder.Services.Configure<RabbitMqSettings>(builder.Configuration.GetSection("RabbitMQ"));
builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();

builder.Services.AddScoped<IOrderExecutionTypeStrategy, BuyOrderExecutionStrategy>();
builder.Services.AddScoped<IOrderExecutionTypeStrategy, SellOrderExecutionStrategy>();
builder.Services.AddScoped<OrderTypeStrategyFactory>();

builder.Services.AddHostedService<PriceEventSubsriber>();
builder.Services.AddHostedService<OrderEventSubcriber>();
builder.Services.AddTransient<IPortfolioService, PortfolioService>();

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
