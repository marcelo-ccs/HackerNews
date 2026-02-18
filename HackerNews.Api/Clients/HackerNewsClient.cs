namespace HackerNews.Api.Clients
{
    using HackerNews.Api.Configuration;
    using HackerNews.Api.Models;
    using Microsoft.Extensions.Options;

    public class HackerNewsClient: IHackerNewsClient
    {
        private readonly HttpClient _httpClient;
        private readonly HackerNewsOptions _options;

        public HackerNewsClient(HttpClient httpClient, IOptions<HackerNewsOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _httpClient.BaseAddress = new Uri(_options.BaseUrl);
        }

        public async Task<List<long>> GetBestStoryIdsAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<List<long>>("beststories.json");
            return response ?? new List<long>();
        }

        public async Task<HackerNewsItem?> GetItemAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<HackerNewsItem>($"item/{id}.json");
        }
    }
}
