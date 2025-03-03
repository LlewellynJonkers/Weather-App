using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Weather_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppAPIController : ControllerBase
    {
        // GET: api/<AppAPIController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AppAPIController>/5
        [HttpGet("{city_name}")]
        public string Get(string city_name)
        {
            return "value";
        }

        
    }
}
