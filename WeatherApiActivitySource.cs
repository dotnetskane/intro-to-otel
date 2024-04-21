using System.Diagnostics;

namespace OTEL1;

public static class WeatherApiActivitySource
{
    public static readonly ActivitySource ActivitySource = new("OTEL1");
}
