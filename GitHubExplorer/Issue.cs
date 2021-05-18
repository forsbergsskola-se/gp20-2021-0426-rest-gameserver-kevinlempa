using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace GitHubExplorer {
    public class Issue : IIssue {
        public string Title { get; set; }
        public int Number { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Comments_url { get; set; }

        public List<IComment> GetComments() {
            var commentJSon = GitHubApi.client.GetStringAsync(Comments_url).Result;
            var comments = JsonSerializer.Deserialize<List<Comment>>(commentJSon, GitHubApi.options);
            var list = new List<IComment>();
            Console.WriteLine("-----------Comments-----------");
            if (!comments.Any()) {
                Console.WriteLine("-----------No comments found-----------");
            }
            foreach (var comment in comments) {
                Console.WriteLine($"Comment : {comment.Body}\r\n" +
                                  $"Created at : {comment.Created_at}\r\n" +
                                  $"ID : {comment.Id}\r\n" +
                                  $"----------------------");
                list.Add(comment);
            }
            return list;
        }
        
        public void UpdateIssue() {
            Console.WriteLine("Title :");
            var title = Console.ReadLine();
            Console.WriteLine("Body :");
            var body = Console.ReadLine();
            var newIssue = new Issue();
            newIssue.Title = title;
            newIssue.Body = body;
            var x = GitHubApi.client.PostAsJsonAsync(Url, newIssue).Result;
            if (x.StatusCode == HttpStatusCode.OK) {
                Console.WriteLine("Issue successfully updated.");
            }else Console.WriteLine("Something went wrong, Issue was not updated.");
        }
        public void CreateComment() {
            Console.WriteLine("Write your comment :");
            var comment = Console.ReadLine();
            var newComment = new Comment();
            newComment.Body = comment;
            var response = GitHubApi.client.PostAsJsonAsync(Comments_url, newComment).Result;
            if (response.StatusCode == HttpStatusCode.Created) {
                Console.WriteLine("Comment successfully created.");
            }else Console.WriteLine("Could not create comment.");
            
        }

        public Uri Url { get; set; }

        public string Name { get; }
        public string Description { get; }
    }
}