namespace Cake.Tfs.PullRequest
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for the <see cref="TfsPullRequestStatusState"/> class.
    /// </summary>
    internal static class TfsPullRequestStatusStateExtensions
    {
        /// <summary>
        /// Converts a <see cref="TfsPullRequestStatusState"/> to an <see cref="GitStatusState"/>.
        /// </summary>
        /// <param name="state">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static GitStatusState ToGitStatusState(this TfsPullRequestStatusState state)
        {
            switch (state)
            {
                case TfsPullRequestStatusState.NotSet:
                    return GitStatusState.NotSet;
                case TfsPullRequestStatusState.Pending:
                    return GitStatusState.Pending;
                case TfsPullRequestStatusState.Succeeded:
                    return GitStatusState.Succeeded;
                case TfsPullRequestStatusState.Failed:
                    return GitStatusState.Failed;
                case TfsPullRequestStatusState.Error:
                    return GitStatusState.Error;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
