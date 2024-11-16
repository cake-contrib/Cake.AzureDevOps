namespace Cake.AzureDevOps.Repos.PullRequest
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
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        /// <exception cref="InvalidOperationException">If build is not running in Azure Pipelines,
        /// or build is not for a pull request.</exception>
        public AzureDevOpsPullRequestSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            var pullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(pullRequestId))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_PULLREQUEST_PULLREQUESTID environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            if (!int.TryParse(pullRequestId, out var pullRequestIdValue))
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
        public int? PullRequestId { get; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if
        /// pull request for <see cref="BaseAzureDevOpsPullRequestSettings.SourceRefName"/> or <see cref="PullRequestId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfPullRequestCouldNotBeFound { get; set; } = true;

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsPullRequestSettings"/> class.</returns>
        /// <exception cref="InvalidOperationException">If build is not running in Azure Pipelines,
        /// build is not for a pull request or 'Allow Scripts to access OAuth token' option is not enabled
        /// on the build definition.</exception>
        public static AzureDevOpsPullRequestSettings UsingAzurePipelinesOAuthToken()
        {
            return UsingAzurePipelinesOAuthToken(true);
        }

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <param name="throwExceptionIfVariablesDontExist">Value indicating whether an exception
        /// should be thrown if required environment variables could not be found.</param>
        /// <returns>The instance of <see cref="AzureDevOpsPullRequestSettings"/> class.
        /// Returns <c>null</c> if variables don't exist and
        /// <paramref name="throwExceptionIfVariablesDontExist"/> is set to <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If <paramref name="throwExceptionIfVariablesDontExist"/>
        /// is set to <c>true</c> and build is not running in Azure Pipelines, build is not for a pull request
        /// or 'Allow Scripts to access OAuth token' option is not enabled on the build definition.</exception>
        public static AzureDevOpsPullRequestSettings UsingAzurePipelinesOAuthToken(bool throwExceptionIfVariablesDontExist)
        {
            var (valid, accessToken) = RetrieveAzurePipelinesVariables(throwExceptionIfVariablesDontExist);

            if (!valid)
            {
                return null;
            }

            try
            {
                return new AzureDevOpsPullRequestSettings(new AzureDevOpsOAuthCredentials(accessToken));
            }
            catch (InvalidOperationException)
            {
                if (!throwExceptionIfVariablesDontExist)
                {
                    return null;
                }

                throw;
            }
        }

        /// <summary>
        /// Validates and retrieves variables set by Azure Pipelines.
        /// </summary>
        /// <param name="throwExceptionIfVariablesDontExist">Value indicating whether an exception
        /// should be thrown if required environment variables could not be found.</param>
        /// <returns>Tuple containing a flag if variables are valid and the variable values.</returns>
        private static (bool Valid, string AccessToken) RetrieveAzurePipelinesVariables(bool throwExceptionIfVariablesDontExist)
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                return throwExceptionIfVariablesDontExist
                    ? throw new InvalidOperationException(
                        "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines build and that the 'Allow Scripts to access OAuth token' option is enabled.")
                    : ((bool Valid, string AccessToken))(false, null);
            }

            return (true, accessToken);
        }
    }
}
