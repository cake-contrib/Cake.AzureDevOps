namespace Cake.Tfs.Tests.PullRequest
{
    using System;
    using Cake.Testing;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;
    using Cake.Tfs.Tests.PullRequest.Fakes;

    internal class PullRequestFixture
    {
        public const string ValidTfsUrl = "http://MyServer/tfs/MyCollection/MyTeamProject/_git/MyRepoName";
        public const string ValidAzureDevOpsUrl = "https://my-account.visualstudio.com/DefaultCollection/MyProject/_git/MyRepoName";
        public const string InvalidTfsUrl = "http://example.com";

        public PullRequestFixture(string repoUrl, int prId)
        {
            this.Settings = new TfsPullRequestSettings(new Uri(repoUrl), prId, new TfsNtlmCredentials());

            this.InitializeFakes();
        }

        public PullRequestFixture(string repoUrl, string sourceBranch)
        {
            this.Settings = new TfsPullRequestSettings(new Uri(repoUrl), sourceBranch, new TfsNtlmCredentials());

            this.InitializeFakes();
        }

        public FakeLog Log { get; set; }

        public TfsPullRequestSettings Settings { get; set; }

        public IGitClientFactory GitClientFactory { get; set; }

        private void InitializeFakes()
        {
            this.Log = new FakeLog();
            this.GitClientFactory = new FakeAllSetGitClientFactory();
        }
    }
}
