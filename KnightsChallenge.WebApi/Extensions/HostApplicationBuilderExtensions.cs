using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace KnightsChallenge.WebApi.Extensions;

public static class HostApplicationBuilderExtensions
{
  public static IHostApplicationBuilder ConfigureOpenTelemetry (this IHostApplicationBuilder builder)
  {
    builder.Logging.AddOpenTelemetry(options =>
    {
      options.IncludeScopes = true;
      options.IncludeFormattedMessage = true;
    });
    
    builder.Services
      .AddOpenTelemetry()
      .WithMetrics(opt =>
      {
        opt
          .AddRuntimeInstrumentation()
          .AddMeter("Microsoft.AspNetCore.Hosting", "Microsoft.AspNetCore.Server.Kestrel", "System.Net.Http");
      })
      .WithTracing(opt =>
      {
        opt
          .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
              .AddService("ModularMonolith"))
          .AddAspNetCoreInstrumentation()
          .AddHttpClientInstrumentation();
      });

    builder.AddOpenTelemetryExporters();

    return builder;
  }

  public static IHostApplicationBuilder AddOpenTelemetryExporters (this IHostApplicationBuilder builder)
  {
    var useOtlpExporter = !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("OTEL_EXPORTER_OTLP_ENDPOINT"));

    if (useOtlpExporter)
    {
      builder.Services.Configure<OpenTelemetryLoggerOptions>(logging => logging.AddOtlpExporter());
      builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter());
      builder.Services.ConfigureOpenTelemetryTracerProvider(tracing => tracing.AddOtlpExporter());
    }

    return builder;
  }
}