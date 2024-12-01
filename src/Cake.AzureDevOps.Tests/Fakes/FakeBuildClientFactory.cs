namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Moq;

    public abstract class FakeBuildClientFactory : IBuildClientFactory
    {
        public abstract BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        public BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            authorizedIdentity = new Identity { ProviderDisplayName = "FakeUser", Id = Guid.NewGuid(), IsActive = true };
            return this.CreateBuildClient(collectionUrl, credentials);
        }

        protected Mock<BuildHttpClient> Setup(Mock<BuildHttpClient> m)
        {
            return m;
        }
    }
}