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
        /// <summary>
        /// Creates a client object for communicating with Azure DevOps.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <param name="authorizedIdentity">Returns identity which is authorized.</param>
        /// <returns>Client object for communicating with Azure DevOps.</returns>
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

        /// <summary>
        /// Creates a client object for communicating with Azure DevOps.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <returns>Client object for communicating with Azure DevOps.</returns>
        public BuildHttpClient CreateBuildClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateBuildClient(collectionUrl, credentials, out _);
        }
    }
}
