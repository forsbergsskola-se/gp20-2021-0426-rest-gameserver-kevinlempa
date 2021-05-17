using System;
using System.Collections.Generic;

namespace GitHubExplorer {
    public interface IIssue {
        List<IComment> GetComments();
        string Name { get; }
        string Description { get; }
        public string Title { get; set; }
        public int Number { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }
        public string Comments_url { get; set; }
    }
}