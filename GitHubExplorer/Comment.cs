using System;
using System.Net.Http.Json;

namespace GitHubExplorer {
    public class Comment : IComment {
        public void PatchComment(string comment) {
            var newComment = new Comment();
            newComment.Body = comment;
            GitHubApi.client.PostAsJsonAsync(Url, newComment);
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