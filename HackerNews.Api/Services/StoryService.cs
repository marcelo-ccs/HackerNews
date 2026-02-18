namespace HackerNews.Api.Services
{
    using HackerNews.Api.Clients;
    using HackerNews.Api.Configuration;
    using HackerNews.Api.Models;
    using Microsoft.Extensions.Caching.Memory;
    using Microsoft.Extensions.Options;

    public class StoryService : IStoryService
    {
        private readonly IHackerNewsClient _client;
        private readonly IMemoryCache _cache;
        private readonly HackerNewsOptions _options;
        private readonly SemaphoreSlim _semaphore;

        public StoryService(
                IHackerNewsClient client,
                IMemoryCache cache,
                IOptions<HackerNewsOptions> options)
        {
            _client = client;
            _cache = cache;
            _options = options.Value;
            _semaphore = new SemaphoreSlim(_options.MaxConcurrency);
        }

        public async Task<List<StoryDto>> GetBestStoriesAsync(int n)
        {
            if (n <= 0)
                throw new ArgumentException("n must be greater than zero");

            if (n > _options.MaxStoriesLimit)
                n = _options.MaxStoriesLimit;

            var ids = await _cache.GetOrCreateAsync("best_ids", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow =
                    TimeSpan.FromSeconds(_options.CacheDurationSeconds);

                return await _client.GetBestStoryIdsAsync();
            });

            var tasks = ids.Select(id => GetStoryWithThrottleAsync(id));

            var stories = await Task.WhenAll(tasks);

            return stories
                .OrderByDescending(s => s!.Score)
                .Take(n)
                .ToList()!;
        }

        private async Task<StoryDto?> GetStoryWithThrottleAsync(long id)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await _cache.GetOrCreateAsync($"story_{id}", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow =
                        TimeSpan.FromSeconds(_options.CacheDurationSeconds);

                    var item = await _client.GetItemAsync(id);
                    return Map(item);
                });
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private static StoryDto? Map(HackerNewsItem? item)
        {
            if (item == null || item.Type != "story")
                return null;

            return new StoryDto
            {
                Title = item.Title,
                Uri = item.Url,
                PostedBy = item.By,
                Time = DateTimeOffset
                    .FromUnixTimeSeconds(item.Time)
                    .UtcDateTime,
                Score = item.Score,
                CommentCount = item.Descendants
            };
        }
    }
}
