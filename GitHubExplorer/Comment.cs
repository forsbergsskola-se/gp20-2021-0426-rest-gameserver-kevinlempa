using System;
using System.Net;
using System.Net.Http.Json;

namespace GitHubExplorer {
    public class Comment : IComment {
        public void PatchComment() {
            
            Console.WriteLine("Enter a Body :");
            var body = Console.ReadLine();
            var newComment = new Comment();
            newComment.Body = body;
            var response =GitHubApi.client.PostAsJsonAsync(Url, newComment).Result;
            if (response.StatusCode == HttpStatusCode.OK) {
                Console.WriteLine("Successfully patched comment");
            }else Console.WriteLine("Could not patch comment");
        }

        public int Id { get; set; }

        public void DeleteComment() {
            var respone = GitHubApi.client.DeleteAsync(Url).Result;
            if (respone.StatusCode == HttpStatusCode.NoContent) {
                Console.WriteLine("Successfully deleted comment");
            }else Console.WriteLine("Could not delete comment");
        }


        public string Body { get; set; }
        public Uri Url { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Comments_url { get; set; }
        public string Name { get; }
        public string Description { get; }
    }
}