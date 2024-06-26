using EasySoft.Simple.Single.Application.Common;
using EasySoft.UtilityTools.Core.Results.Implements;
using EasySoft.UtilityTools.Core.Results.Interfaces;

namespace EasySoft.Simple.Single.Application.Controllers;

/// <summary>
/// WeatherForecast
/// </summary>
[ApiController]
[Route("Weather")]
public class WeatherForecastController : ControllerCore
{
    private static readonly string[] Summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    /// <summary>
    /// WeatherForecast
    /// </summary>
    /// <param name="logger"></param>
    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns></returns>
    [HttpGet("get", Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5)
            .Select(
                index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }
            )
            .ToArray();
    }

    /// <summary>
    /// Get1
    /// </summary>
    /// <returns></returns>
    [Description("获取天气")]
    [ApiVersion("1.0")]
    [HttpGet("get1", Name = "GetWeatherForecast1")]
    public IEnumerable<WeatherForecast> Get1()
    {
        return Enumerable.Range(1, 5)
            .Select(
                index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }
            )
            .ToArray();
    }

    /// <summary>
    /// Get3
    /// </summary>
    /// <returns></returns>
    [HttpGet("get3", Name = "GetWeatherForecast3")]
    public IApiResult Get3()
    {
        var a = this.ParamAsync("a");

        var o = ReturnCode.Ok.ToMessage();

        return new ApiResult(
            ReturnCode.Ok,
            o.Success,
            o.Message
        )
        {
            Data = new
            {
                value = a
            }
        };
    }

    /// <summary>
    /// Get4
    /// </summary>
    /// <returns></returns>
    [HttpGet("get4", Name = "GetWeatherForecast4")]
    public IApiResult Get4()
    {
        var a = this.ParamAsync("a", 0);

        return this.Success(
            new
            {
                value = a
            }
        );
    }
}