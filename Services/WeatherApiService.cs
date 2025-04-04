namespace Weather_App.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IWebHostEnvironment _env;
        private readonly WebApplicationBuilder _builder;

        public WeatherApiService(WebApplicationBuilder builder)
        {
            _builder = builder ?? throw new ArgumentNullException(nameof(builder));
            _httpClient = new HttpClient();
        }

        public async Task<string> GetWeatherData(string city)
        {
            try
            {
                var key = await GetKey();
                var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={key}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throws exception if response is not successful

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return $"error:{e.Message}";
            }
        }

        private async Task<string> GetKey()
        {
            var key = _builder.Configuration.GetSection("AppSettings").GetSection("WeatherApiKey").Value;
            return key ?? throw new InvalidOperationException("WeatherApiKey is not configured.");
        }
    }
}
