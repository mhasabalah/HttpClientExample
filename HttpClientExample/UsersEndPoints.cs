using Microsoft.Extensions.Options;

namespace HttpClientExample;

public static class UsersEndPoints
{
    public static void MapUsersEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/users/v1/{username}", async (
            string username,
            IOptions<GitHubSettings> settings) =>
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", settings.Value.GitHubToken);
            client.DefaultRequestHeaders.Add("User-Agent", settings.Value.UserAgent);
            client.BaseAddress = new Uri("https://api.github.com");

            var content = await client.GetFromJsonAsync<GitHubUser>($"users/{username}");

            return Results.Ok(content);
        });

        app.MapGet("/users/v2/{username}", async (
            string username,
            IHttpClientFactory factory,
            IOptions<GitHubSettings> settings) =>
        {
            var client = factory.CreateClient();

            client.DefaultRequestHeaders.Add("Authorization", settings.Value.GitHubToken);
            client.DefaultRequestHeaders.Add("User-Agent", settings.Value.UserAgent);
            client.BaseAddress = new Uri("https://api.github.com");

            var content = await client.GetFromJsonAsync<GitHubUser>($"users/{username}");
            return Results.Ok(content);
        });

        app.MapGet("/users/v3/{username}", async (
            string username,
            IHttpClientFactory factory) =>
        {
            var client = factory.CreateClient();

            var content = await client.GetFromJsonAsync<GitHubUser>($"users/{username}");
            
            return Results.Ok(content);
        });

        app.MapGet("/users/v4/{username}", async (
            string username,
            GitHubService service) =>
        {
            GitHubUser? content = await service.GetUserAsync(username);

            return Results.Ok(content);
        });
    }
}
