using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using TopHackerNews.Models;

namespace TopHackerNews.Services
{
    public interface IHackerNewsService
    {
        IEnumerable<Story> GetBest();
        IEnumerable<int> GetBestIDs();
        IEnumerable<Story> GetStoriesByID(IEnumerable<int> ids);

    }

    public class HackerNewsService : IHackerNewsService
    {
        public HttpClient Client { get; set; }

        public HackerNewsService()
        {
            if (Client == null)
            {
                Client = new HttpClient { BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/") };
            }
        }

        public IEnumerable<Story> GetBest()
        {
            return GetStoriesByID(GetBestIDs());
        }

        public IEnumerable<int> GetBestIDs()
        {
            IEnumerable<int> topIDs = new List<int>();
            var responseTask = Client.GetAsync("beststories.json");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<IList<int>>();
                readTask.Wait();

                if (readTask.Result != null)
                {
                    topIDs = readTask.Result;
                }
            }
            return topIDs;
        }

        public IEnumerable<Story> GetStoriesByID(IEnumerable<int> ids)
        {
            IList<Story> topStories = new List<Story>();

            foreach (int id in ids)
            {
                var addressSuffix = string.Format("item/{0}.json", id);
                var innerResponseTask = Client.GetAsync(addressSuffix);
                var innerResult = innerResponseTask.Result;

                if (innerResult.IsSuccessStatusCode)
                {
                    var innerReadTask = innerResult.Content.ReadAsAsync<Story>();
                    innerReadTask.Wait();

                    topStories.Add(innerReadTask.Result);
                }
            }

            topStories.OrderByDescending(story => story.score);
            return topStories;
        }
    }
}
