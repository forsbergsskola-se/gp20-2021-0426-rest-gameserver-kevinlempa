using System;
using System.Collections.Generic;
using System.Text.Json;

namespace GitHubExplorer {
    public class Repository : IRepository {
        public string Name { get; set; }
        public string Html_url { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Issues_url { get; set; }
        public List<IIssue> GetIssues() {
            var repoInfo = GitHubApi.client.GetStringAsync(Issues_url).Result;
            var issues = JsonSerializer.Deserialize<List<Issue>>(repoInfo, GitHubApi.options);
            Console.WriteLine("Issues:\r\n");
            var list = new List<IIssue>();
            foreach (var issue in issues) {
                list.Add(issue);
                Console.WriteLine($"Title : {issue.Title}\r\n" +
                                  $"Id : {issue.Number}\r\n");
            }
            return list;
        }
        
    }
}