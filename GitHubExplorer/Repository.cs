using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace GitHubExplorer {
    public class Repository : IRepository {
        public string Name { get; set; }
        public string Html_url { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Issues_url { get; set; }

        public void CreateIssue() {
            Console.WriteLine("Title :");
            var title = Console.ReadLine();
            Console.WriteLine("Body :");
            var body = Console.ReadLine();
            var newIssue = new Issue();
            newIssue.Title = title;
            newIssue.Body = body;
            var x = GitHubApi.client.PostAsJsonAsync(Issues_url, newIssue).Result;
            if (x.StatusCode == HttpStatusCode.Created) {
                Console.WriteLine("Issue successfully created.");
            }else Console.WriteLine("Something went wrong, Issue was not created.");
        }

        public List<IIssue> GetIssues() {
            var issuesJson = GitHubApi.client.GetStringAsync(Issues_url).Result;
            var issues = JsonSerializer.Deserialize<List<Issue>>(issuesJson, GitHubApi.options);
            Console.WriteLine("-----------Issues-----------\r\n");
            var list = new List<IIssue>();
            foreach (var issue in issues) {
                list.Add(issue);
                Console.WriteLine($"Title : {issue.Title}\r\n" +
                                  $"Id : {issue.Number}\r\n" +
                                  $"----------------------");
            }
            return list;
        }
        
    }
}