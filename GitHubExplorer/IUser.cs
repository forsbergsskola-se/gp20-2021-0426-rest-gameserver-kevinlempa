namespace GitHubExplorer {
    public interface IUser {
        IRepository GetRepository(string repositoryName);
        string Name { get; }
        string Location { get; }
    }
}