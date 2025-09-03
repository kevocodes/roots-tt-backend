using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RootsTechnicalTest.Api.Controllers;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Tests;

public class MarcasAutosControllerTests
{
    // Should return 200 OK and the 5 seeded items ordered by Id.
    [Fact]
    public async Task GetAllReturnsOkWithSeededBrandsInOrder()
    {
        using var ctx = TestDbHelper.CreateContext(nameof(GetAllReturnsOkWithSeededBrandsInOrder), seed: true);
        var controller = new MarcasAutosController(ctx);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);

        Assert.Equal(5, list.Count());

        var ids = list.Select(x => x.Id).ToArray();
        Assert.Equal(ids.OrderBy(i => i), ids); // Enforces ascending order by Id.
    }

    // Should return 200 OK and an empty list when there is no data.
    [Fact]
    public async Task GetAllReturnsEmptyListWhenNoData()
    {
        using var ctx = TestDbHelper.CreateContext(nameof(GetAllReturnsEmptyListWhenNoData));
        var controller = new MarcasAutosController(ctx);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);
        Assert.Empty(list);
    }

    // Should never return items with null/empty Name.
    [Fact]
    public async Task GetAllReturnsItemsWithNonEmptyNames()
    {
        using var ctx = TestDbHelper.CreateContext(nameof(GetAllReturnsItemsWithNonEmptyNames), seed: true);
        var controller = new MarcasAutosController(ctx);

        var result = await controller.GetAll();

        var ok = Assert.IsType<OkObjectResult>(result.Result);
        var list = Assert.IsAssignableFrom<IEnumerable<MarcasAutos>>(ok.Value);
        Assert.All(list, item => Assert.False(string.IsNullOrWhiteSpace(item.Name)));
    }
}
