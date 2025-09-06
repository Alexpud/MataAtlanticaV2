using MataAtlanticaV2.Application.Produtos;
using MataAtlanticaV2.Configuration;
using MataAtlanticaV2.Domain.Repository;
using MataAtlanticaV2.Infrastructure.Repositories;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

public static class ConfigureServices
{
    public static IServiceCollection AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        
        var otlpOptions = GetEnabledOtlpOptions(configuration);

        services.AddLogging(x => x.AddOpenTelemetry(p =>
        {
            p.SetResourceBuilder(ResourceBuilder.CreateEmpty()
                .AddService(ServiceConstants.ApplicationName));

            p.AddOtlpExporter(config =>
            {
                config.Endpoint = new Uri($"{otlpOptions.Url}/v1/logs");
                config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                config.Headers = otlpOptions.Key;
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
                    config.Endpoint = new Uri($"{otlpOptions.Url}/v1/traces");
                    config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    config.Headers = otlpOptions.Key;
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
                    config.Endpoint = new Uri($"{otlpOptions.Url}");
                    config.Protocol = OpenTelemetry.Exporter.OtlpExportProtocol.HttpProtobuf;
                    config.Headers = otlpOptions.Key;
                });
            });

        return services;
    }

    private static OpenTelemetryExporterItem GetEnabledOtlpOptions(IConfiguration configuration)
    {
        var newRelicOtlOptions = new OpenTelemetryExporterItem();
        configuration.GetSection("OpenTelemetry:Exporter:NewRelic").Bind(newRelicOtlOptions);

        var seqOtlOptions = new OpenTelemetryExporterItem();
        configuration.GetSection("OpenTelemetry:Exporter:Seq").Bind(seqOtlOptions);

        var otlpOptions = newRelicOtlOptions.Enabled ? newRelicOtlOptions : seqOtlOptions;
        return otlpOptions;
    }
}