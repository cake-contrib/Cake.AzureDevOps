namespace Cake.AzureDevOps.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling pull requests.
    /// </summary>
    public class AzureDevOpsPullRequestSettings : BaseAzureDevOpsPullRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceBranch">Branch for which the pull request is made.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsPullRequestSettings(Uri repositoryUrl, string sourceBranch, IAzureDevOpsCredentials credentials)
            : base(repositoryUrl, sourceBranch, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="pullRequestId">ID of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsPullRequestSettings(Uri repositoryUrl, int pullRequestId, IAzureDevOpsCredentials credentials)
            : base(repositoryUrl, credentials)
        {
            pullRequestId.NotNegativeOrZero(nameof(pullRequestId));

            this.PullRequestId = pullRequestId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsPullRequestSettings(AzureDevOpsPullRequestSettings settings)
            : base(settings)
        {
            this.PullRequestId = settings.PullRequestId;
            this.ThrowExceptionIfPullRequestCouldNotBeFound = settings.ThrowExceptionIfPullRequestCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestSettings"/> class using environment variables
        /// as set by a Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsPullRequestSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            var pullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(pullRequestId))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_PULLREQUEST_PULLREQUESTID environment variable. Make sure you are running in an Azure Pipelines build.");
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
        /// pull request for <see cref="BaseAzureDevOpsPullRequestSettings.SourceRefName"/> or <see cref="PullRequestId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfPullRequestCouldNotBeFound { get; set; } = true;

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsPullRequestSettings"/> class.</returns>
        public static AzureDevOpsPullRequestSettings UsingAzurePipelinesOAuthToken()
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines build and that the 'Allow Scripts to access OAuth token' option is enabled.");
            }

            return new AzureDevOpsPullRequestSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
