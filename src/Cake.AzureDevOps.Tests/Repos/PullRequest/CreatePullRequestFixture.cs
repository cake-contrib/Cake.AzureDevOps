namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;

    internal class CreatePullRequestFixture
        : BasePullRequestFixture
    {
        public CreatePullRequestFixture(string repoUrl, string sourceRefName, string targetRefName, string title, string description)
        {
            this.Settings =
                new AzureDevOpsCreatePullRequestSettings(
                    new Uri(repoUrl),
                    sourceRefName,
                    targetRefName,
                    title,
                    description,
                    new AzureDevOpsNtlmCredentials());
        }

        public AzureDevOpsCreatePullRequestSettings Settings { get; }
    }
}
