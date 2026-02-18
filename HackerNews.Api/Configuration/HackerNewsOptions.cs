namespace HackerNews.Api.Configuration
{
    public class HackerNewsOptions
    {
        public string BaseUrl { get; set; }
        public int CacheDurationSeconds { get; set; }
        public int MaxConcurrency { get; set; }
        public int MaxStoriesLimit { get; set; }
    }
}
