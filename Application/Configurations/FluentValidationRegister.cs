using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class FluentValidationRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterValidators(this IServiceCollection services) =>
        services.AddValidatorsFromAssembly(AssemblyReference.Assembly);
}