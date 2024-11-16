namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases creating pull requests.
    /// </summary>
    public class AzureDevOpsCreatePullRequestSettings : BaseAzureDevOpsPullRequestSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsCreatePullRequestSettings"/> class.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceRefName">Branch for which the pull request is made.</param>
        /// <param name="targetRefName">Target branch of the pull request.
        /// If <see langword="null"/> or <see cref="string.Empty"/> the default branch of the repository will be used.</param>
        /// <param name="title">Title of the pull request.</param>
        /// <param name="description">Description of the pull request.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsCreatePullRequestSettings(
            Uri repositoryUrl,
            string sourceRefName,
            string targetRefName,
            string title,
            string description,
            IAzureDevOpsCredentials credentials)
            : base(repositoryUrl, sourceRefName, credentials)
        {
            title.NotNullOrWhiteSpace(nameof(title));
            description.NotNull(nameof(description));

            this.TargetRefName = targetRefName;
            this.Title = title;
            this.Description = description;
        }

        /// <summary>
        /// Gets the target branch of the pull request.
        /// If <see langword="null"/> or <see cref="string.Empty"/> the default branch of the repository will be used.
        /// </summary>
        public string TargetRefName { get; }

        /// <summary>
        /// Gets the title of the pull request.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets the description of the pull request.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <param name="repositoryUrl">Full URL of the Git repository,
        /// eg. <code>http://myserver:8080/defaultcollection/myproject/_git/myrepository</code>.
        /// Supported URL schemes are HTTP, HTTPS and SSH.
        /// URLs using SSH scheme are converted to HTTPS.</param>
        /// <param name="sourceRefName">Branch for which the pull request is made.</param>
        /// <param name="targetRefName">Target branch of the pull request.
        /// If <see langword="null"/> or <see cref="string.Empty"/> the default branch of the repository will be used.</param>
        /// <param name="title">Title of the pull request.</param>
        /// <param name="description">Description of the pull request.</param>
        /// <returns>The instance of <see cref="AzureDevOpsCreatePullRequestSettings"/> class.</returns>
        public static AzureDevOpsCreatePullRequestSettings UsingAzurePipelinesOAuthToken(
            Uri repositoryUrl,
            string sourceRefName,
            string targetRefName,
            string title,
            string description)
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);

            return
                !string.IsNullOrWhiteSpace(accessToken)
                ? new AzureDevOpsCreatePullRequestSettings(
                    repositoryUrl,
                    sourceRefName,
                    targetRefName,
                    title,
                    description,
                    new AzureDevOpsOAuthCredentials(accessToken))
                : throw new InvalidOperationException(
                    "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines build and that the 'Allow Scripts to access OAuth token' option is enabled.");
        }
    }
}