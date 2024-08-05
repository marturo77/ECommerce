using Todo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services
    .RegisterCors()
    .RegisterValidators()
    .RegisterMediator()
    .RegisterDatabase(builder.Configuration)
    .RegisterSwagger();

var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1"));

app.UseHttpsRedirection();
app.UseStaticFiles();

// Usar CORS
app.UseCors("AllowAllOrigins");

app.RegisterProductsEndpoints();
app.RegisterOrderEndpoints();
app.UseRouting();

app.Run();