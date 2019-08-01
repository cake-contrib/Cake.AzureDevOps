namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;

    /// <summary>
    /// The interface for a Git client factory.
    /// </summary>
    internal interface IGitClientFactory
    {
        /// <summary>
        /// Creates the instance of the <see cref="GitHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <returns>The instance of <see cref="GitHttpClient"/> class.</returns>
        GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        /// <summary>
        /// Creates the instance of the <see cref="GitHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <param name="identity">Returns identity which is authorized.</param>
        /// <returns>The instance of <see cref="GitHttpClient"/> class.</returns>
        GitHttpClient CreateGitClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity identity);
    }
}
