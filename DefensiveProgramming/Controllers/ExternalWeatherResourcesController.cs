using Microsoft.AspNetCore.Mvc;

namespace DefensiveProgramming.Controllers
{
    [Route("api/weather")]
    public class ExternalWeatherResourcesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly string? ApiKey;
        private readonly HttpClient _httpClient;
        public ExternalWeatherResourcesController(IConfiguration configuration, HttpClient httpClient)
        {
            Configuration= configuration;
            ApiKey=Configuration["ApiKey"];
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            string url = $"https://api.weather.com/data?city={city}";

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return Ok(data);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Weather data unavailable");
                }
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(401, "User is Unauthorised.");
            }
            catch (TaskCanceledException)
            {
                return StatusCode(408, "Request timed out, please try again later");
            }
        }
    }

}

