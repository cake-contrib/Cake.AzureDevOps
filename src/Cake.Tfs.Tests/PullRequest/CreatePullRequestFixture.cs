namespace Cake.Tfs.Tests.PullRequest
{
    using System;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;

    internal class CreatePullRequestFixture
        : BasePullRequestFixture
    {
        public CreatePullRequestFixture(string repoUrl, string sourceRefName, string targetRefName, string title, string description)
            : base()
        {
            this.Settings =
                new TfsCreatePullRequestSettings(
                    new Uri(repoUrl),
                    sourceRefName,
                    targetRefName,
                    title,
                    description,
                    new TfsNtlmCredentials());
        }

        public TfsCreatePullRequestSettings Settings { get; set; }
    }
}
