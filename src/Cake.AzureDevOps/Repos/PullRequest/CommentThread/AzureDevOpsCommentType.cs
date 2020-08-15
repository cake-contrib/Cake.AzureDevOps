namespace Cake.AzureDevOps.Repos.PullRequest.CommentThread
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// The wrapper around the native Azure DevOps API comment type.
    /// </summary>
    public enum AzureDevOpsCommentType
    {
        /// <summary>
        /// Unknown comment type.
        /// </summary>
        Unknown = CommentType.Unknown,

        /// <summary>
        /// The comment is text.
        /// </summary>
        Text = CommentType.Text,

        /// <summary>
        /// The comment is a code change.
        /// </summary>
        CodeChange = CommentType.CodeChange,

        /// <summary>
        /// System comment.
        /// </summary>
        System = CommentType.System,
    }
}
