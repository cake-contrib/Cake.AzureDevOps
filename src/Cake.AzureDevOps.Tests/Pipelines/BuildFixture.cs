namespace Cake.AzureDevOps.Tests.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Pipelines;
    using Cake.AzureDevOps.Tests.Fakes;
    using Cake.Core.Diagnostics;
    using Cake.Testing;

    internal class BuildFixture
    {
        public const string ValidAzureDevOpsCollectionUrl = "https://my-account.visualstudio.com/DefaultCollection";

        public BuildFixture(string collectionUrl, string projectName, int buildId)
        {
            this.Settings = new AzureDevOpsBuildSettings(new Uri(collectionUrl), projectName, buildId, new AzureDevOpsNtlmCredentials());
            this.InitializeFakes();
        }

        public ICakeLog Log { get; set; }

        public IBuildClientFactory BuildClientFactory { get; set; }

        public ITestManagementClientFactory TestManagementClientFactory { get; set; }

        public IWorkItemTrackingClientFactory WorkItemTrackingClientFactory { get; set; }

        public AzureDevOpsBuildSettings Settings { get; }

        private void InitializeFakes()
        {
            this.Log = new FakeLog();
            this.BuildClientFactory = new FakeAllSetBuildClientFactory();
            this.TestManagementClientFactory = new FakeAllSetTestManagementClientFactory();
            this.WorkItemTrackingClientFactory = new FakeAllSetWorkItemTrackingClientFactory();
        }
    }
}
