namespace Cake.Tfs
{
    using System;
    using Cake.Tfs.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class GitClientFactory : IGitClientFactory
    {
        /// <summary>
        /// Creates a client object for communicating with Team Foundation Server or Azure DevOps.
        /// </summary>
        /// <param name="collectionUrl">The URL of the TFS/Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to TFS/Azure DevOps.</param>
        /// <param name="authorizedIdentity">Returns identity which is authorized.</param>
        /// <returns>Client object for communicating with Team Foundation Server or Azure DevOps.</returns>
        public GitHttpClient CreateGitClient(Uri collectionUrl, ITfsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var gitClient = connection.GetClient<GitHttpClient>();
            if (gitClient == null)
            {
                throw new TfsException("Could not retrieve the GitHttpClient object");
            }

            return gitClient;
        }

        /// <summary>
        /// Creates a client object for communicating with Team Foundation Server or Azure DevOps.
        /// </summary>
        /// <param name="collectionUrl">The URL of the TFS/Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to TFS/Azure DevOps.</param>
        /// <returns>Client object for communicating with Team Foundation Server or Azure DevOps.</returns>
        public GitHttpClient CreateGitClient(Uri collectionUrl, ITfsCredentials credentials)
        {
            return this.CreateGitClient(collectionUrl, credentials, out _);
        }
    }
}
