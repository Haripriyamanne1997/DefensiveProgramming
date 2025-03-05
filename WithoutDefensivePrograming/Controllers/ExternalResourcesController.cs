using Microsoft.AspNetCore.Mvc;

namespace WithoutDefensivePrograming.Controllers
{
    [Route("api/weather")]
    public class ExternalResourcesController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        private readonly string? ApiKey;
        public ExternalResourcesController(IConfiguration configuration)
        {
            Configuration= configuration;
            ApiKey=Configuration["ApiKey"];
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            try
            {
                using HttpClient client = new HttpClient();
                string url = $"https://api.weather.com/data?city={city}&apikey={ApiKey}";
                string response = await client.GetStringAsync(url);

                return Ok(response);
            }
            catch (UnauthorizedAccessException unAuthorisedException)
            {
                return StatusCode(401, "User is Unauthorized.");
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(503, "Weather service is unavailable.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }

}

