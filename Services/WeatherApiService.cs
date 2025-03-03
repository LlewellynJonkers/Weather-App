namespace Weather_App.Services
{
    public class WeatherApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherApiService> _logger;
        private readonly IWebHostEnvironment _env;

        public WeatherApiService(HttpClient httpClient,IWebHostEnvironment env)//, ILogger<WeatherApiService> logger)
        {
            _httpClient = httpClient;
            _env = env;
            //_logger = logger;
        }
        public async Task<string> GetWeatherData(string city)
        {
            try
            {
                var key = await GetKey();
                var apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={key}";

                HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throws exception if response is not successful

                string content = await response.Content.ReadAsStringAsync();
                return content;
            }
            catch(Exception e)
            {
                return $"error:{e.Message}";
            }
        }
        private async Task<string> GetKey()
        {
            string filePath = Path.Combine(_env.WebRootPath, "key.txt");

            if (File.Exists(filePath))
            {
                return await File.ReadAllTextAsync(filePath);
            }
            else
            {
                throw new FileNotFoundException("The key.txt file not found!\nSearched in \n\t~\\key.txt");
            }
        }
    }
}
