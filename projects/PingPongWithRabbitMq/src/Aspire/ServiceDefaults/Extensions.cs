using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.Hosting;

public static class Extensions
{
    public static IServiceCollection AddServiceDefaults(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOpenTelemetry(configuration);

        services.AddServiceDiscovery();

        services.ConfigureHttpClientDefaults(http =>
        {
            // Turn on resilience by default
            http.AddStandardResilienceHandler();

            // Turn on service discovery by default
            http.AddServiceDiscovery();
        });

        return services;
    }

    private static IServiceCollection ConfigureOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter("Wolverine")
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation();
            })
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource("Wolverine")
                    .AddAspNetCoreInstrumentation()
                    // Uncomment the following line to enable gRPC instrumentation (requires the OpenTelemetry.Instrumentation.GrpcNetClient package)
                    //.AddGrpcClientInstrumentation()
                    .AddHttpClientInstrumentation();
            });

        services.AddOpenTelemetryExporters(configuration);

       return services;
    }

    private static IServiceCollection AddOpenTelemetryExporters(this IServiceCollection services, IConfiguration configuration)
    {
        var useOtlpExporter = !string.IsNullOrWhiteSpace(configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

        if (useOtlpExporter)
        {
            services.AddOpenTelemetry().UseOtlpExporter();
        }

        return services;
    }
}
