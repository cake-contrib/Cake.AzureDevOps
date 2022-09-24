namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Core.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class ProjectClientFactory : IProjectClientFactory
    {
        /// <inheritdoc/>
        public ProjectHttpClient CreateProjectClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateProjectClient(collectionUrl, credentials, out _);
        }

        /// <inheritdoc/>
        public ProjectHttpClient CreateProjectClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var projectClient = connection.GetClient<ProjectHttpClient>();
            if (projectClient == null)
            {
                throw new AzureDevOpsException("Could not retrieve the ProjectHttpClient object");
            }

            return projectClient;
        }
    }
}
