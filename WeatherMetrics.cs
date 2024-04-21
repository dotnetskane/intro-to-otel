using System.Diagnostics.Metrics;

namespace OTEL1;

public class WeatherMetrics
{
    private readonly IMeterFactory meterFactory;

    private readonly Counter<int> totalWeatherForecasts;

    public WeatherMetrics(IMeterFactory meterFactory)
    {
        this.meterFactory = meterFactory;
        var meter = meterFactory.Create("otel1.weatherapi");
        totalWeatherForecasts = meter.CreateCounter<int>("weatherapi.forecasts_total", "forecasts", "Total number of weather forecasts generated.");
    }

    public void RecordWeatherForecast(int forecasts) => totalWeatherForecasts.Add(forecasts);
}
