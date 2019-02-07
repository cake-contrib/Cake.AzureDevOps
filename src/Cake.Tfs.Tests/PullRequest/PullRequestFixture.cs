namespace Cake.Tfs.Tests.PullRequest
{
    using System;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;

    internal class PullRequestFixture
        : BasePullRequestFixture
    {
        public PullRequestFixture(string repoUrl, int prId)
            : base()
        {
            this.Settings = new TfsPullRequestSettings(new Uri(repoUrl), prId, new TfsNtlmCredentials());
        }

        public PullRequestFixture(string repoUrl, string sourceRefName)
            : base()
        {
            this.Settings = new TfsPullRequestSettings(new Uri(repoUrl), sourceRefName, new TfsNtlmCredentials());
        }

        public TfsPullRequestSettings Settings { get; set; }
    }
}
