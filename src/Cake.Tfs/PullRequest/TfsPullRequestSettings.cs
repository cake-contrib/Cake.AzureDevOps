namespace Cake.Tfs.PullRequest
{
    using System;
    using Cake.Tfs.Authentication;

    /// <summary>
    /// Settings for aliases handling pull requests.
    /// </summary>
    public class TfsPullRequestSettings : BaseTfsPullRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        public TfsPullRequestSettings(Uri repositoryUrl, string sourceBranch, ITfsCredentials credentials)
            : base(repositoryUrl, sourceBranch, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        public TfsPullRequestSettings(Uri repositoryUrl, int pullRequestId, ITfsCredentials credentials)
            : base(repositoryUrl, credentials)
        {
            pullRequestId.NotNegativeOrZero(nameof(pullRequestId));

            this.PullRequestId = pullRequestId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class
        /// based on the instance of a <see cref="TfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public TfsPullRequestSettings(TfsPullRequestSettings settings)
            : base(settings)
        {
            this.PullRequestId = settings.PullRequestId;
            this.ThrowExceptionIfPullRequestCouldNotBeFound = settings.ThrowExceptionIfPullRequestCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class using environment variables
        /// as set by a Azure Pipelines or Team Foundation Server build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        public TfsPullRequestSettings(ITfsCredentials credentials)
            : base(credentials)
        {
            var pullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(pullRequestId))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_PULLREQUEST_PULLREQUESTID environment variable. Make sure you are running in an Azure Pipelines or Team Foundation Server build.");
            }

            if (!int.TryParse(pullRequestId, out int pullRequestIdValue))
            {
                throw new InvalidOperationException("SYSTEM_PULLREQUEST_PULLREQUESTID environment variable should contain integer value");
            }

            if (pullRequestIdValue <= 0)
            {
                throw new InvalidOperationException("SYSTEM_PULLREQUEST_PULLREQUESTID environment variable should contain integer value and it should be greater than zero");
            }

            this.PullRequestId = pullRequestIdValue;
        }

        /// <summary>
        /// Gets the ID of the pull request.
        /// </summary>
        public int? PullRequestId { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if
        /// pull request for <see cref="BaseTfsPullRequestSettings.SourceRefName"/> or <see cref="PullRequestId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfPullRequestCouldNotBeFound { get; set; } = true;

        /// <summary>
        /// Constructs the settings object using the access token provided by a Azure Pipelines or Team Foundation Server build.
        /// </summary>
        /// <returns>The instance of <see cref="TfsPullRequestSettings"/> class.</returns>
        public static TfsPullRequestSettings UsingTfsBuildOAuthToken()
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines or Team Foundation Server build and that the 'Allow Scripts to access OAuth token' option is enabled.");
            }

            return new TfsPullRequestSettings(new TfsOAuthCredentials(accessToken));
        }
    }
}
