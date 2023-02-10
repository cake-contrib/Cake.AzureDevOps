namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Moq;

    public abstract class FakeTestManagementClientFactory : ITestManagementClientFactory
    {
        public abstract TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        public TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            authorizedIdentity = new Identity { ProviderDisplayName = "FakeUser", Id = Guid.NewGuid(), IsActive = true };
            return this.CreateTestManagementClient(collectionUrl, credentials);
        }

        protected Mock<TestManagementHttpClient> Setup(Mock<TestManagementHttpClient> m)
        {
            return m;
        }
    }
}
