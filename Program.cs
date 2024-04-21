using Microsoft.AspNetCore.Mvc;
using OTEL1;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureOpenTelemetry();
builder.Services.AddSingleton<WeatherMetrics>();
builder.Services.AddHttpClient();
builder.Configuration.AddUserSecrets<Program>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", ([FromServices]ILogger<Program> logger, [FromServices] WeatherMetrics metrics) =>
{
    using var activity = WeatherApiActivitySource.ActivitySource.StartActivity(
        name: "Custom activity GetWeatherForecasts",
        kind: System.Diagnostics.ActivityKind.Internal,
        tags: [new("Hello", "World")]);

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    
    logger.LogInformation("Getting {forecasts} weather forecasts.", forecast.Length);
    metrics?.RecordWeatherForecast(forecast.Length);
    
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapGet("/downstream", async ([FromServices] IHttpClientFactory clientFactory, [FromServices] ILogger<Program> logger, [FromServices] IConfiguration configuration) =>
{
    var client = clientFactory.CreateClient();

    var response = await client.GetAsync(configuration["Services:DownstreamApi:BaseAddress"]);
    var content = await response.Content.ReadAsStringAsync();
    
    logger.LogInformation("Received content from downstream service: {content}", content);
    
    return content;
})
.WithName("GetDownstreamContent")
.WithOpenApi();

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
