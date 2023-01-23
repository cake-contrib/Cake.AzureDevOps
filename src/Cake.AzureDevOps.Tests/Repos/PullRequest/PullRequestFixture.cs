namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;

    internal class PullRequestFixture
        : BasePullRequestFixture
    {
        public PullRequestFixture(string repoUrl, int prId)
        {
            this.Settings = new AzureDevOpsPullRequestSettings(new Uri(repoUrl), prId, new AzureDevOpsNtlmCredentials());
        }

        public PullRequestFixture(string repoUrl, string sourceRefName)
        {
            this.Settings = new AzureDevOpsPullRequestSettings(new Uri(repoUrl), sourceRefName, new AzureDevOpsNtlmCredentials());
        }

        public AzureDevOpsPullRequestSettings Settings { get; init; }
    }
}
