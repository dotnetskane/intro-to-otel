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
            logging.AddOtlpExporter();
        });

        builder.Services.AddOpenTelemetry().WithMetrics(metrics =>
        {
            metrics.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddRuntimeInstrumentation()
                   .AddOtlpExporter();

            metrics.AddMeter("otel1.weatherapi");
        });

        builder.Services.AddOpenTelemetry().WithTracing(tracing =>
        {
            if (builder.Environment.IsDevelopment())
            {
                tracing.SetSampler(new AlwaysOnSampler());
            }

            tracing.AddAspNetCoreInstrumentation()
                   .AddHttpClientInstrumentation()
                   .AddOtlpExporter();
            
            tracing.AddSource("OTEL1");
        });

        return builder;
    }
}
