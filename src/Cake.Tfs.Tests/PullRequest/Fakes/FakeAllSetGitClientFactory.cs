namespace Cake.Tfs.Tests.PullRequest.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Moq;

    public class FakeAllSetGitClientFactory : FakeGitClientFactory
    {
        public override GitHttpClient CreateGitClient(Uri collectionUrl, ITfsCredentials credentials)
        {
            var mock = new Mock<GitHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetPullRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), null, null, null, null, null, null, default(CancellationToken)))
                .ReturnsAsync((string project1, string repoId1, int prId, int i1, int i2, int i3, bool b1, bool b2, object o1, CancellationToken c1) => new GitPullRequest
                {
                    PullRequestId = prId,
                    Repository = new GitRepository
                    {
                        Id = Guid.NewGuid(),
                        Name = repoId1
                    },
                    SourceRefName = "foo",
                    TargetRefName = "master",
                    CodeReviewId = 123,
                    LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" },
                    LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" }
                });

            mock.Setup(arg => arg.GetPullRequestsAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<GitPullRequestSearchCriteria>(),
                    null,
                    null,
                    1,
                    null,
                    default(CancellationToken)))
                .ReturnsAsync((string project2, string repoId2, GitPullRequestSearchCriteria sc, int j1, int j2, int top, object o2, CancellationToken c2)
                    => new List<GitPullRequest>(new[]
                    {
                        new GitPullRequest
                        {
                            PullRequestId = 777,
                            Repository = new GitRepository
                            {
                                Id = Guid.NewGuid(),
                                Name = repoId2
                            },
                            SourceRefName = sc.SourceRefName,
                            TargetRefName = "master",
                            CodeReviewId = 123,
                            LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" },
                            LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" }
                        }
                    }));

            mock = this.Setup(mock);

            return mock.Object;
        }

        protected override Mock<GitHttpClient> Setup(Mock<GitHttpClient> m)
        {
            m.Setup(arg => arg.CreatePullRequestReviewerAsync(
                    It.Is<IdentityRefWithVote>(i => Enum.IsDefined(typeof(TfsPullRequestVote), i.Vote)),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    default(CancellationToken)))
             .ReturnsAsync((IdentityRefWithVote identity, Guid project, int prId, string reviewerId, object o, CancellationToken c)
                    => new IdentityRefWithVote
                    {
                        Vote = identity.Vote,
                    });

            m.Setup(arg => arg.CreatePullRequestReviewerAsync(
                    It.Is<IdentityRefWithVote>(i => !Enum.IsDefined(typeof(TfsPullRequestVote), i.Vote)),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    default(CancellationToken)))
             .Throws(new Exception("Something went wrong"));

            m.Setup(arg => arg.CreatePullRequestStatusAsync(
                    It.IsAny<GitPullRequestStatus>(),
                    It.IsAny<Guid>(),
                    It.IsAny<int>(),
                    It.IsAny<object>(),
                    It.IsAny<CancellationToken>()))
             .ReturnsAsync((GitPullRequestStatus status, Guid repoId, int prId, object o, CancellationToken c)
                    => new GitPullRequestStatus
                    {
                        Context = status.Context,
                        State = status.State
                    });

            // Setup CommitDiffs object
            var gitChanges = new List<GitChange>
            {
                new GitChange
                {
                    ChangeId = 1,
                    ChangeType = VersionControlChangeType.Edit,
                    Item = new GitItem("/src/project/myclass.cs", "ID1", GitObjectType.Commit, "6b13ff8", 0)
                },
                null,
                new GitChange
                {
                    ChangeId = 2,
                    ChangeType = VersionControlChangeType.Edit,
                    Item = new GitItem("/tools/folder", "ID2", GitObjectType.Tree, "6b13ff8", 0)
                }
            };

            var gitCommitDiffs = new GitCommitDiffs
            {
                ChangeCounts = new Dictionary<VersionControlChangeType, int> { { VersionControlChangeType.Edit, 2 } },
                Changes = gitChanges
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
             .ReturnsAsync((string prj, Guid rId, bool? b, int? t, int? s, GitBaseVersionDescriptor bvd, GitTargetVersionDescriptor tvd, object o, CancellationToken c)
                    => gitCommitDiffs);

            m.Setup(arg => arg.UpdateThreadAsync(
                It.IsAny<GitPullRequestCommentThread>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                null,
                CancellationToken.None))
             .ReturnsAsync((GitPullRequestCommentThread prct, Guid g, int prId, int thId, object o, CancellationToken c)
                    => new GitPullRequestCommentThread { Id = thId, Status = prct.Status });

            // Setup GitPullRequestCommentThread collection
            var commentThreads = new List<GitPullRequestCommentThread>
            {
                new GitPullRequestCommentThread
                {
                    Id = 11,
                    ThreadContext = new CommentThreadContext()
                    {
                        FilePath = "/some/path/to/file.cs"
                    },
                    Comments = new List<Comment>
                    {
                        new Comment { Content = "Hello", IsDeleted = false, CommentType = CommentType.CodeChange },
                        new Comment { Content = "Goodbye", IsDeleted = true, CommentType = CommentType.Text }
                    },
                    Status = CommentThreadStatus.Active
                },
                new GitPullRequestCommentThread
                {
                    Id = 22,
                    ThreadContext = null,
                    Comments = new List<Comment>(),
                    Status = CommentThreadStatus.Fixed
                }
            };

            m.Setup(arg => arg.GetThreadsAsync(
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                null,
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync((Guid rId, int prId, int? it, int? baseIt, object o, CancellationToken c)
                    => commentThreads);

            m.Setup(arg => arg.CreateThreadAsync(
                It.IsAny<GitPullRequestCommentThread>(),
                It.IsAny<Guid>(),
                It.IsAny<int>(),
                null,
                CancellationToken.None))
             .ReturnsAsync((GitPullRequestCommentThread prct, Guid g, int i, object o, CancellationToken c)
                    => prct);

            m.Setup(arg => arg.GetPullRequestIterationsAsync(
                It.IsAny<Guid>(),
                It.Is<int>(i => i != 13),
                null,
                null,
                CancellationToken.None))
             .ReturnsAsync((Guid repoId, int prId, bool? b, object o, CancellationToken c)
                    => new List<GitPullRequestIteration>
                    {
                        new GitPullRequestIteration { Id = 42, CreatedDate = DateTime.Today.AddDays(-3) },
                        new GitPullRequestIteration { Id = 16, CreatedDate = DateTime.Today.AddDays(-1) }
                    });

            m.Setup(arg => arg.GetPullRequestIterationsAsync(
                    It.IsAny<Guid>(),
                    It.Is<int>(i => i == 13), // Just to emulate the unlucky case
                    null,
                    null,
                    CancellationToken.None))
                .ReturnsAsync((Guid repoId, int prId, bool? b, object o, CancellationToken c)
                    => new List<GitPullRequestIteration>
                    {
                        new GitPullRequestIteration { Id = null }
                    });

            // Setup GitPullRequestIterationChanges collection
            var changes = new GitPullRequestIterationChanges
            {
                ChangeEntries = new List<GitPullRequestChange>
                {
                    new GitPullRequestChange { ChangeId = 100, ChangeTrackingId = 1, Item = new GitItem { Path = "/src/my/class1.cs" } },
                    new GitPullRequestChange { ChangeId = 200, ChangeTrackingId = 2, Item = new GitItem { Path = string.Empty } }
                }
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
             .ReturnsAsync((Guid repoId, int prId, int iterId, int? t, int? s, int? ct, object o, CancellationToken c)
                    => changes);

            // Setup pull request creation
            m.Setup(arg => arg.GetRefsAsync(It.IsAny<string>(), "MyRepoName", "NotExistingBranch", null, null, null, CancellationToken.None))
             .ReturnsAsync(() => new List<GitRef>());

            m.Setup(args => args.GetRepositoryAsync(It.IsAny<string>(), "MyRepoName", null, null, CancellationToken.None))
                .ReturnsAsync(() => new GitRepository() { DefaultBranch = "master" });

            m.Setup(arg => arg.GetRefsAsync(It.IsAny<string>(), "MyRepoName", "master", null, null, null, CancellationToken.None))
             .ReturnsAsync(() => new List<GitRef>()
             {
                 new GitRef("master")
             });

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
                    string project,
                    string repositoryId,
                    bool? supportsIterations,
                    object userState,
                    CancellationToken cancellationToken) =>
                {
                    gitPullRequestToCreate.PullRequestId = 777;
                    gitPullRequestToCreate.Repository = new GitRepository
                    {
                        Id = Guid.NewGuid(),
                        Name = repositoryId
                    };
                    gitPullRequestToCreate.CodeReviewId = 123;
                    gitPullRequestToCreate.LastMergeSourceCommit = new GitCommitRef { CommitId = "4a92b977" };
                    gitPullRequestToCreate.LastMergeTargetCommit = new GitCommitRef { CommitId = "78a3c113" };

                     return gitPullRequestToCreate;
                 });

            return m;
        }
    }
}
