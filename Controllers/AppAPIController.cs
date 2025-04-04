using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppAPIController : ControllerBase
    {
        HttpClient _httpClient;
        public static WebApplicationBuilder _builder;
        public static ILogger _logger;
        public AppAPIController(ILogger logger):base()
        {
            _httpClient = new HttpClient();
            _logger = logger;
        }
        // GET: api/<AppAPIController>
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return new string[] { await Get("cape town"), await Get("new york") };
        }

        // GET api/<AppAPIController>/5
        [HttpGet("{city_name}")]
        public async Task<string> Get(string city_name)
        {
            try
            {
                if (_builder == null)
                {
                    throw new InvalidOperationException("WebApplicationBuilder is not initialized.");
                }
                var key = _builder.Configuration.GetSection("AppSettings")["WeatherApiKey"] ?? throw new InvalidOperationException("WeatherApiKey is not configured.");
                _logger.LogInformation("WeatherApiKey: " + key);
                var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city_name}&appid={key}";
                HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;
                
                response.EnsureSuccessStatusCode(); // Throws exception if response is not successful
                string resp = await response.Content.ReadAsStringAsync();

                _logger.LogInformation("Response from API: " + resp);
                Console.WriteLine("Response: " + resp);
                return resp;
            }
            catch (Exception e)
            {
                return "{\"error\": \""+e.Message+"\"}";
            }
        }
        
    }
}
