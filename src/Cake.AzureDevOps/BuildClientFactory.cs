namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class BuildClientFactory : IBuildClientFactory
    {
        /// <inheritdoc/>
        public BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateBuildClient(collectionUrl, credentials, out _);
        }

        /// <inheritdoc/>
        public BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var buildClient = connection.GetClient<BuildHttpClient>();
            if (buildClient == null)
            {
                throw new AzureDevOpsException("Could not retrieve the BuildHttpClient object");
            }

            return buildClient;
        }
    }
}
