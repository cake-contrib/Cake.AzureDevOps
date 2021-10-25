namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Moq;

    public class FakeAllSetBuildClientFactory : FakeBuildClientFactory
    {
        public override BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<BuildHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<Guid>(), It.IsAny<int>(), null, null, default(CancellationToken)))
                .ReturnsAsync((Guid projectId, int buildId, string propertyFilters, object userState, CancellationToken token) => new Build
                {
                    Id = buildId,
                    BuildNumber = buildId.ToString(),
                    Project = new TeamProjectReference { Id = projectId },
                });

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<string>(), It.IsAny<int>(), null, null, default(CancellationToken)))
                .ReturnsAsync((string projectName, int buildId, string propertyFilters, object userState, CancellationToken token) => new Build
                {
                    Id = buildId,
                    BuildNumber = buildId.ToString(),
                    Project = new TeamProjectReference { Name = projectName },
                });

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}
