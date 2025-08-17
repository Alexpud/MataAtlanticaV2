using MataAtlanticaV2.OpenTelemetry;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using SerilogTracing;
using System.Diagnostics;

namespace MataAtlanticaV2.Apis;

public static class WeatherForecastApi
{
    public static void AddWeatherForecastApi(this WebApplication app)
    {
        app.MapGet("/weatherforecast", (
            [FromServices] ILogger<WeatherForecast> logger,
            [FromServices] ActivitySourceWrapper activitySource) =>
        {
            // Adiciona mais informações ao TRACE criado para a requisição
            Activity.Current?.SetTag("teste", 1);
            //Activity.Current.AddEvent(new ActivityEvent("Começando o role"));
            using var activity = activitySource.GetActivitySource().StartActivity("GetWeatherForecast");

            logger.LogInformation("Message={Message};", "Beggining weather forecast");
            activity?.AddTag("teste", 1);

            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            logger.LogInformation("Message={Message};", "Ending weather forecast");

            using var secondActivity = activitySource.GetActivitySource().StartActivity("Second activity");
            logger.LogInformation("Message={Message};", "Logging second activity");

            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    }
}

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

