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

        _ = services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}