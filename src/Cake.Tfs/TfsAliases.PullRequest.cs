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
        /// Votes for the Team Foundation Server or Visual Studio Team Services pull request
        /// using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <param name="vote">The vote for the pull request.</param>
        /// <example>
        /// <para>Vote 'Approved' to a TFS pull request:</para>
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

        /// <summary>
        /// Gets the last commit hash on the source branch of the Team Foundation Server or
        /// Visual Studio Team Services pull request using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for accessing the pull request system.</param>
        /// <example>
        /// <para>Get the hash of the last commit:</para>
        /// <code>
        /// <![CDATA[
        ///     var pullRequestSettings =
        ///         new TfsPullRequestSettings(
        ///             new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
        ///             "refs/heads/feature/myfeature",
        ///             TfsAuthenticationNtlm());
        ///
        ///     var hash =
        ///         TfsPullRequestLastSourceCommit(
        ///             pullRequestSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The hash of the last commit on the source branch or <see cref="string.Empty"/>
        /// if no pull request could be found.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Pull Request")]
        public static string TfsPullRequestLastSourceCommit(
            this ICakeContext context,
            TfsPullRequestSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var pullRequest = new TfsPullRequest(context.Log, settings);
            return pullRequest.LastSourceCommitId;
        }
    }
}