using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Read connection string from configuration or environment
var connectionString = builder.Configuration.GetConnectionString("Postgres")
         ?? Environment.GetEnvironmentVariable("POSTGRES_CONNECTION");

// Register DbContext with Npgsql provider.
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Apply pending migrations at startup (idempotent).
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();