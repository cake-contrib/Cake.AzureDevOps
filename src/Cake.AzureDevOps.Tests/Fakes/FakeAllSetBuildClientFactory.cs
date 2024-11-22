namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;
    using Moq;

    public class FakeAllSetBuildClientFactory : FakeBuildClientFactory
    {
        public override BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<BuildHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<Guid>(), It.IsAny<int>(), null, null, default))
                .ReturnsAsync((Guid projectId, int buildId, string _, object _, CancellationToken _) => new Build
                {
                    Id = buildId,
                    BuildNumber = buildId.ToString(),
                    Project = new TeamProjectReference { Id = projectId },
                });

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<string>(), It.IsAny<int>(), null, null, default))
                .ReturnsAsync((string projectName, int buildId, string _, object _, CancellationToken _) => new Build
                {
                    Id = buildId,
                    BuildNumber = buildId.ToString(),
                    Project = new TeamProjectReference { Name = projectName },
                });

            mock.Setup(arg => arg.GetBuildWorkItemsRefsAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int?>(), null, default))
               .ReturnsAsync((string _, int _, int? _, object _, CancellationToken _) =>
               [
                  new() { Id = "42" },
               ]);

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}