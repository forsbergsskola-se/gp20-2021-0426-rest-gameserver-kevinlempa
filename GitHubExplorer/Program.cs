using System;
using System.Threading.Tasks;

namespace GitHubExplorer {
    class Program {
        static async Task Main(string[] args) {
            var gitHubApi = new GitHubApi();
            Console.WriteLine("Enter a user name");
            var username = Console.ReadLine();
            var user = gitHubApi.GetUser(username);
            Console.WriteLine("Enter Repository name :");
            var repositories = Console.ReadLine();
            var repo = user.GetRepository(repositories);
            Console.WriteLine(repo.Description);
            var issues = repo.GetIssues();
          if(issues.Count == 0)
            Console.WriteLine("No Issues found.");
          
        }
    }
}