namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <inheritdoc />
    internal class TestManagementClientFactory : ITestManagementClientFactory
    {
        /// <inheritdoc/>
        public TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            return this.CreateTestManagementClient(collectionUrl, credentials, out _);
        }

        /// <inheritdoc />
        public TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials, out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(collectionUrl, credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            return connection.GetClient<TestManagementHttpClient>() ??
                throw new AzureDevOpsException("Could not retrieve the TestManagementHttpClient object");
        }
    }
}
