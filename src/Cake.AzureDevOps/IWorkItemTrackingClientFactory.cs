namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
    using Microsoft.VisualStudio.Services.Identity;

    /// <summary>
    /// The interface for a work item tracking client factory.
    /// </summary>
    internal interface IWorkItemTrackingClientFactory
    {
        /// <summary>
        /// Creates the instance of the <see cref="WorkItemTrackingHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <returns>The instance of <see cref="WorkItemTrackingHttpClient"/> class.</returns>
        WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        /// <summary>
        /// Creates the instance of the <see cref="WorkItemTrackingHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <param name="authorizedIdentity">Returns identity which is authorized.</param>
        /// <returns>The instance of <see cref="WorkItemTrackingHttpClient"/> class.</returns>
        WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity);
    }
}
