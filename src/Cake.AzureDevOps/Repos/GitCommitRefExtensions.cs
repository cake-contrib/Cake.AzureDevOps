namespace Cake.AzureDevOps.Repos
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Extensions for the <see cref="GitCommitRef"/> class.
    /// </summary>
    internal static class GitCommitRefExtensions
    {
        /// <summary>
        /// Converts a <see cref="GitCommitRef"/> to a <see cref="AzureDevOpsCommit"/>.
        /// </summary>
        /// <param name="commit">Commit to convert.</param>
        /// <returns>Converted commit.</returns>
        public static AzureDevOpsCommit ToAzureDevOpsCommit(this GitCommitRef commit)
        {
            commit.NotNull(nameof(commit));

            return
                new AzureDevOpsCommit
                {
                    Id = commit.CommitId,
                    Message = commit.Comment,
                    IsMessageTruncated = commit.CommentTruncated,
                    ParentIds = commit.Parents,
                    RemoteUrl = commit.RemoteUrl,
                };
        }
    }
}