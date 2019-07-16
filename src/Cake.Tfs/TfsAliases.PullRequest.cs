namespace Cake.Tfs
{
    using System;
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Tfs.PullRequest;

    /// <content>
    /// Contains functionality related to Team Foundation Server or Azure DevOps pull requests.
    /// </content>
    public static partial class TfsAliases
    {
        /// <summary>
        /// Gets a Team Foundation Server or Azure DevOps pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the pull request.</param>
        /// <example>
        /// <para>Get a pull request based on the source branch:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     var pullRequest =
        ///         TfsPullRequest(
        ///             pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the pull request.
        /// Returns null if pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest.CommentThread")]
        public static TfsPullRequest TfsPullRequest(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new TfsPullRequest(context.Log, settings, new GitClientFactory());

            if (pullRequest.HasPullRequestLoaded)
            {
                return pullRequest;
            }

            return null;
        }

        /// <summary>
        /// Gets a Team Foundation Server or Azure DevOps pull request using the settings provided by an
        /// Azure Pipelines or Team Foundation Server build.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <example>
        /// <para>Get a pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequest =
        ///         TfsPullRequestUsingTfsBuildOAuthToken();
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the pull request.
        /// Returns null if pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        /// <exception cref="InvalidOperationException">If build is not running in Azure Pipelines or Team Foundation Server build or
        /// 'Allow Scripts to access OAuth token' option is not enabled on the build definition.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest.CommentThread")]
        public static TfsPullRequest TfsPullRequestUsingTfsBuildOAuthToken(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            var settings = TfsPullRequestSettings.UsingTfsBuildOAuthToken();

            return TfsPullRequest(context, settings);
        }

        /// <summary>
        /// Votes for the Team Foundation Server or Azure DevOps pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request.</param>
        /// <param name="vote">The vote for the pull request.</param>
        /// <example>
        /// <para>Vote 'Approved' to a Team Foundation Server pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     TfsVotePullRequest(
        ///         pullRequestSettings,
        ///         TfsVotePullRequest.Approved);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest.CommentThread")]
        public static void TfsVotePullRequest(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestVote vote)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            new TfsPullRequest(context.Log, settings, new GitClientFactory()).Vote(vote);
        }

        /// <summary>
        /// Sets a status on a Team Foundation Server or Azure DevOps pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request.</param>
        /// <param name="status">Description of the status which should be set.</param>
        /// <example>
        /// <para>Set a custom status on the pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     var pullRequstStatus =
        ///         new TfsPullRequestStatus("MyStatus")
        ///         {
        ///             Genre = "MyGenre",
        ///             State = TfsPullRequestStatusState.Succeeded,
        ///             Description = "My custom status is successful"
        ///         }
        ///
        ///     TfsSetPullRequestStatus(
        ///         pullRequestSettings,
        ///         pullRequstStatus);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest.CommentThread")]
        public static void TfsSetPullRequestStatus(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestStatus status)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));
            status.NotNull(nameof(status));

            new TfsPullRequest(context.Log, settings, new GitClientFactory()).SetStatus(status);
        }

        /// <summary>
        /// Creates a pull request in Team Foundation Server or Azure DevOps
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for creating the pull request.</param>
        /// <returns>Returns a <see cref="PullRequest.TfsPullRequest" /> initialized with the
        /// created pull request.</returns>
        /// <example>
        /// <para>Creates a pull request:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsCreatePullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             "refs/heads/develop",
        ///             "Merge my feature from myfeature",
        ///             "Merge my feature from myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     TfsCreatePullRequest(pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <exception cref="TfsBranchNotFoundException">If the target branch could not be found.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest")]
        [CakeNamespaceImport("Cake.Tfs.PullRequest.CommentThread")]
        public static TfsPullRequest TfsCreatePullRequest(
            this ICakeContext context,
            TfsCreatePullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            context.NotNull(nameof(settings));

            return PullRequest.TfsPullRequest.Create(context.Log, settings);
        }
    }
}