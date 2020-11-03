namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Moq;

    public class FakeNullGitClientFactory : FakeGitClientFactory
    {
        public override GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<GitHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetPullRequestAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), null, null, null, null, null, null, default(CancellationToken)))
                .ReturnsAsync(() => null);

            mock.Setup(arg => arg.GetPullRequestsAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<GitPullRequestSearchCriteria>(), null, null, 1, null, default(CancellationToken)))
                .ReturnsAsync(() => new List<GitPullRequest>());

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}
