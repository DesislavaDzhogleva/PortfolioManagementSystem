using Microsoft.EntityFrameworkCore;
using Orders.Data.Models;

namespace Orders.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options)
            : base(options) { }

        public DbSet<OrderEntity> Orders { get; set; }
    }
}
