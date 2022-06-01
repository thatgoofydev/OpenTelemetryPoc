using Newtonsoft.Json;
using WeatherApi.Models;

namespace WeatherApi.ServiceClients;

public interface ILocationService
{
    Task<Coordinates> GetCoordinates(string city);
}

public class LocationServiceClient : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<Coordinates> GetCoordinates(string city)
    {
        var response = await _httpClient.GetAsync("https://localhost:7001/api/location/coordinates?city=" + city);
        response.EnsureSuccessStatusCode();

        var body = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<Coordinates>(body)!;
    }
}