namespace HttpClientExample;
// typed client
public class GitHubService
{
    private readonly HttpClient _client;

    public GitHubService(HttpClient client)
    {
        _client = client;
    }

    public async Task<GitHubUser?> GetUserAsync(string username)
    {
        GitHubUser? user = await _client
            .GetFromJsonAsync<GitHubUser>($"users/{username}");

        return user;
    }
}

// named client
//public class GitHubService
//{
//    private readonly IHttpClientFactory _factory;

//    public GitHubService(IHttpClientFactory factory) => _factory = factory;

//    public async Task<GitHubUser?> GetUserAsync(string username)
//    {
//        var client = _factory.CreateClient("github");

//        GitHubUser? user = await client
//            .GetFromJsonAsync<GitHubUser>($"users/{username}");

//        return user;
//    }
//}

