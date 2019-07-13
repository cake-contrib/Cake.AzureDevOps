namespace Cake.Tfs.PullRequest
{
    /// <summary>
    /// Defines the vote for a Team Foundation Server or Azure DevOps pull request.
    /// </summary>
    public enum TfsPullRequestVote : short
    {
        // See https://www.visualstudio.com/en-us/docs/integrate/api/git/pull-requests/reviewers for infos about the enum values.
        // -10 means "Rejected", -5 means "Waiting for author", 0 means "No response", 5 means "Approved with suggestions", and 10 means "Approved".

        /// <summary>
        /// The changes aren't acceptable.
        /// </summary>
        Rejected = -10,

        /// <summary>
        /// Do not approve the changes, the author should take some actions.
        /// </summary>
        WaitingForAuthor = -5,

        /// <summary>
        /// Reset a previous vote.
        /// </summary>
        ResetFeedback = 0,

        /// <summary>
        /// Approve the changes but let the author know there are some suggestions.
        /// </summary>
        ApprovedWithSuggestions = 5,

        /// <summary>
        /// Approve the changes.
        /// </summary>
        Approved = 10,
    }
}