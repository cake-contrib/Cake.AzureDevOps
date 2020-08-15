namespace Cake.AzureDevOps.Repos.PullRequest
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for the <see cref="AzureDevOpsPullRequestStatusState"/> class.
    /// </summary>
    internal static class AzureDevOpsPullRequestStatusStateExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsPullRequestStatusState"/> to an <see cref="GitStatusState"/>.
        /// </summary>
        /// <param name="state">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static GitStatusState ToGitStatusState(this AzureDevOpsPullRequestStatusState state)
        {
            switch (state)
            {
                case AzureDevOpsPullRequestStatusState.NotSet:
                    return GitStatusState.NotSet;
                case AzureDevOpsPullRequestStatusState.Pending:
                    return GitStatusState.Pending;
                case AzureDevOpsPullRequestStatusState.Succeeded:
                    return GitStatusState.Succeeded;
                case AzureDevOpsPullRequestStatusState.Failed:
                    return GitStatusState.Failed;
                case AzureDevOpsPullRequestStatusState.Error:
                    return GitStatusState.Error;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
