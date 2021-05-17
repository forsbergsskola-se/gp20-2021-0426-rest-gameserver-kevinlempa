using System.Collections.Generic;

namespace GitHubExplorer {
    public interface IRepository {
        List<IIssue> GetIssues();
        string Name { get; }
        string Description { get; }
    }
}