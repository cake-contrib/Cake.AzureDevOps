namespace Cake.AzureDevOps.PullRequest
{
    /// <summary>
    /// Possible states for a pull request status.
    /// </summary>
    public enum AzureDevOpsPullRequestStatusState : byte
    {
        /// <summary>
        /// Status is not set.
        /// </summary>
        NotSet,

        /// <summary>
        /// Status is pending.
        /// </summary>
        Pending,

        /// <summary>
        /// Status is successful.
        /// </summary>
        Succeeded,

        /// <summary>
        /// Status failed.
        /// </summary>
        Failed,

        /// <summary>
        /// Status showing an error.
        /// </summary>
        Error,
    }
}
