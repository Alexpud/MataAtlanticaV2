namespace MataAtlanticaV2.Configuration;

public class OpenTelemetryOptions
{
    public OpenTelemetryExporter Exporter { get; set; }
}

public class OpenTelemetryExporter
{
    public OpenTelemetryExporterItem Seq { get; set; }
}

public class OpenTelemetryExporterItem
{
    public string Url { get; set; }
    public string Key { get; set; }
}
