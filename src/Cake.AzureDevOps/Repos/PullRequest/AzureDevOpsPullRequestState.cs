namespace Cake.AzureDevOps.Repos.PullRequest
{
    /// <summary>
    /// Possible states of a pull request.
    /// </summary>
    public enum AzureDevOpsPullRequestState : byte
    {
        /// <summary>
        /// Status is not set.
        /// </summary>
        NotSet,

        /// <summary>
        /// Pull request is active.
        /// </summary>
        Active,

        /// <summary>
        /// Pull request is abandoned.
        /// </summary>
        Abandoned,

        /// <summary>
        /// Pull request is completed.
        /// </summary>
        Completed,
    }
}