using Microsoft.AspNetCore.Mvc;
using WeatherApi.Models;
using WeatherApi.ServiceClients;

namespace WeatherApi.Controllers;

[ApiController]
[Route("api/weather")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILocationService _locationService;

    public WeatherForecastController(ILocationService locationService)
    {
        _locationService = locationService;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string? city = null)
    {
        Coordinates? coordinates = null; 
        if (city != null)
        {
            coordinates = await _locationService.GetCoordinates(city);
        }
        
        if (Random.Shared.Next(2) == 0)
        {
            await SomeAction();
            await SomeAction();
            await SomeAction();
            await SomeAction();
            await SomeAction();
        }
        else
        {
            await Task.WhenAll(
                SomeAction(),
                SomeAction(),
                SomeAction(),
                SomeAction(),
                SomeAction()
            );
        }

        return Ok(new
        {
            Coordinates = coordinates,
            Forecast = GetRandomForecast()
        });
    }

    private static async Task SomeAction()
    {
        using var activity = Telemetry.Source.StartActivity();
        await Task.Delay(TimeSpan.FromMilliseconds(Random.Shared.Next(50)));
    }
    
    private static readonly string[] Summaries = { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
    private static WeatherForecast[] GetRandomForecast()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}