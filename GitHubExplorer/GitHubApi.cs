using System;
using System.Net.Http;
using System.Text.Json;

namespace GitHubExplorer {
    public class GitHubApi : IGitHubAPI {
        public static HttpClient client;
        public static JsonSerializerOptions options;

        public GitHubApi() {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            Console.WriteLine("Enter auth token");
            var token = Console.ReadLine();

            client.DefaultRequestHeaders.UserAgent.Add(
                new System.Net.Http.Headers.ProductInfoHeaderValue("GitHubExplorer", "0.1"));
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);
        }

        public IUser GetUser(string userName) {
            var responseText = "";
            responseText = client.GetStringAsync($"https://api.github.com/users/{userName}").Result;
            options = new JsonSerializerOptions();
            options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            var user = JsonSerializer.Deserialize<GitHubUser>(responseText, options);
            Console.WriteLine(user);
            return user;
        }
    }
}