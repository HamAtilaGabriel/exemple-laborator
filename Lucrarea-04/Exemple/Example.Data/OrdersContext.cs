using Example.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.Data
{
    public class OrdersContext: DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options) : base(options) {}

        public DbSet<ProductDto> Products { get; set; }

        public DbSet<OrderLineDto> OrderLines { get; set; }

        public DbSet<OrderHeaderDto> OrderHeaders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDto>().ToTable("Product").HasKey(s => s.ProductId);
            modelBuilder.Entity<OrderLineDto>().ToTable("OrderLine").HasKey(s => s.OrderLineId);
            modelBuilder.Entity<OrderHeaderDto>().ToTable("OrderHeader").HasKey(s => s.OrderId);
        }
    }
}
