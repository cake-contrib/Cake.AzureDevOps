namespace Cake.Tfs.Tests.PullRequest.Fakes
{
    using System;
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

            return m;
        }
    }
}
