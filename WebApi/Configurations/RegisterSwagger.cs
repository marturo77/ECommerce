public static class SwaggerRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer()
            .AddSwaggerGen(options => { options.CustomSchemaIds(s => s.FullName?.Replace('+', '.')); });

        return services;
    }
}