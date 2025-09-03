namespace RootsTechnicalTest.Api.Domain;

/// <summary>
/// Domain entity that represents a car brand registered in the system.
/// </summary>
public class MarcasAutos
{
    public int Id { get; set; }

    /// <summary>
    /// Brand's name (e.g., "Toyota").
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Optional country of origin (e.g., "Japan").
    /// </summary>
    public string? Country { get; set; }
}
