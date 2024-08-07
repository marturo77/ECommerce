var builder = WebApplication.CreateBuilder(args);

// Registrar servicios adicionales.
builder.Services
    .RegisterCors()
    .RegisterValidators()
    .RegisterMediator()
    .RegisterDatabase(builder.Configuration)
    .RegisterSwagger()
    .AddSignalR();

var app = builder.Build();

// Como es una prueba tecnica se permite siempre la visualizacion de swagger
app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1"));

app.UseHttpsRedirection();
app.UseStaticFiles();

// Usar CORS
app.UseCors("AllowAllOrigins");
app.UseRouting();
app.UseAuthorization();

// Registrar Endpoints personalizados
app.RegisterProductsEndpoints();
app.RegisterOrderEndpoints();

// Registro de notificaciones
app.UseSignalR();

app.Run();