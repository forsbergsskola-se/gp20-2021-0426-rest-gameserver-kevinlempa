using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GitHubExplorer {
    class Program {
        static async Task Main(string[] args) {
            var gitHubApi = new GitHubApi();
            Console.WriteLine("Enter a user name");
            var username = Console.ReadLine();
            var user = gitHubApi.GetUser(username);
            Console.WriteLine("Enter Repository:");
            var repositories = Console.ReadLine();
            var repo = user.GetRepository(repositories);
            Console.WriteLine($"-----------{repo.Name}-----------\r\n{repo.Description}");
            var issues = repo.GetIssues();
            if (issues.Count == 0) {
                Console.WriteLine("No Issues found.");
            }

            Console.WriteLine($"To create a new issue, type : create\r\n" +
                              $"To view comments or update an issue type the issue number.");

            var choose = Console.ReadLine();
            if ("create".Equals(choose, StringComparison.InvariantCultureIgnoreCase)) {
                repo.CreateIssue();
            }

            var comments = new List<IComment>();
            ChooseOption(issues, choose, out comments);
        }

        private static void ChooseOption(List<IIssue> issues, string choose, out List<IComment> comments) {
            foreach (var issue in issues) {
                if (Int32.TryParse(choose, out var x)) {
                    if (issue.Number == x) {
                        comments = issue.GetComments();
                        Console.WriteLine($"To create a new comment, type : create\r\n" +
                                          $"To update the issue, type : update\r\n" +
                                          "To delete a comment, type : delete\r\n" +
                                          "To patch a comment, type : patch\r\n" +
                                          "To quit type : quit\r\n");


                        var choice = Console.ReadLine();
                        if (choice.Equals("patch", StringComparison.InvariantCultureIgnoreCase)) {
                            if (comments.Any()) {
                                Console.WriteLine("Type in a comment ID :");
                                var readline = Console.ReadLine();
                                if (Int32.TryParse(readline, out var id)) {
                                    foreach (var comment in comments) {
                                        if (comment.Id == id) {
                                            comment.PatchComment();
                                            return;
                                        }
                                    }
                                }
                            } else Console.WriteLine("No comments available to patch.");
                        }

                        if (choice.Equals("create", StringComparison.InvariantCultureIgnoreCase)) {
                            issue.CreateComment();
                            return;
                        }

                        if (choice.Equals("update", StringComparison.InvariantCultureIgnoreCase)) {
                            issue.UpdateIssue();
                            return;
                        }
                        if (choice.Equals("quit", StringComparison.InvariantCultureIgnoreCase)) {
                            return;
                        }

                        if (choice.Equals("delete", StringComparison.InvariantCultureIgnoreCase)) {
                            if (comments.Any()) {
                                Console.WriteLine("Type in a comment ID :");
                                var readline = Console.ReadLine();
                                if (Int32.TryParse(readline, out var id)) {
                                    foreach (var comment in comments) {
                                        if (comment.Id == id) {
                                            comment.DeleteComment();
                                            return;
                                        }
                                    }
                                }
                            } else {
                                Console.WriteLine("No comments available to delete.");
                            }
                        }
                    }
                }
            }
            comments = new List<IComment>();
        }
    }
}