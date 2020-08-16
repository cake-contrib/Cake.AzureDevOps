namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using Cake.AzureDevOps.Repos;
    using Cake.AzureDevOps.Tests.Repos.PullRequest.Fakes;
    using Cake.Testing;

    internal abstract class BasePullRequestFixture
    {
        public const string ValidAzureDevOpsServerUrl = "http://MyServer/tfs/MyCollection/MyTeamProject/_git/MyRepoName";
        public const string ValidAzureDevOpsUrl = "https://my-account.visualstudio.com/DefaultCollection/MyProject/_git/MyRepoName";
        public const string InvalidUrl = "http://example.com";

        public BasePullRequestFixture()
        {
            this.InitializeFakes();
        }

        public FakeLog Log { get; set; }

        public IGitClientFactory GitClientFactory { get; set; }

        private void InitializeFakes()
        {
            this.Log = new FakeLog();
            this.GitClientFactory = new FakeAllSetGitClientFactory();
        }
    }
}
