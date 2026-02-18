using HackerNews.Api.Clients;
using HackerNews.Api.Configuration;
using HackerNews.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<HackerNewsOptions>(
    builder.Configuration.GetSection("HackerNews"));

builder.Services.AddMemoryCache();

builder.Services.AddHttpClient<IHackerNewsClient, HackerNewsClient>()
    .AddStandardResilienceHandler(options =>
    {
        options.Retry.MaxRetryAttempts = 3;
        options.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(30);
    });

builder.Services.AddScoped<IStoryService, StoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
