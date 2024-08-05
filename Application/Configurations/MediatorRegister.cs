using Microsoft.Extensions.DependencyInjection;

public static class MediatorRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterMediator(this IServiceCollection services) =>
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(AssemblyReference.Assembly); });
}