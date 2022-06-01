using System.Diagnostics;

namespace WeatherApi;

public static class Telemetry
{
    public const string ServiceName = "WeatherApi";
    public const string ServiceVersion = "1.3.5";
    public static readonly ActivitySource Source = new(ServiceName);
}