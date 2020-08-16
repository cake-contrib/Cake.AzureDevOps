namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Microsoft.VisualStudio.Services.Identity;

    /// <summary>
    /// The interface for a Git client factory.
    /// </summary>
    internal interface ITestManagementClientFactory
    {
        /// <summary>
        /// Creates the instance of the <see cref="TestManagementHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <returns>The instance of <see cref="TestManagementHttpClient"/> class.</returns>
        TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials);

        /// <summary>
        /// Creates the instance of the <see cref="TestManagementHttpClient"/> class.
        /// </summary>
        /// <param name="collectionUrl">The URL of the Azure DevOps team project collection.</param>
        /// <param name="credentials">The credentials to connect to Azure DevOps.</param>
        /// <param name="authorizedIdentity">Returns identity which is authorized.</param>
        /// <returns>The instance of <see cref="TestManagementHttpClient"/> class.</returns>
        TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity);
    }
}
