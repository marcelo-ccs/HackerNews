namespace HackerNews.Api.Controllers
{
    using HackerNews.Api.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/stories")]
    public class StoriesController : ControllerBase
    {
        private readonly IStoryService _service;

        public StoriesController(IStoryService service)
        {
            _service = service;
        }

        [HttpGet("best")]
        public async Task<IActionResult> GetBestStories([FromQuery] int n = 10)
        {
            var stories = await _service.GetBestStoriesAsync(n);
            return Ok(stories);
        }
    }

}
