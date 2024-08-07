public static class CorsRegister
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterCors(this IServiceCollection services)
    {
        // Configurar CORS
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            options.AddPolicy("AllowSpecificOrigin",
             builder =>
             {
                 builder.WithOrigins("http://localhost:4200") // Origen permitido
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials(); 
             });
        });

        return services;
    }
}