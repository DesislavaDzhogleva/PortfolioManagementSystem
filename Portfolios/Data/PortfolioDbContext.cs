using Microsoft.EntityFrameworkCore;
using Portfolios.Data.Models;

namespace Portfolios.Data
{
    public class PortfolioDbContext : DbContext
    {
        public PortfolioDbContext(DbContextOptions<PortfolioDbContext> options)
            : base(options)
        {
        }

        public DbSet<PortfolioEntity> Portfolios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
