using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopHackerNews.Models;

namespace TopHackerNews.Controllers
{
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        [HttpGet("bestStories")]
        public IEnumerable<Story> GetBestStories()
        {
            IList<Story> topStories = new List<Story>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
                var responseTask = client.GetAsync("beststories.json");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<int>>();
                    readTask.Wait();

                    var topIDs = readTask.Result;

                    foreach (int id in topIDs)
                    {
                        var addressSuffix = string.Format("item/{0}.json", id);
                        var innerResponseTask = client.GetAsync(addressSuffix);
                        var innerResult = innerResponseTask.Result;

                        if (innerResult.IsSuccessStatusCode)
                        {
                            var innerReadTask = innerResult.Content.ReadAsAsync<Story>();
                            innerReadTask.Wait();

                            topStories.Add(innerReadTask.Result);
                        }

                    }
                    topStories.OrderByDescending(story => story.score);
                }
            }

            return topStories;
        }

        [HttpGet("allStories")]
        public IEnumerable<Story> GetAllStories()
        {
            return null;
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
