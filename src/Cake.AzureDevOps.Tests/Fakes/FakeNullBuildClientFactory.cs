namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Moq;

    public class FakeNullBuildClientFactory : FakeBuildClientFactory
    {
        public override BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<BuildHttpClient>(MockBehavior.Loose, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<Guid>(), It.IsAny<int>(), null, null, default))
                .ReturnsAsync(() => null);

            mock.Setup(arg => arg.GetBuildAsync(It.IsAny<string>(), It.IsAny<int>(), null, null, default))
                .ReturnsAsync(() => null);

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}
