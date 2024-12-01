namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Moq;

    public abstract class FakeWorkItemTrackingClientFactory : IWorkItemTrackingClientFactory
    {
        public abstract WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        public WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            authorizedIdentity = new Identity { ProviderDisplayName = "FakeUser", Id = Guid.NewGuid(), IsActive = true };
            return this.CreateWorkItemTrackingClient(collectionUrl, credentials);
        }

        protected Mock<WorkItemTrackingHttpClient> Setup(Mock<WorkItemTrackingHttpClient> m)
        {
            return m;
        }
    }
}