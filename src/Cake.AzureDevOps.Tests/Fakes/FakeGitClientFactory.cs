namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Moq;

    public abstract class FakeGitClientFactory : IGitClientFactory
    {
        public abstract GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        public GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity identity)
        {
            identity = new Identity { ProviderDisplayName = "FakeUser", Id = Guid.NewGuid(), IsActive = true };
            return this.CreateGitClient(collectionUrl, credentials);
        }

        protected virtual Mock<GitHttpClient> Setup(Mock<GitHttpClient> m)
        {
            return m;
        }
    }
}
