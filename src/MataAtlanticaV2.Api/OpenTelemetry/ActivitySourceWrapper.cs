using System.Diagnostics;

namespace MataAtlanticaV2.OpenTelemetry;

public class ActivitySourceWrapper
{
    private readonly ActivitySource _activitySource;

    public ActivitySourceWrapper()
    {
        _activitySource = new ActivitySource(ServiceConstants.ApplicationName);
    }

    public ActivitySource GetActivitySource() => _activitySource;
}
