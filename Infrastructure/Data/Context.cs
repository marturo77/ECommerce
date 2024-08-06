using Application.Modules.Orders.Models;
using Application.Modules.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Business
{
    /// <summary>
    /// Contexto de datos, en caso de requerir una arquitectura de microservicios, es necesario
    /// crear otro contexto y distribuir las entidadades productos y ordenes en diferentes contextos
    ///
    /// Para este ejemplo se conservan los datos en el mismo contexto de datos.
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
        /// Tabla o informacion relacionada con las ordenes
        /// </summary>
        public DbSet<OrderInfo> Orders { get; set; }

        /// <summary>
        /// Informacion relacionada con el detalle de cantidad y precio incluida en
        /// cada item de la orden
        /// </summary>
        public DbSet<OrderItemInfo> OrderItems { get; set; }

        /// <summary>
        /// Tabla para los productos
        /// </summary>
        public DbSet<ProductInfo> Products { get; set; }

        /// <summary>
        /// Configuracion cardinal de datos
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderInfo>()
               .HasKey(o => o.OrderId);

            modelBuilder.Entity<OrderInfo>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderInfo>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Configurar eliminación en cascada

            modelBuilder.Entity<OrderItemInfo>()
                .HasKey(oi => oi.OrderItemId);

            modelBuilder.Entity<OrderItemInfo>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItemInfo>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<ProductInfo>()
                .HasKey(p => p.ProductId);

            modelBuilder.Entity<ProductInfo>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Asegúrate de que OrderId, OrderItemId y ProductId sean columnas de identidad
            modelBuilder.Entity<OrderInfo>()
                .Property(o => o.OrderId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItemInfo>()
                .Property(oi => oi.OrderItemId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProductInfo>()
                .Property(p => p.ProductId)
                .ValueGeneratedOnAdd();
        }
    }
}