namespace Cake.Tfs.PullRequest
{
    using System;
    using Cake.Tfs.Authentication;

    /// <summary>
    /// Base class for settings for aliases handling pull requests.
    /// </summary>
    public abstract class BaseTfsPullRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        protected BaseTfsPullRequestSettings(Uri repositoryUrl, ITfsCredentials credentials)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            credentials.NotNull(nameof(credentials));

            this.RepositoryUrl = repositoryUrl;
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceRefName">Branch for which the pull request is made.
        /// <see cref="ArgumentException"/> if <see langword="null"/> or <see cref="string.Empty"/>.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.
        /// <see cref="ArgumentException"/> if <see langword="null"/>.</param>
        protected BaseTfsPullRequestSettings(Uri repositoryUrl, string sourceRefName, ITfsCredentials credentials)
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            sourceRefName.NotNullOrWhiteSpace(nameof(sourceRefName));
            credentials.NotNull(nameof(credentials));

            this.RepositoryUrl = repositoryUrl;
            this.SourceRefName = sourceRefName;
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTfsPullRequestSettings"/> class
        /// based on the instance of a <see cref="BaseTfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.
        /// <see cref="ArgumentException"/> if <see langword="null"/>.</param>
        protected BaseTfsPullRequestSettings(BaseTfsPullRequestSettings settings)
        {
            settings.NotNull(nameof(settings));

            this.RepositoryUrl = settings.RepositoryUrl;
            this.SourceRefName = settings.SourceRefName;
            this.Credentials = settings.Credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseTfsPullRequestSettings"/> class using environment variables
        /// as set by a Azure Pipelines or Team Foundation Server build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        protected BaseTfsPullRequestSettings(ITfsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            this.Credentials = credentials;

            var repositoryUrl = Environment.GetEnvironmentVariable("BUILD_REPOSITORY_URI", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(repositoryUrl))
            {
                throw new InvalidOperationException(
                    "Failed to read the BUILD_REPOSITORY_URI environment variable. Make sure you are running in an Azure Pipelines or Team Foundation Server build.");
            }

            this.RepositoryUrl = new Uri(repositoryUrl);
        }

        /// <summary>
        /// Gets the full URL of the Git repository, eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// </summary>
        public Uri RepositoryUrl { get; private set; }

        /// <summary>
        /// Gets the branch for which the pull request is made.
        /// </summary>
        [Obsolete("Use SourceRefName instead.")]
        public string SourceBranch => this.SourceRefName;

        /// <summary>
        /// Gets the branch for which the pull request is made.
        /// </summary>
        public string SourceRefName { get; private set; }

        /// <summary>
        /// Gets the credentials used to authenticate against Team Foundation Server or
        /// Azure DevOps.
        /// </summary>
        public ITfsCredentials Credentials { get; private set; }
    }
}
