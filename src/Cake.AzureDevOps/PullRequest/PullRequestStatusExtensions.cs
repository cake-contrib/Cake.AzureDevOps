namespace Cake.AzureDevOps.PullRequest
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
            switch (state)
            {
                case PullRequestStatus.NotSet:
                    return AzureDevOpsPullRequestState.NotSet;
                case PullRequestStatus.Active:
                    return AzureDevOpsPullRequestState.Active;
                case PullRequestStatus.Abandoned:
                    return AzureDevOpsPullRequestState.Abandoned;
                case PullRequestStatus.Completed:
                    return AzureDevOpsPullRequestState.Completed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
