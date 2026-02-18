namespace HackerNews.Api.Clients
{
    using HackerNews.Api.Models;

    public interface IHackerNewsClient
    {
        Task<List<long>> GetBestStoryIdsAsync();
        Task<HackerNewsItem?> GetItemAsync(long id);
    }
}
