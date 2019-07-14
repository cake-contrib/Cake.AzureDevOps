namespace Cake.Tfs.PullRequest.CommentThread
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// The wrapper around the native TFS API comment thread status.
    /// </summary>
    public enum TfsCommentThreadStatus
    {
        /// <summary>
        /// The comment thread is active.
        /// </summary>
        Active = CommentThreadStatus.Active,

        /// <summary>
        /// The comment thread status is by design.
        /// </summary>
        ByDesign = CommentThreadStatus.ByDesign,

        /// <summary>
        /// The comment thread is closed.
        /// </summary>
        Closed = CommentThreadStatus.Closed,

        /// <summary>
        /// The comment thread is fixed.
        /// </summary>
        Fixed = CommentThreadStatus.Fixed,

        /// <summary>
        /// The comment thread is pending.
        /// </summary>
        Pending = CommentThreadStatus.Pending,

        /// <summary>
        /// Unknown comment thread status.
        /// </summary>
        Unknown = CommentThreadStatus.Unknown,

        /// <summary>
        /// The comment thread won't be fixed.
        /// </summary>
        WontFix = CommentThreadStatus.WontFix,
    }
}
