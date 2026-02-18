# HackerNews API

A RESTful API built with .NET 10 that fetches and serves the best stories from Hacker News. The API includes caching, resilience patterns, and configurable settings for optimal performance.

## 🚀 Features

- **Best Stories Endpoint**: Retrieve the top N best stories from Hacker News
- **In-Memory Caching**: Configurable cache duration to reduce API calls
- **Resilience Patterns**: Built-in retry logic and circuit breaker using Polly
- **Concurrent Processing**: Efficiently fetches multiple stories in parallel
- **OpenAPI Support**: Interactive API documentation in development mode
- **Configurable Limits**: Control max stories, concurrency, and cache settings

## 🛠️ Technologies

- **.NET 10**: Latest .NET framework
- **ASP.NET Core Web API**: RESTful API framework
- **HttpClient with Resilience**: Standard resilience handler with retry and circuit breaker
- **Memory Cache**: Built-in distributed caching
- **OpenAPI/Swagger**: API documentation

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- A code editor (e.g., Visual Studio 2022, VS Code, or Rider)

## 🔧 Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/marcelo-ccs/HackerNews.git
   cd HackerNews
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Build the project**
   ```bash
   dotnet build
   ```

4. **Run the application**
   ```bash
   cd HackerNews.Api
   dotnet run
   ```

The API will start and be available at `https://localhost:5001` (or the port shown in the console).

## ⚙️ Configuration

Configure the application by modifying `appsettings.json`:

```json
{
  "HackerNews": {
    "BaseUrl": "https://hacker-news.firebaseio.com/v0/",
    "CacheDurationSeconds": 300,
    "MaxConcurrency": 10,
    "MaxStoriesLimit": 100
  }
}
```

### Configuration Options

| Option | Description | Default |
|--------|-------------|---------|
| `BaseUrl` | Hacker News API base URL | `https://hacker-news.firebaseio.com/v0/` |
| `CacheDurationSeconds` | Cache expiration time in seconds | `300` (5 minutes) |
| `MaxConcurrency` | Maximum concurrent requests to Hacker News API | `10` |
| `MaxStoriesLimit` | Maximum number of stories that can be requested | `100` |

## 📚 API Endpoints

### Get Best Stories

Retrieves the best stories from Hacker News.

**Endpoint**: `GET /api/stories/best`

**Query Parameters**:
- `n` (optional, default: 10): Number of stories to retrieve (max: configured `MaxStoriesLimit`)

**Example Request**:
```bash
curl -X GET "https://localhost:5001/api/stories/best?n=20"
```

**Example Response**:
```json
[
  {
    "title": "Show HN: My New Project",
    "uri": "https://example.com/project",
    "postedBy": "username",
    "time": "2024-01-15T10:30:00Z",
    "score": 250,
    "commentCount": 42
  },
  ...
]
```

## 🏗️ Project Structure

```
HackerNews.Api/
├── Clients/
│   ├── IHackerNewsClient.cs   # HN API client interface
│   └── HackerNewsClient.cs    # HN API client implementation
├── Configuration/
│   └── HackerNewsOptions.cs   # Configuration options
├── Controllers/
│   └── StoriesController.cs   # API endpoints
├── Models/
│   └── StoryDto.cs            # Story data transfer object
├── Services/
│   ├── IStoryService.cs       # Story service interface
│   └── StoryService.cs        # Story service implementation
├── appsettings.json           # App configuration
└── Program.cs                 # App entry point
```

## 🔄 Resilience Patterns

The API implements resilience patterns using the standard resilience handler:

- **Retry Policy**: Automatically retries failed requests up to 3 times
- **Circuit Breaker**: Prevents cascading failures with a 30-second sampling duration

## 🧪 Development

### Running in Development Mode

When running in development mode, OpenAPI documentation is automatically available:

1. Run the application
2. Navigate to the OpenAPI endpoint (typically `/openapi/v1.json`)
3. Use tools like Swagger UI or Postman to explore the API

### Building for Production

```bash
dotnet publish -c Release -o ./publish
```

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 👤 Author

**Marcelo Silva**

- GitHub: [@marcelo-ccs](https://github.com/marcelo-ccs)

## 🤝 Contributing

Contributions, issues, and feature requests are welcome!

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📧 Support

If you have any questions or need help, please open an issue in the GitHub repository.

## 🔮 Potential Improvements (With More Time)

If evolving this into a production microservice:

### Infrastructure

- Replace `IMemoryCache` with Redis (distributed cache)
- Dockerize the application

### Resilience

- Global rate limiting middleware
- Timeout policies

### Observability

- Structured logging (Serilog)

### Testing

- Unit tests (xUnit + Moq)
- Integration tests (WebApplicationFactory)

---




