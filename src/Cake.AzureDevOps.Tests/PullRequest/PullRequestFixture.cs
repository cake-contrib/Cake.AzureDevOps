namespace Cake.AzureDevOps.Tests.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.PullRequest;

    internal class PullRequestFixture
        : BasePullRequestFixture
    {
        public PullRequestFixture(string repoUrl, int prId)
            : base()
        {
            this.Settings = new AzureDevOpsPullRequestSettings(new Uri(repoUrl), prId, new AzureDevOpsNtlmCredentials());
        }

        public PullRequestFixture(string repoUrl, string sourceRefName)
            : base()
        {
            this.Settings = new AzureDevOpsPullRequestSettings(new Uri(repoUrl), sourceRefName, new AzureDevOpsNtlmCredentials());
        }

        public AzureDevOpsPullRequestSettings Settings { get; set; }
    }
}
