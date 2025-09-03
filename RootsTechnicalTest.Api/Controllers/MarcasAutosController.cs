using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RootsTechnicalTest.Api.Data;
using RootsTechnicalTest.Api.Domain;

namespace RootsTechnicalTest.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarcasAutosController(AppDbContext appDb) : ControllerBase
{
    /// <summary>
    /// Get all car brands stored in the database
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MarcasAutos>>> GetAll()
    {
        var data = await appDb.CarBrands
            .AsNoTracking() // readonly optimization
            .OrderBy(b => b.Id)
            .ToListAsync();

        return Ok(data);
    }
}