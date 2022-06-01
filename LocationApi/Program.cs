using LocationApi;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Exporter;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(b => b.UseSqlServer("Server=.;Database=otel_test;Trusted_Connection=True;"));
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
        .AddHttpClientInstrumentation()
        .AddSqlClientInstrumentation(opt =>
        {
            opt.SetDbStatementForText = true;
            opt.RecordException = true;
            opt.EnableConnectionLevelAttributes = true;
        });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();