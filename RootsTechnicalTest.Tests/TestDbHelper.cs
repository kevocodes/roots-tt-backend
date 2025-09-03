using System.Linq;
using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Data;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Tests;

/// <summary>
/// Creates in-memory DbContext instances for isolated unit tests.
/// </summary>
internal static class TestDbHelper
{
    /// <summary>
    /// Creates an in-memory AppDbContext. When <paramref name="seed"/> is true,
    /// the method populates the database with a fixed set of CarBrands.
    /// </summary>
    public static AppDbContext CreateContext(string dbName, bool seed = false)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var ctx = new AppDbContext(options);

        if (seed && !ctx.CarBrands.Any())
        {   
            // Mirror the real seed so the controller returns 5 items
            ctx.CarBrands.AddRange(
                new MarcasAutos { Id = 1, Name = "Toyota",      Country = "Japan" },
                new MarcasAutos { Id = 2, Name = "Ford",        Country = "United States" },
                new MarcasAutos { Id = 3, Name = "Volkswagen",  Country = "Germany" },
                new MarcasAutos { Id = 4, Name = "Honda",       Country = "Bulgaria" },
                new MarcasAutos { Id = 5, Name = "Mazda",       Country = "Japan" }
            );
            ctx.SaveChanges();
        }

        return ctx;
    }
}