namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;

    internal class CreatePullRequestFixture(string repoUrl, string sourceRefName, string targetRefName, string title, string description)
                : BasePullRequestFixture
    {
        public AzureDevOpsCreatePullRequestSettings Settings { get; } =
                new AzureDevOpsCreatePullRequestSettings(
                    new Uri(repoUrl),
                    sourceRefName,
                    targetRefName,
                    title,
                    description,
                    new AzureDevOpsNtlmCredentials());
    }
}