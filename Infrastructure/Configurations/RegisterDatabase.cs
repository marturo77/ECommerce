using Application.Modules.Orders.Queries;
using Application.Modules.Orders.Repositories;
using Application.Modules.Products.Queries;
using Application.Modules.Products.Repositories;
using Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///
/// </summary>
public static class DatabaseRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddDbContext<ECommerceContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ConnString"));
        });

        // Registro del repositorio para productos
        _ = services.AddScoped<IProductRepository, ProductRepository>();

        // Registro del repositior para ordenes
        _ = services.AddScoped<IOrderRepository, OrderRepository>();

        _ = services.AddScoped<IProductQuery, ProductQueryRepository>();

        _ = services.AddScoped<IOrderQuery, OrderQueryRepository>();

        return services;
    }
}