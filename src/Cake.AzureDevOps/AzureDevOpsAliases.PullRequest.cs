namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.PullRequest;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to Azure DevOps pull requests.
    /// </content>
    public static partial class AzureDevOpsAliases
    {
        /// <summary>
        /// Gets an Azure DevOps pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the pull request.</param>
        /// <example>
        /// <para>Get a pull request based on the source branch:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequestSettings =
        ///     new AzureDevOpsPullRequestSettings(
        ///         new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
        ///         "refs/heads/feature/myfeature",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var pullRequest =
        ///     AzureDevOpsPullRequest(
        ///         pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the pull request.
        /// Returns null if pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static AzureDevOpsPullRequest AzureDevOpsPullRequest(
            this ICakeContext context,
            AzureDevOpsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new AzureDevOpsPullRequest(context.Log, settings, new GitClientFactory());

            if (pullRequest.HasPullRequestLoaded)
            {
                return pullRequest;
            }

            return null;
        }

        /// <summary>
        /// Gets an Azure DevOps pull request using the settings provided by an Azure Pipelines build.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <para>Get a pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequest =
        ///     AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken();
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the pull request.</returns>
        /// <exception cref="InvalidOperationException">If build is not running in Azure Pipelines,
        /// build is not for a pull request or 'Allow Scripts to access OAuth token' option is not enabled
        /// on the build definition.</exception>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static AzureDevOpsPullRequest AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return context.AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken(true);
        }

        /// <summary>
        /// Gets an Azure DevOps pull request using the settings provided by an Azure Pipelines build.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="throwExceptionIfPullRequestCouldNotBeFound">Value indicating whether an exception
        /// should be thrown if pull request could not be found.</param>
        /// <example>
        /// <para>Get a pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequest =
        ///     AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken(false);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the pull request.
        /// Returns <c>null</c> if pull request could not be found and
        /// <paramref name="throwExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If build is not running in Azure Pipelines,
        /// build is not for a pull request or 'Allow Scripts to access OAuth token' option is not enabled
        /// on the build definition.</exception>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <paramref name="throwExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static AzureDevOpsPullRequest AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            bool throwExceptionIfPullRequestCouldNotBeFound)
        {
            context.NotNull(nameof(context));

            var settings = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(throwExceptionIfPullRequestCouldNotBeFound);
            settings.ThrowExceptionIfPullRequestCouldNotBeFound = throwExceptionIfPullRequestCouldNotBeFound;

            return AzureDevOpsPullRequest(context, settings);
        }

        /// <summary>
        /// Votes for an Azure DevOps pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request.</param>
        /// <param name="vote">The vote for the pull request.</param>
        /// <example>
        /// <para>Vote 'Approved' to a Azure DevOps Server pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequestSettings =
        ///     new AzureDevOpsPullRequestSettings(
        ///         new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
        ///         "refs/heads/feature/myfeature",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// AzureDevOpsVotePullRequest(
        ///     pullRequestSettings,
        ///     AzureDevOpsVotePullRequest.Approved);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static void AzureDevOpsVotePullRequest(
            this ICakeContext context,
            AzureDevOpsPullRequestSettings settings,
            AzureDevOpsPullRequestVote vote)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            new AzureDevOpsPullRequest(context.Log, settings, new GitClientFactory()).Vote(vote);
        }

        /// <summary>
        /// Sets a status on an Azure DevOps pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request.</param>
        /// <param name="status">Description of the status which should be set.</param>
        /// <example>
        /// <para>Set a custom status on the pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequestSettings =
        ///     new AzureDevOpsPullRequestSettings(
        ///         new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
        ///         "refs/heads/feature/myfeature",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var pullRequstStatus =
        ///     new AzureDevOpsPullRequestStatus("MyStatus")
        ///     {
        ///         Genre = "MyGenre",
        ///         State = AzureDevOpsPullRequestStatusState.Succeeded,
        ///         Description = "My custom status is successful"
        ///     };
        ///
        /// AzureDevOpsSetPullRequestStatus(
        ///     pullRequestSettings,
        ///     pullRequstStatus);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static void AzureDevOpsSetPullRequestStatus(
            this ICakeContext context,
            AzureDevOpsPullRequestSettings settings,
            AzureDevOpsPullRequestStatus status)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));
            status.NotNull(nameof(status));

            new AzureDevOpsPullRequest(context.Log, settings, new GitClientFactory()).SetStatus(status);
        }

        /// <summary>
        /// Adds a new comment thread with a single comment to an Azure DevOps pull request using
        /// the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request.</param>
        /// <param name="comment">The comment which should be added to the pull request.</param>
        /// <example>
        /// <para>Add comment 'Hello World' to pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequestSettings =
        ///     new AzureDevOpsPullRequestSettings(
        ///         new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
        ///         "refs/heads/feature/myfeature",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// AzureDevOpsAddCommentToPullRequest(
        ///     pullRequestSettings,
        ///     "Hello World");
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static void AzureDevOpsAddCommentToPullRequest(
            this ICakeContext context,
            AzureDevOpsPullRequestSettings settings,
            string comment)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));
            comment.NotNullOrWhiteSpace(nameof(comment));

            new AzureDevOpsPullRequest(context.Log, settings, new GitClientFactory())
                .CreateComment(comment);
        }

        /// <summary>
        /// Creates a pull request in Azure DevOps using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for creating the pull request.</param>
        /// <returns>Returns a <see cref="PullRequest.AzureDevOpsPullRequest" /> initialized with the
        /// created pull request.</returns>
        /// <example>
        /// <para>Creates a pull request:</para>
        /// <code>
        /// <![CDATA[
        /// var pullRequestSettings =
        ///     new AzureDevOpsCreatePullRequestSettings(
        ///         new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
        ///         "refs/heads/feature/myfeature",
        ///         "refs/heads/develop",
        ///         "Merge my feature from myfeature",
        ///         "Merge my feature from myfeature",
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// AzureDevOpsCreatePullRequest(pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="AzureDevOpsBranchNotFoundException">If the target branch could not be found.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.AzureDevOps.Repos")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest")]
        [CakeNamespaceImport("Cake.AzureDevOps.PullRequest.CommentThread")]
        public static AzureDevOpsPullRequest AzureDevOpsCreatePullRequest(
            this ICakeContext context,
            AzureDevOpsCreatePullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            context.NotNull(nameof(settings));

            return PullRequest.AzureDevOpsPullRequest.Create(context.Log, new GitClientFactory(), settings);
        }
    }
}