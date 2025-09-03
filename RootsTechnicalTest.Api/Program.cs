using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Reflection;
using Microsoft.OpenApi.Models;
using RootsTechnicalTest.Api.Data;


var builder = WebApplication.CreateBuilder(args);

// Read connection string from configuration or environment
var connectionString = builder.Configuration.GetConnectionString("Postgres") 
                       ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                       ?? throw new InvalidOperationException("Postgres connection string not configured.");


// Register DbContext with Npgsql provider.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
builder.Services.AddControllers();

// Generate Open API docs
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "RootsTechnicalTest API",
        Version = "v1",
        Description = "Documentation for Roots Technical Test API"
    });

    // Include XML Comments
    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});

var app = builder.Build();

// Apply pending migrations at startup.
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    // Expose /swagger/v1/swagger.json
    app.UseSwagger();

    // Configure scalar UI
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("RootsTechnicalTest API")
            .WithDarkMode(true)
            .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json"); // 👈 clave
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();