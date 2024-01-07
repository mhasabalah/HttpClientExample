namespace HttpClientExample;

public  class GitHubSettings
{
    public  string UserAgent { get; set; } = null!;

    public  string GitHubToken { get; set; } = null!;
    public static string ConfigurationSection { get; } = "GitHubSettings";
}