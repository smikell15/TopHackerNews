using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TopHackerNews.Models;
using TopHackerNews.Services;
using Xunit;

namespace TopHackerNews.Tests
{
    public class TestHackerNewsService
    {
        
        private List<Story> testStories = new List<Story>()
            {
                new Story() { id = 1, by = "by1", score = 1, title = "title1", url = "url1" },
                new Story() { id = 2, by = "by2", score = 1, title = "title2", url = "url2" },
                new Story() { id = 3, by = "by3", score = 1, title = "title3", url = "url3" },
                new Story() { id = 4, by = "by4", score = 1, title = "title4", url = "url4" },
                new Story() { id = 5, by = "by5", score = 1, title = "title5", url = "url5" },
                new Story() { id = 6, by = "by6", score = 1, title = "title6", url = "url6" },
                new Story() { id = 7, by = "by7", score = 1, title = "title7", url = "url7" },
                new Story() { id = 8, by = "by8", score = 1, title = "title8", url = "url8" },
                new Story() { id = 9, by = "by9", score = 1, title = "title9", url = "url9" },
                new Story() { id = 10, by = "by10", score = 1, title = "title10", url = "url0" },
                new Story() { id = 11, by = "by11", score = 1, title = "title11", url = "url1" },
                new Story() { id = 12, by = "by12", score = 1, title = "title12", url = "url2" },
                new Story() { id = 13, by = "by13", score = 1, title = "title13", url = "url3" },
                new Story() { id = 14, by = "by14", score = 1, title = "title14", url = "url4" },
                new Story() { id = 15, by = "by15", score = 1, title = "title15", url = "url5" },
                new Story() { id = 16, by = "by16", score = 1, title = "title16", url = "url6" },
                new Story() { id = 17, by = "by17", score = 1, title = "title17", url = "url7" },
                new Story() { id = 18, by = "by18", score = 1, title = "title18", url = "url8" },
                new Story() { id = 19, by = "by19", score = 1, title = "title17", url = "url9" },
                new Story() { id = 20, by = "by20", score = 1, title = "title20", url = "ur20" }
            };

        [Fact]
        public void GetBestIDs_ReturnsValid()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(testStories.Select(s => s.id)), Encoding.UTF8, "application/json"),
                })
                .Verifiable();

            var client = new HttpClient(handlerMock.Object);
            client.BaseAddress = new Uri("http://test.test");
            var service = new HackerNewsService();
            service.Client = client;

            var result = service.GetBestIDs();

            Assert.True(result != null);
            Assert.Equal(result.Count(), testStories.Count());
        }

        [Fact]
        public void GetStoriesByID_ReturnsValid()
        {
            var testId = 1;
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(testStories.Where(s => s.id == testId).First()), Encoding.UTF8, "application/json"),
                })
                .Verifiable();

            var client = new HttpClient(handlerMock.Object);
            client.BaseAddress = new Uri("http://test.test");
            var service = new HackerNewsService();
            service.Client = client;

            var result = service.GetStoriesByID(new List<int>() { testId });

            Assert.True(result != null);
            Assert.True(result.Count() == 1);
            Assert.Equal(result.First().id, testId);
        }

        [Fact]
        public void GetStoriesByID_InvalidIDReturnsValid()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                      "SendAsync",
                      ItExpr.IsAny<HttpRequestMessage>(),
                      ItExpr.IsAny<CancellationToken>()
                    )
                .ReturnsAsync(new HttpResponseMessage()
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(string.Empty)
                })
                .Verifiable();

            var client = new HttpClient(handlerMock.Object);
            client.BaseAddress = new Uri("http://test.test");
            var service = new HackerNewsService();
            service.Client = client;

            var result = service.GetStoriesByID(new List<int>() { 21 });

            Assert.True(result != null);
            Assert.True(result.First() == null);
        }
    }
}
