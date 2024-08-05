using Business;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

// Configurar servicios
builder.Services.AddControllers();

// Configurar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// Configurar DbContext con SQL Server
builder.Services.AddDbContext<ECommerceContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnString")));

var app = builder.Build();

builder.Services
    .RegisterMediator()
    .RegisterValidators()
    .RegisterDatabase(builder.Configuration)
    .RegisterSwagger();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce API v1"));

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Usar CORS
app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.RegisterProductsEndpoints();
app.RegisterOrderEndpoints();

app.Run();