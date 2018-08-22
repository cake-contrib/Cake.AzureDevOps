namespace Cake.Tfs
{
    using Cake.Core;
    using Cake.Core.Annotations;
    using Cake.Tfs.PullRequest;

    /// <content>
    /// Contains functionality related to Team Foundation Server or Visual Studio Team Services pull requests.
    /// </content>
    [CakeNamespaceImport("Cake.Tfs.PullRequest")]
    public static partial class TfsAliases
    {
        /// <summary>
        /// Gets a Team Foundation Server or Visual Studio Team Services pull request using the specified settings.
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
        /// <exception cref="TfsException">If pull request could not be found and
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
        /// Votes for the Team Foundation Server or Visual Studio Team Services pull request
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
        ///         TfsPullRequestVote.Approved);
        /// ]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        public static void TfsPullRequestVote(
            this ICakeContext context,
            TfsPullRequestSettings settings,
            TfsPullRequestVote vote)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new TfsPullRequest(context.Log, settings);
            pullRequest.Vote(vote);
        }
    }
}