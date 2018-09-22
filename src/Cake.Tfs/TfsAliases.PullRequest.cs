namespace Cake.Tfs
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Tfs.PullRequest;

    /// <content>
    /// Contains functionality related to Team Foundation Server or Azure DevOps pull requests.
    /// </content>
    [CakeNamespaceImport("Cake.Tfs.PullRequest")]
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
        public static TfsPullRequest TfsPullRequest(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new TfsPullRequest(context.Log, settings);

            if (pullRequest.HasPullRequestLoaded)
            {
                return pullRequest;
            }

            return null;
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
        public static void TfsVotePullRequest(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestVote vote)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            new TfsPullRequest(context.Log, settings).Vote(vote);
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
        public static void TfsSetPullRequestStatus(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestStatus status)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));
            status.NotNull(nameof(status));

            new TfsPullRequest(context.Log, settings).SetStatus(status);
        }
    }
}