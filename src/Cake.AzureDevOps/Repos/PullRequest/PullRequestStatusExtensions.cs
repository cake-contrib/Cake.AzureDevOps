namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for the <see cref="PullRequestStatus"/> class.
    /// </summary>
    internal static class PullRequestStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="PullRequestStatus"/> to a <see cref="AzureDevOpsPullRequestState"/>.
        /// </summary>
        /// <param name="state">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static AzureDevOpsPullRequestState ToAzureDevOpsPullRequestState(this PullRequestStatus state)
        {
            return state switch
            {
                PullRequestStatus.NotSet => AzureDevOpsPullRequestState.NotSet,
                PullRequestStatus.Active => AzureDevOpsPullRequestState.Active,
                PullRequestStatus.Abandoned => AzureDevOpsPullRequestState.Abandoned,
                PullRequestStatus.Completed => AzureDevOpsPullRequestState.Completed,
                _ => throw new ArgumentOutOfRangeException(nameof(state)),
            };
        }
    }
}
