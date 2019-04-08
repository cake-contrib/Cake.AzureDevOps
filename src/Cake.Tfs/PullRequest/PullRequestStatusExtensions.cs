namespace Cake.Tfs.PullRequest
{
    using System;
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for the <see cref="PullRequestStatus"/> class.
    /// </summary>
    internal static class PullRequestStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="PullRequestStatus"/> to a <see cref="TfsPullRequestState"/>.
        /// </summary>
        /// <param name="state">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static TfsPullRequestState ToTfsPullRequestState(this PullRequestStatus state)
        {
            switch (state)
            {
                case PullRequestStatus.NotSet:
                    return TfsPullRequestState.NotSet;
                case PullRequestStatus.Active:
                    return TfsPullRequestState.Active;
                case PullRequestStatus.Abandoned:
                    return TfsPullRequestState.Abandoned;
                case PullRequestStatus.Completed:
                    return TfsPullRequestState.Completed;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state));
            }
        }
    }
}
