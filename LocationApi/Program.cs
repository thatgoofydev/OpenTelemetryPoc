using LocationApi;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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