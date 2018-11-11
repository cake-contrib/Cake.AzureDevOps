namespace Cake.Tfs.Tests.PullRequest.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Moq;

    public class FakeNullForMethodsGitClientFactory : FakeAllSetGitClientFactory
    {
        protected override Mock<GitHttpClient> Setup(Mock<GitHttpClient> m)
        {
            m.Setup(arg => arg.CreatePullRequestReviewerAsync(It.IsAny<IdentityRefWithVote>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<object>(), default(CancellationToken)))
             .ReturnsAsync(() => null);

            m.Setup(arg => arg.CreatePullRequestStatusAsync(It.IsAny<GitPullRequestStatus>(), It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<object>(), It.IsAny<CancellationToken>()))
             .ReturnsAsync(() => null);

            m.Setup(arg => arg.GetCommitDiffsAsync(It.IsAny<string>(), It.IsAny<Guid>(), true, null, null, It.IsAny<GitBaseVersionDescriptor>(), It.IsAny<GitTargetVersionDescriptor>(), null, CancellationToken.None))
             .ReturnsAsync(()
                    => new GitCommitDiffs { ChangeCounts = new Dictionary<VersionControlChangeType, int>(), Changes = new List<GitChange>() });

            return m;
        }
    }
}
