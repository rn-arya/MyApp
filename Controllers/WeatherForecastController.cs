using Microsoft.AspNetCore.Mvc;

namespace MyApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MyAppController : ControllerBase
{    
    private readonly ILogger<MyAppController> _logger;
    private readonly IHttpClientFactory _httpClientFactory;

    public MyAppController(ILogger<MyAppController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var httpClient = _httpClientFactory.CreateClient("TestAPI");
        var response = await httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("api/WeatherForecast");
        return response ?? Enumerable.Empty<WeatherForecast>();
    }
}
