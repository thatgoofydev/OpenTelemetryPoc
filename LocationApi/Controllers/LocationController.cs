using LocationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LocationApi.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly ILogger<LocationController> _logger;

    public LocationController(DatabaseContext context, ILogger<LocationController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    [HttpGet("coordinates")]
    public async Task<IActionResult> GetCoords([FromQuery] string city)
    {
        var count = await _context.Simples
            .Where(x => EF.Functions.Like(x.Value, $"%{city[0]}%"))
            .CountAsync();
        
        _logger.LogInformation("{Count} values in database containing '{Letter}'", count, city[0]);
        
        
        return Ok(new Coordinates
        {
            Latitude = "51.509865",
            Longitude = "-0.118092"
        });
    }
}