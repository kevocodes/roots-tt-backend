using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    /// Represents the CarBrands table.
    public DbSet<CarBrand> CarBrands => Set<CarBrand>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity mapping
        modelBuilder.Entity<CarBrand>(entity =>
        {
            // Map to table "CarBrands"
            entity.ToTable("CarBrands");

            // Primary key
            entity.HasKey(x => x.Id);

            // Column rules/constraints
            entity.Property(x => x.Name)
                  .IsRequired()         // NOT NULL at the database level
                  .HasMaxLength(120);   // varchar(120)

            entity.Property(x => x.Country)
                  .HasMaxLength(80);    // optional, limited length
        });

        // HasData is applied via migrations. After adding a migration and updating
        // the database, EF will insert (or update) these rows to match the model snapshot.
        modelBuilder.Entity<CarBrand>().HasData(
            new CarBrand { Id = 1, Name = "Toyota",     Country = "Japan" },
            new CarBrand { Id = 2, Name = "Ford",       Country = "United States" },
            new CarBrand { Id = 3, Name = "Volkswagen", Country = "Germany" }
        );
    }
}
