namespace Cake.AzureDevOps
{
    using System;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Microsoft.VisualStudio.Services.Common;

    /// <inheritdoc />
    internal class TestManagementClientFactory : ITestManagementClientFactory
    {
        /// <inheritdoc />
        public TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, VssCredentials credentials)
        {
            TestManagementHttpClient testClient = new TestManagementHttpClient(collectionUrl, credentials);
            return testClient;
        }
    }
}
