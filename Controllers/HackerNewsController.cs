using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TopHackerNews.Models;
using TopHackerNews.Services;

namespace TopHackerNews.Controllers
{
    [Route("api/[controller]")]
    public class HackerNewsController : Controller
    {
        private IHackerNewsService _service;

        public HackerNewsController (IHackerNewsService service)
        {
            _service = service;
        }

        [HttpGet("bestStories")]
        public IEnumerable<Story> GetBestStories()
        {
            return _service.GetBest();
        }

        [HttpGet("allStories")]
        public IEnumerable<Story> GetAllStories()
        {
            return null;
        }
    }
}
