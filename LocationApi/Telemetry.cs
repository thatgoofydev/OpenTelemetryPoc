using System.Diagnostics;

namespace LocationApi;

public static class Telemetry
{
    public const string ServiceName = "LocationApi";
    public const string ServiceVersion = "1.0.0";
    public static readonly ActivitySource Source = new(ServiceName);
}