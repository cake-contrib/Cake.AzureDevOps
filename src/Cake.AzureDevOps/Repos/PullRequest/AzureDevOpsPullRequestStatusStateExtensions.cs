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
            return state switch
            {
                AzureDevOpsPullRequestStatusState.NotSet => GitStatusState.NotSet,
                AzureDevOpsPullRequestStatusState.Pending => GitStatusState.Pending,
                AzureDevOpsPullRequestStatusState.Succeeded => GitStatusState.Succeeded,
                AzureDevOpsPullRequestStatusState.Failed => GitStatusState.Failed,
                AzureDevOpsPullRequestStatusState.Error => GitStatusState.Error,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
