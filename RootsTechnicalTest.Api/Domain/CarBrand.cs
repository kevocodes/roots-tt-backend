namespace RootsTechnicalTest.Api.Domain;

/// <summary>
/// Domain entity that represents a car brand registered in the system.
/// Kept as a simple POCO to avoid framework coupling.
/// </summary>
public class CarBrand
{
    public int Id { get; set; }

    /// <summary>
    /// Brand's name (e.g., "Toyota").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Optional country of origin (e.g., "Japan"). Not required for the test,
    /// but useful to show minimal realistic modeling.
    /// </summary>
    public string? Country { get; set; }
}
