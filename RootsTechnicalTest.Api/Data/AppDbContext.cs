using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Api.Data;
public class AppDbContext(DbContextOptions<AppDbContext> options) : Microsoft.EntityFrameworkCore.DbContext(options)
{
    /// Represents the CarBrands table.
    public DbSet<MarcasAutos> CarBrands => Set<MarcasAutos>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Entity mapping
        modelBuilder.Entity<MarcasAutos>(entity =>
        {
            // Map to table "CarBrands"
            entity.ToTable("MarcasAutos");

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
        modelBuilder.Entity<MarcasAutos>().HasData(
            new MarcasAutos { Id = 1, Name = "Toyota", Country = "Japan" },
            new MarcasAutos { Id = 2, Name = "Ford", Country = "United States" },
            new MarcasAutos { Id = 3, Name = "Volkswagen", Country = "Germany" },
            new MarcasAutos { Id = 4, Name = "Honda", Country = "Bulgaria" },
            new MarcasAutos { Id = 5, Name = "Mazda", Country = "Japan" }
        );
    }
}
