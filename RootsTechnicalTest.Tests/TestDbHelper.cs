using System.Linq;
using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Data;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Tests;

// Creates an isolated in-memory DbContext per request
internal static class TestDbHelper
{
    public static AppDbContext CreateContext(string dbName, bool seed = false)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var ctx = new AppDbContext(options);

        // Seed a fixed dataset only when requested.
        if (seed && !ctx.CarBrands.Any())
        {
            ctx.CarBrands.AddRange(
                new MarcasAutos { Id = 1, Name = "Toyota",     Country = "Japan" },
                new MarcasAutos { Id = 2, Name = "Ford",       Country = "United States" },
                new MarcasAutos { Id = 3, Name = "Volkswagen", Country = "Germany" },
                new MarcasAutos { Id = 4, Name = "Honda",      Country = "Bulgaria" },
                new MarcasAutos { Id = 5, Name = "Mazda",      Country = "Japan" }
            );
            ctx.SaveChanges();
        }

        return ctx;
    }
}