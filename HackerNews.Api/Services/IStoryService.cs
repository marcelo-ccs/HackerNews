namespace HackerNews.Api.Services
{
    using HackerNews.Api.Models;

    public interface IStoryService
    {
        Task<List<StoryDto>> GetBestStoriesAsync(int n);
    }
}
