namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Base class for settings for aliases handling pull requests.
    /// </summary>
    public abstract class BaseAzureDevOpsPullRequestSettings : BaseAzureDevOpsCredentialsSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsPullRequestSettings(Uri repositoryUrl, IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));

            this.RepositoryUrl = repositoryUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceRefName">Branch for which the pull request is made.
        /// <see cref="ArgumentException"/> if <see langword="null"/> or <see cref="string.Empty"/>.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.
        /// <see cref="ArgumentException"/> if <see langword="null"/>.</param>
        protected BaseAzureDevOpsPullRequestSettings(Uri repositoryUrl, string sourceRefName, IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            sourceRefName.NotNullOrWhiteSpace(nameof(sourceRefName));

            this.RepositoryUrl = repositoryUrl;
            this.SourceRefName = sourceRefName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsPullRequestSettings"/> class
        /// based on the instance of a <see cref="BaseAzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.
        /// <see cref="ArgumentException"/> if <see langword="null"/>.</param>
        protected BaseAzureDevOpsPullRequestSettings(BaseAzureDevOpsPullRequestSettings settings)
            : base(settings)
        {
            this.RepositoryUrl = settings.RepositoryUrl;
            this.SourceRefName = settings.SourceRefName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsPullRequestSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsPullRequestSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            var repositoryUrl = Environment.GetEnvironmentVariable("BUILD_REPOSITORY_URI", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(repositoryUrl))
            {
                throw new InvalidOperationException(
                    "Failed to read the BUILD_REPOSITORY_URI environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            this.RepositoryUrl = new Uri(repositoryUrl);
        }

        /// <summary>
        /// Gets the full URL of the Git repository, eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// </summary>
        public Uri RepositoryUrl { get; }

        /// <summary>
        /// Gets the branch for which the pull request is made.
        /// </summary>
        public string SourceRefName { get; }
    }
}