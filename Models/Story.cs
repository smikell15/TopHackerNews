using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopHackerNews.Models
{
    public class Story
    {
        public int id { get; set; }
        public string by { get; set; }
        public int score { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }
}
