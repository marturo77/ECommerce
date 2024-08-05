using Application.Modules.Orders.Models;
using Application.Modules.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    /// <summary>
    ///
    /// </summary>
    public class ECommerceContext : DbContext
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="options"></param>
        public ECommerceContext(DbContextOptions<ECommerceContext> options)
            : base(options)
        {
        }

        /// <summary>
        ///
        /// </summary>
        public DbSet<OrderInfo> Orders { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DbSet<OrderItemInfo> OrderItems { get; set; }

        /// <summary>
        ///
        /// </summary>
        public DbSet<ProductInfo> Products { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItemInfo>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItemInfo>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
        }
    }
}