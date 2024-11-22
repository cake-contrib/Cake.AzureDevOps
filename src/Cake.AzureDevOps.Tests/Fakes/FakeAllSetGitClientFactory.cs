namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Moq;

    public class FakeAllSetGitClientFactory : FakeGitClientFactory
    {
        public override GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<GitHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetPullRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), null, null, null, null, null, null, default))
                .ReturnsAsync((string _, string repoId1, int prId, int _, int _, int _, bool _, bool _, object _, CancellationToken _) => new GitPullRequest
                {
                    PullRequestId = prId,
                    Status = PullRequestStatus.Active,
                    Repository = new GitRepository
                    {
                        Id = Guid.NewGuid(),
                        Name = repoId1,
                    },
                    SourceRefName = "foo",
                    TargetRefName = "master",
                    CodeReviewId = 123,
                    LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" },
                    LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" },
                });

            mock.Setup(arg => arg.GetPullRequestsAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<GitPullRequestSearchCriteria>(),
                    null,
                    null,
                    1,
                    null,
                    default))
                .ReturnsAsync((string _, string repoId2, GitPullRequestSearchCriteria sc, int _, int _, int _, object _, CancellationToken _)
                    =>
                        [
                          new GitPullRequest
                            {
                                PullRequestId = 777,
                                Status = PullRequestStatus.Active,
                                Repository = new GitRepository
                                {
                                    Id = Guid.NewGuid(),
                                    Name = repoId2,
                                },
                                SourceRefName = sc.SourceRefName,
                                TargetRefName = "master",
                                CodeReviewId = 123,
                                LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" },
                                LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" },
                            },
                        ]);

            mock = this.Setup(mock);

            return mock.Object;
        }

        protected override Mock<GitHttpClient> Setup(Mock<GitHttpClient> m)
        {
            m.Setup(arg => arg.CreatePullRequestReviewerAsync(
                    It.Is<IdentityRefWithVote>(i => Enum.IsDefined(typeof(AzureDevOpsPullRequestVote), i.Vote)),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    default))
             .ReturnsAsync((IdentityRefWithVote identity, Guid _, int _, string _, object _, CancellationToken _)
                    => new IdentityRefWithVote
                    {
                        Vote = identity.Vote,
                    });

            m.Setup(arg => arg.CreatePullRequestReviewerAsync(
                    It.Is<IdentityRefWithVote>(i => !Enum.IsDefined(typeof(AzureDevOpsPullRequestVote), i.Vote)),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    default))
             .Throws(new Exception("Something went wrong"));

            m.Setup(arg => arg.CreatePullRequestStatusAsync(
                    It.IsAny<GitPullRequestStatus>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>()))
             .ReturnsAsync((GitPullRequestStatus status, Guid _, int _, object _, CancellationToken _)
                    => new GitPullRequestStatus
                    {
                        Context = status.Context,
                        State = status.State,
                    });

            // Setup CommitDiffs object
            var gitChanges = new List<GitChange>
            {
                new()
                {
                    ChangeId = 1,
                    ChangeType = VersionControlChangeType.Edit,
                    Item = new GitItem("/src/project/myclass.cs", "ID1", GitObjectType.Commit, "6b13ff8", 0),
                },
                null,
                new()
                {
                    ChangeId = 2,
                    ChangeType = VersionControlChangeType.Edit,
                    Item = new GitItem("/tools/folder", "ID2", GitObjectType.Tree, "6b13ff8", 0),
                },
            };

            var gitCommitDiffs = new GitCommitDiffs
            {
                ChangeCounts = new Dictionary<VersionControlChangeType, int> { { VersionControlChangeType.Edit, 2 } },
                Changes = gitChanges,
            };

            m.Setup(arg => arg.GetCommitDiffsAsync(
                    It.IsAny<string>(),
                    It.IsAny<Guid>(),
                    true,
                    null,
                    null,
                    It.IsAny<GitBaseVersionDescriptor>(),
                    It.IsAny<GitTargetVersionDescriptor>(),
                    null,
                    CancellationToken.None))
             .ReturnsAsync((string _, Guid _, bool? _, int? _, int? _, GitBaseVersionDescriptor _, GitTargetVersionDescriptor _, object _, CancellationToken _)
                    => gitCommitDiffs);

            m.Setup(arg => arg.UpdateThreadAsync(
                It.IsAny<GitPullRequestCommentThread>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                null,
                CancellationToken.None))
             .ReturnsAsync((GitPullRequestCommentThread prct, Guid _, int _, int thId, object _, CancellationToken _)
                    => new GitPullRequestCommentThread { Id = thId, Status = prct.Status });

            // Setup GitPullRequestCommentThread collection
            var commentThreads = new List<GitPullRequestCommentThread>
            {
                new()
                {
                    Id = 11,
                    ThreadContext = new CommentThreadContext()
                    {
                        FilePath = "/some/path/to/file.cs",
                    },
                    Comments =
                        [
                            new() { Content = "Hello", IsDeleted = false, CommentType = CommentType.CodeChange, Id = 1 },
                            new() { Content = "Goodbye", IsDeleted = true, CommentType = CommentType.Text, Id = 2 },
                        ],
                    Status = CommentThreadStatus.Active,
                },
                new()
                {
                    Id = 22,
                    ThreadContext = null,
                    Comments = [],
                    Status = CommentThreadStatus.Fixed,
                },
            };

            m.Setup(arg => arg.GetThreadsAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                null,
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync((Guid _, int _, int? _, int? _, object _, CancellationToken _)
                    => commentThreads);

            m.Setup(arg => arg.CreateThreadAsync(
                It.IsAny<GitPullRequestCommentThread>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                null,
                CancellationToken.None))
             .ReturnsAsync((GitPullRequestCommentThread prct, Guid _, int _, object _, CancellationToken _)
                    => prct);

            m.Setup(arg => arg.GetPullRequestIterationsAsync(
                It.IsAny<Guid>(),
                It.Is<int>(i => i != 13),
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync((Guid _, int _, bool? _, object _, CancellationToken _)
                    =>
                        [
                            new() { Id = 42, CreatedDate = DateTime.Today.AddDays(-3) },
                            new() { Id = 16, CreatedDate = DateTime.Today.AddDays(-1) },
                        ]);

            m.Setup(arg => arg.GetPullRequestIterationsAsync(
                    It.IsAny<Guid>(),
                    It.Is<int>(i => i == 13), // Just to emulate the unlucky case
                    null,
                    null,
                    CancellationToken.None))
                .ReturnsAsync((Guid _, int _, bool? _, object _, CancellationToken _)
                    =>
                        [
                            new() { Id = null },
                        ]);

            // Setup GitPullRequestIterationChanges collection
            var changes = new GitPullRequestIterationChanges
            {
                ChangeEntries =
                    [
                        new() { ChangeId = 100, ChangeTrackingId = 1, Item = new GitItem { Path = "/src/my/class1.cs" } },
                        new() { ChangeId = 200, ChangeTrackingId = 2, Item = new GitItem { Path = string.Empty } },

                    ],
            };

            m.Setup(arg => arg.GetPullRequestIterationChangesAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                null,
                null,
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync((Guid _, int _, int _, int? _, int? _, int? _, object _, CancellationToken _)
                    => changes);

            // Setup pull request creation
            m.Setup(arg => arg.GetRefsAsync(
                It.IsAny<string>(),
                "MyRepoName",
                "NotExistingBranch",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync(() => []);

            m.Setup(args => args.GetRepositoryAsync(It.IsAny<string>(), "MyRepoName", null, CancellationToken.None))
                .ReturnsAsync(() => new GitRepository() { DefaultBranch = "master" });

            m.Setup(arg => arg.GetRefsAsync(
                It.IsAny<string>(),
                "MyRepoName",
                "master",
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync(() =>
                [
                     new("master"),
                ]);

            m.Setup(
                arg =>
                    arg.CreatePullRequestAsync(
                        It.IsAny<GitPullRequest>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        null,
                        null,
                        CancellationToken.None))
             .ReturnsAsync(
                (
                    GitPullRequest gitPullRequestToCreate,
                    string _,
                    string repositoryId,
                    bool? _,
                    object _,
                    CancellationToken _) =>
                {
                    gitPullRequestToCreate.PullRequestId = 777;
                    gitPullRequestToCreate.Repository = new GitRepository
                    {
                        Id = Guid.NewGuid(),
                        Name = repositoryId,
                    };
                    gitPullRequestToCreate.CodeReviewId = 123;
                    gitPullRequestToCreate.LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" };
                    gitPullRequestToCreate.LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" };

                    return gitPullRequestToCreate;
                });

            m.Setup(arg => arg.UpdateCommentAsync(
                   It.IsAny<Comment>(),
                   It.IsAny<Guid>(),
                   It.IsAny<int>(),
                   It.IsAny<int>(),
                   It.IsAny<int>(),
                   null,
                   CancellationToken.None))
               .ReturnsAsync((Comment comment, Guid _, int _, int _, int _, object _, CancellationToken _)
                   => new Comment
                   {
                       Id = comment.Id,
                       Content = comment.Content,
                   });

            return m;
        }
    }
}