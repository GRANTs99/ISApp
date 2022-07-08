using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using ISApi.Services;

namespace ISApi.Controllers
{
    [ApiController]
    public class SiteController : ControllerBase
    {

        [HttpGet("[controller]/GetCovid/{country}")]
        [Authorize]
        public async Task<IActionResult> Get(string country)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://covid-19-coronavirus-statistics.p.rapidapi.com/v1/total?country={country}"),
                Headers =
    {
        { "X-RapidAPI-Key", "78a3c2e194msh2bb428f348d3130p1f2813jsn83ae3737f26d" },
        { "X-RapidAPI-Host", "covid-19-coronavirus-statistics.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                return Ok(body);
            }
        }
        [HttpGet("[controller]/GetChuck")]
        [Authorize]
        public async Task<IActionResult> Get()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://matchilling-chuck-norris-jokes-v1.p.rapidapi.com/jokes/random"),
                Headers =
    {
        { "accept", "application/json" },
        { "X-RapidAPI-Key", "78a3c2e194msh2bb428f348d3130p1f2813jsn83ae3737f26d" },
        { "X-RapidAPI-Host", "matchilling-chuck-norris-jokes-v1.p.rapidapi.com" },
    },
            };
            string body;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                body = await response.Content.ReadAsStringAsync();
                string joke = JObject.Parse(body).GetValue("value").ToString();
                return Ok(joke);
            }
        }
    }
}
