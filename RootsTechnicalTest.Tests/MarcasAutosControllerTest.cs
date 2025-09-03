using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RootsTechnicalTest.Api.Controllers;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Tests;

/// <summary>
/// Unit tests for MarcasAutosController using EF Core InMemory provider.
/// </summary>
public class MarcasAutosControllerTests
{
    /// <summary>
    /// Verifies the controller returns HTTP 200 with the seeded list ordered by Id.
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsOk_WithSeededBrands_InOrder()
    {
        // Arrange: create an isolated in-memory DB and seed data
        using var ctx = TestDbHelper.CreateContext(nameof(GetAll_ReturnsOk_WithSeededBrands_InOrder), seed: true);
        var controller = new MarcasAutosController(ctx);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);

        Assert.Equal(5, list.Count());
        Assert.True(list.Select(x => x.Id).SequenceEqual(list.Select(x => x.Id).OrderBy(id => id)),
            "The controller should return brands ordered by Id ascending.");
    }

    /// <summary>
    /// Verifies the controller returns HTTP 200 and an empty list when there is no data.
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsEmptyList_WhenNoData()
    {
        // Arrange: empty in-memory DB
        using var ctx = TestDbHelper.CreateContext(nameof(GetAll_ReturnsEmptyList_WhenNoData));
        var controller = new MarcasAutosController(ctx);

        // Act
        var result = await controller.GetAll();

        // Assert
        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);
        Assert.Empty(list);
    }
    
    [Fact]
    public async Task GetAll_ReturnsJsonResultWithNonNullNames()
    {
        using var ctx = TestDbHelper.CreateContext(nameof(GetAll_ReturnsJsonResultWithNonNullNames), seed: true);
        var controller = new MarcasAutosController(ctx);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);
        Assert.All(list, item => Assert.False(string.IsNullOrWhiteSpace(item.Name)));
    }

}