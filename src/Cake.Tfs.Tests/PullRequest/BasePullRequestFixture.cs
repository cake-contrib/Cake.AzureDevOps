namespace Cake.Tfs.Tests.PullRequest
{
    using Cake.Testing;
    using Cake.Tfs.Tests.PullRequest.Fakes;

    internal abstract class BasePullRequestFixture
    {
        public const string ValidTfsUrl = "http://MyServer/tfs/MyCollection/MyTeamProject/_git/MyRepoName";
        public const string ValidAzureDevOpsUrl = "https://my-account.visualstudio.com/DefaultCollection/MyProject/_git/MyRepoName";
        public const string InvalidTfsUrl = "http://example.com";

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
