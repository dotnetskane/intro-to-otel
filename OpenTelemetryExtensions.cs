using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace OTEL1;

public static class OpenTelemetryExtensions
{
    public static WebApplicationBuilder ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(logging =>
        {
            logging.IncludeFormattedMessage = true;
            logging.IncludeScopes = true;
        });

        builder.Services.AddOpenTelemetry().WithTracing(tracing =>
        {
            if (builder.Environment.IsDevelopment())
            {
                tracing.SetSampler(new AlwaysOnSampler());
            }

            tracing.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation();

            tracing.AddSource("OTEL1");
        });

        builder.Services.AddOpenTelemetry().WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddRuntimeInstrumentation();

            metrics.AddMeter("otel1.weatherapi");
        });

        builder.Services.AddOpenTelemetry().UseOtlpExporter();
        
        return builder;
    }
}
