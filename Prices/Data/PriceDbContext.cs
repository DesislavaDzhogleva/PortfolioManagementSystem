using Microsoft.EntityFrameworkCore;
using Prices.Data.Models;

namespace Prices.Data
{
    public class PriceDbContext : DbContext
    {
        public PriceDbContext(DbContextOptions<PriceDbContext> options)
            : base(options) { }

        public DbSet<StockEntity> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockEntity>().HasData(
                new StockEntity { Id = 1, Ticker = "AAPL", CompanyName = "Apple Inc." },
                new StockEntity { Id = 2, Ticker = "GOOGL", CompanyName = "Alphabet Inc." },
                new StockEntity { Id = 3, Ticker = "NVDA", CompanyName = "NVIDIA Corporation" },
                new StockEntity { Id = 4, Ticker = "MSFT", CompanyName = "Microsoft Corporation" },
                new StockEntity { Id = 5, Ticker = "AMZN", CompanyName = "Amazon.com Inc." },
                new StockEntity { Id = 6, Ticker = "FB", CompanyName = "Meta Platforms Inc." }
            );

            modelBuilder.Entity<StockEntity>().HasIndex(s => s.Ticker).IsUnique();
        }
    } 
}
