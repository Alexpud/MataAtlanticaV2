using MataAtlanticaV2.Configuration;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class ConfigureServices
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(x => x.AddOpenTelemetry(p =>
        {
            p.SetResourceBuilder(ResourceBuilder.CreateEmpty()
                .AddService(ServiceConstants.ApplicationName));

            p.AddOtlpExporter(config =>
            {
                var seqOptions = new OpenTelemetryExporterItem();
                configuration.GetSection("OpenTelemetry:Exporter:Seq").Bind(seqOptions);
                config.Endpoint = new Uri($"{seqOptions.Url}/ingest/otlp/v1/logs");
                config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                config.Headers = $"X-Seq-ApiKey={seqOptions.Key}";
            });

            p.AddConsoleExporter();
            p.IncludeFormattedMessage = true;
            p.IncludeScopes = true;
        }));

        services
            .AddOpenTelemetry()
            .ConfigureResource(r => r.AddService(ServiceConstants.ApplicationName))
            .WithTracing(tracing =>
            {
                tracing.AddSource(ServiceConstants.ApplicationName);
                tracing.AddOtlpExporter(config =>
                {
                    var seqOptions = new OpenTelemetryExporterItem();
                    configuration.GetSection("OpenTelemetry:Exporter:Seq").Bind(seqOptions);
                    config.Endpoint = new Uri($"{seqOptions.Url}/ingest/otlp/v1/traces");
                    config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    config.Headers = $"X-Seq-ApiKey={seqOptions.Key}";
                });
                tracing.AddHttpClientInstrumentation();
                tracing.AddConsoleExporter();
                tracing.AddAspNetCoreInstrumentation();
            })
            .WithMetrics(metric =>
            {
                metric.AddMeter(
                    "Microsoft.AspNetCore.Hosting",
                    "System.Net.Http");
                metric.AddRuntimeInstrumentation();
                metric.AddOtlpExporter(config =>
                {
                    var seqOptions = new OpenTelemetryExporterItem();
                    configuration.GetSection("OpenTelemetry:Exporter:Seq").Bind(seqOptions);
                    config.Endpoint = new Uri($"{seqOptions.Url}/ingest/otlp/v1/metrics");
                    config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    config.Headers = $"X-Seq-ApiKey={seqOptions.Key}";
                });
            });

        return services;
    }
}