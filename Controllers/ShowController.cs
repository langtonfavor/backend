using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using backend.Models;

namespace backend.Controllers
{
    [Route("api/shows")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ShowsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("search/{query}")]
        public async Task<IActionResult> SearchShows(string query)
        {
            
            var omdbApiKey = "dd53f34293068294e9be03e8253d59c7";
            var omdbApiBaseUrl = "http://www.omdbapi.com/";

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetStringAsync($"{omdbApiBaseUrl}?apikey={omdbApiKey}&s={query}&type=series");

            var data = JObject.Parse(response);

            if (data["Search"] != null)
            {
                var shows = data["Search"].ToObject<Show[]>();
                return Ok(shows);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("show/{id}")]
        public async Task<IActionResult> GetShowDetails(string id)
        {
            var omdbApiKey = "dd53f34293068294e9be03e8253d59c7";
            var omdbApiBaseUrl = "http://www.omdbapi.com/";

            var httpClient = _httpClientFactory.CreateClient();

            var response = await httpClient.GetStringAsync($"{omdbApiBaseUrl}?apikey={omdbApiKey}&i={id}");

            var showDetails = JObject.Parse(response);

            return Ok(showDetails);
        }
    }
}
