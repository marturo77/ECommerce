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

        // Registro de cache de repositorios para operaciones CRUD como operaciones de consulta
        // En que los repositorios de consulta y escritura sean diferentes se podria inyectar la cadena de conexion
        // diferente para cada implementacion de repositorio, util para escenarios donde se tiene base de datos en modo
        // replicacion o inclusive bases de datos o repositorios en nube como table storage accounts, mongodb etc.

        _ = services.AddScoped<IProductRepository, ProductRepository>();
        _ = services.AddScoped<IProductQuery, ProductQueryRepository>();
        _ = services.AddScoped<IOrderRepository, OrderRepository>();
        _ = services.AddScoped<IOrderQuery, OrderQueryRepository>();

        // Patron singleton para el administrador de cache
        _ = services.AddSingleton<ICachingManager, CachingManager>();

        return services;
    }
}