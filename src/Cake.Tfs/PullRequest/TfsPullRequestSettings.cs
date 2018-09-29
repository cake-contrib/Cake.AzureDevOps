﻿namespace Cake.Tfs.PullRequest
{
    using System;
    using Cake.Tfs.Authentication;

    /// <summary>
    /// Settings for pull request aliases.
    /// </summary>
    public class TfsPullRequestSettings
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
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            sourceBranch.NotNullOrWhiteSpace(nameof(sourceBranch));
            credentials.NotNull(nameof(credentials));

            this.RepositoryUrl = repositoryUrl;
            this.SourceBranch = sourceBranch;
            this.Credentials = credentials;
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
        {
            repositoryUrl.NotNull(nameof(repositoryUrl));
            pullRequestId.NotNegativeOrZero(nameof(pullRequestId));
            credentials.NotNull(nameof(credentials));

            this.RepositoryUrl = repositoryUrl;
            this.PullRequestId = pullRequestId;
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class
        /// based on the instance of a <see cref="TfsPullRequestSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public TfsPullRequestSettings(TfsPullRequestSettings settings)
        {
            settings.NotNull(nameof(settings));

            this.RepositoryUrl = settings.RepositoryUrl;
            this.SourceBranch = settings.SourceBranch;
            this.PullRequestId = settings.PullRequestId;
            this.Credentials = settings.Credentials;
            this.ThrowExceptionIfPullRequestCouldNotBeFound = settings.ThrowExceptionIfPullRequestCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestSettings"/> class using environment variables.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Team Foundation Server or
        /// Azure DevOps.</param>
        public TfsPullRequestSettings(ITfsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));
            this.Credentials = credentials;

            var repositoryUrl = Environment.GetEnvironmentVariable("BUILD_REPOSITORY_URI", EnvironmentVariableTarget.Process);
            repositoryUrl.NotNullOrWhiteSpace(nameof(repositoryUrl));
            this.RepositoryUrl = new Uri(repositoryUrl);

            var pullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", EnvironmentVariableTarget.Process);
            pullRequestId.NotNullOrWhiteSpace(nameof(pullRequestId));
            if (!int.TryParse(pullRequestId, out int pullRequestIdValue))
            {
                throw new ArgumentException("SYSTEM_PULLREQUEST_PULLREQUESTID should contain integer value", nameof(pullRequestId));
            }

            pullRequestIdValue.NotNegativeOrZero(nameof(pullRequestId));
            this.PullRequestId = pullRequestIdValue;
        }

        /// <summary>
        /// Constructs the settings object using the provided access token.
        /// </summary>
        /// <returns>The instance of <see cref="TfsPullRequestSettings"/> class.</returns>
        public static TfsPullRequestSettings UsingTfsBuildOAuthToken()
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            var creds = new TfsOAuthCredentials(accessToken);
            return new TfsPullRequestSettings(creds);
        }

        /// <summary>
        /// Gets the full URL of the Git repository, eg. <code>http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository</code>.
        /// </summary>
        public Uri RepositoryUrl { get; private set; }

        /// <summary>
        /// Gets the branch for which the pull request is made.
        /// </summary>
        public string SourceBranch { get; private set; }

        /// <summary>
        /// Gets the ID of the pull request.
        /// </summary>
        public int? PullRequestId { get; private set; }

        /// <summary>
        /// Gets the credentials used to authenticate against Team Foundation Server or
        /// Azure DevOps.
        /// </summary>
        public ITfsCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if
        /// pull request for <see cref="SourceBranch"/> or <see cref="PullRequestId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfPullRequestCouldNotBeFound { get; set; } = true;
    }
}
