namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class WorkItemTrackingClientFactory : IWorkItemTrackingClientFactory
    {
        /// <inheritdoc/>
        public WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateWorkItemTrackingClient(collectionUrl, credentials, out _);
        }

        /// <inheritdoc/>
        public WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var workItemTrackingClient = connection.GetClient<WorkItemTrackingHttpClient>();
            if (workItemTrackingClient == null)
            {
                throw new AzureDevOpsException("Could not retrieve the WorkItemTrackingHttpClient object");
            }

            return workItemTrackingClient;
        }
    }
}
