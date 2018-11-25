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
             .ReturnsAsync((string prj, Guid rId, bool? b, int? t, int? s, GitBaseVersionDescriptor bvd, GitTargetVersionDescriptor tvd, object o1, CancellationToken c1)
                    => gitCommitDiffs);

            return m;
        }
    }
}
