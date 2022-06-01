using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using WeatherApi;
using WeatherApi.ServiceClients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddScoped<ILocationService, LocationServiceClient>();
builder.Services.AddOpenTelemetryTracing(tracerProviderBuilder =>
{
    tracerProviderBuilder
        .AddOtlpExporter(opt =>
        {
            opt.Protocol = OtlpExportProtocol.Grpc;
        })
        .AddSource(Telemetry.Source.Name)
        .SetResourceBuilder(ResourceBuilder.CreateDefault()
            .AddService(Telemetry.ServiceName, serviceVersion: Telemetry.ServiceVersion))
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation();
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();