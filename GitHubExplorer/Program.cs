using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace GitHubExplorer {
    class Program {
        static async Task Main(string[] args) {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.github.com");
            Console.WriteLine("Enter auth token");
            var token = Console.ReadLine();

            client.DefaultRequestHeaders.UserAgent.Add(
                new System.Net.Http.Headers.ProductInfoHeaderValue("GitHubExplorer", "0.1"));
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Token", token);

            var response = await client.GetAsync("/user");
            Console.WriteLine(response);
            var responseText = "";
            // Console.WriteLine("Enter a user name");
            var username = "kevinlempa";
            try {
                responseText = client.GetStringAsync($"https://api.github.com/users/{username}").Result;
                Console.WriteLine(responseText);
                var option = new JsonSerializerOptions();
                option.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                var user = JsonSerializer.Deserialize<GitHubUser>(responseText, option);

                Console.WriteLine(user);

                var r = client.GetStringAsync($"{user.Repos_url}").Result;
                Console.WriteLine(r);
                var l =  JsonSerializer.Deserialize<List<Repo>>(r,option);
                foreach (var repo in l) {
                    var index = repo.Issues_url.IndexOf("{");
                   repo.Issues_url = repo.Issues_url.Remove(index);
                    Console.WriteLine(repo.Issues_url);
                }
                Console.WriteLine(l[0].Issues_url);
                var repoInfo = client.GetStringAsync(l[0].Issues_url).Result;
                var issues = JsonSerializer.Deserialize<List<Issue>>(repoInfo,option);
                Console.WriteLine(repoInfo);
                foreach (var issue in issues) {
                    Console.WriteLine($"Title : {issue.Title}\r\nInfo :{issue.Body}");
                }

                var commentJSon = client.GetStringAsync(issues[1].Comments_url).Result;
                var comments = JsonSerializer.Deserialize<List<Issue>>(commentJSon, option);
                Console.WriteLine(commentJSon);
                foreach (var issue in comments) {
                    Console.WriteLine($"Title : {issue.Title}\r\nInfo :{issue.Body}\r\n");
                }

                ;


            }
            catch (Exception e) {
                throw new Exception("Get request failed. @ \r\n " + e);
            }
        }
    }

    public class Issue {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Comments_url { get; set; }
    }
  
    public class Repo {
        public string Name { get; set;}
        public string Html_url { get; set;}
        public string Url { get; set;}
        public string Description { get; set;}
        public string Issues_url { get; set;}
    }

    public class GitHubUser {
        public string Login { get; set; }
        public string Repos_url { get; set; }
        public string Followers_url { get; set; }
        public string Organinzations_url { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public string Hireable { get; set; }
        public string Bio { get; set; }
        public int Public_repos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }

        private string PrintToConsole() {
            if (string.IsNullOrEmpty(Company)) {
                Company = "Not available";
            }

            if (string.IsNullOrEmpty(Location)) {
                Location = "Not available";
            }

            if (string.IsNullOrEmpty(Email)) {
                Email = "Not available";
            }

            if (string.IsNullOrEmpty(Blog)) {
                Blog = "Not available";
            }

            if (string.IsNullOrEmpty(Hireable)) {
                Hireable = "Unknown";
            }

            if (string.IsNullOrEmpty(Bio)) {
                Bio = "Not available";
            }

            return $"User : {Login}\r\n" +
                   $"Location : {Location}\r\n" +
                   $"Email : {Email}\r\n" +
                   $"Hireable : {Hireable}\r\n" +
                   $"Blog : {Blog}\r\n" +
                   $"Bio : {Bio}\r\n" +
                   $"Public Repos : {Public_repos}" +
                   $"Followers : {Followers}\r\n" +
                   $"Following : {Following}\r\n" +
                   $"Created at : {Created_at}\r\n" +
                   $"Updated at : {Updated_at}";
        }

        public override string ToString() {
            return PrintToConsole();
        }
    }
 
}