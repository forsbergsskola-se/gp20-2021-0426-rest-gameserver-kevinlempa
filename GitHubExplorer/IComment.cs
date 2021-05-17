using System;

namespace GitHubExplorer {
    public interface IComment {
        public void PatchComment(string comment);
        public string Body { get; set; }
        public Uri Url { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Comments_url { get; set; }
        string Name { get; }
        string Description { get; }
    }
}