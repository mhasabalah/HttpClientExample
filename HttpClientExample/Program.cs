using HttpClientExample;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<GitHubSettings>()
    .BindConfiguration(GitHubSettings.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

// named client
//builder.Services.AddHttpClient("github", (serviceProvider, client) =>
//{
//    GitHubSettings? settings = serviceProvider
//        .GetRequiredService<IOptions<GitHubSettings>>().Value;

//    client.DefaultRequestHeaders.Add("Authorization", settings.GitHubToken);
//    client.DefaultRequestHeaders.Add("User-Agent", settings.UserAgent);

//    client.BaseAddress = new Uri("https://api.github.com/");
//});

// typed client

builder.Services.AddHttpClient<GitHubService>((serviceProvider, client) =>
{
   var settings = serviceProvider
       .GetRequiredService<IOptions<GitHubSettings>>().Value;

   client.DefaultRequestHeaders.Add("Authorization", settings.GitHubToken);
   client.DefaultRequestHeaders.Add("User-Agent", settings.UserAgent);

   client.BaseAddress = new Uri("https://api.github.com");
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
   return new SocketsHttpHandler()
   {
       PooledConnectionLifetime = TimeSpan.FromMinutes(15)
   };
})
.SetHandlerLifetime(Timeout.InfiniteTimeSpan);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();