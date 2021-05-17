using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GitHubExplorer {
    public class GitHubUser : IUser {
        public string Login { get; set; }
        public string Repos_url { get; set; }
        public string Followers_url { get; set; }
        public string Organinzations_url { get; set; }
        public string Company { get; set; }
        public string Blog { get; set; }

        public List<Repository> repos;

        public IRepository GetRepository(string repositoryName) {
            while (true) {
                foreach (var repo in repos) {
                    if (repo.Name.Equals(repositoryName, StringComparison.InvariantCultureIgnoreCase)) return repo;
                }
                
                Console.WriteLine("Repository not found try again :");
                repositoryName = Console.ReadLine();
            }
        }

        public string Name => Login;
        public string Location { get; set; }
        public string Email { get; set; }
        public string Hireable { get; set; }
        public string Bio { get; set; }
        public int Public_repos { get; set; }
        public int Followers { get; set; }
        public int Following { get; set; }
        public string Created_at { get; set; }
        public string Updated_at { get; set; }
        
        public override string ToString() {
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

            var r = GitHubApi.client.GetStringAsync($"{Repos_url}").Result;

            repos = JsonSerializer.Deserialize<List<Repository>>(r, GitHubApi.options);
            var repoList = "\r\n";
            foreach (var repo in repos) {
                var index = repo.Issues_url.IndexOf("{");
                repo.Issues_url = repo.Issues_url.Remove(index);
                repoList += $"Repository name : {repo.Name}\r\n";
            }

            return $"User : {Login}\r\n" +
                   $"Location : {Location}\r\n" +
                   $"Email : {Email}\r\n" +
                   $"Hireable : {Hireable}\r\n" +
                   $"Blog : {Blog}\r\n" +
                   $"Bio : {Bio}\r\n" +
                   $"Public Repos : {Public_repos}\r\n" +
                   $"Followers : {Followers}\r\n" +
                   $"Following : {Following}\r\n" +
                   $"Created at : {Created_at}\r\n" +
                   $"Updated at : {Updated_at}\r\n" +
                   $"Repositories : {repoList}";
        }
    }
}