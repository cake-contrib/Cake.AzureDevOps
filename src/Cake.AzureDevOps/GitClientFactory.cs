namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class GitClientFactory : IGitClientFactory
    {
        /// <inheritdoc/>
        public GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateGitClient(collectionUrl, credentials, out _);
        }

        /// <inheritdoc/>
        public GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var gitClient = connection.GetClient<GitHttpClient>();
            if (gitClient == null)
            {
                throw new AzureDevOpsException("Could not retrieve the GitHttpClient object");
            }

            return gitClient;
        }
    }
}
