using LocationApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace LocationApi.Controllers;

[ApiController]
[Route("api/location")]
public class LocationController : ControllerBase
{
    [HttpGet("coordinates")]
    public IActionResult GetCoords([FromQuery] string city)
    {
        return Ok(new Coordinates
        {
            Latitude = "51.509865",
            Longitude = "-0.118092"
        });
    }
}