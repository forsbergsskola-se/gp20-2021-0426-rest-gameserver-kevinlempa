using System;
using System.Collections.Generic;
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
            foreach (var comment in comments) {
                list.Add(comment);
            }
            return list;
        }

        public string Name { get; }
        public string Description { get; }
    }
}