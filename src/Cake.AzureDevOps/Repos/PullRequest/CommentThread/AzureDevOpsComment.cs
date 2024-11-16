namespace Cake.AzureDevOps.Repos.PullRequest.CommentThread
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Class representing a comment in the pull request comment thread.
    /// </summary>
    public class AzureDevOpsComment
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsComment"/> class.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <param name="isDeleted">Equals 'true' if a comment is deleted. Otherwise, 'false'.</param>
        public AzureDevOpsComment(string content, bool isDeleted)
        {
            content.NotNullOrWhiteSpace(nameof(content));

            this.Comment = new Comment { Content = content, IsDeleted = isDeleted };
            this.ThreadId = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsComment"/> class with empty Git comment.
        /// </summary>
        public AzureDevOpsComment()
            : this(new Comment(), 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsComment"/> class with real Git comment.
        /// </summary>
        /// <param name="comment">The original Azure DevOps pull request comment.</param>
        /// <param name="threadId">The parent thread ID.</param>
        internal AzureDevOpsComment(Comment comment, int threadId)
        {
            comment.NotNull(nameof(comment));
            threadId.NotNegative(nameof(threadId));

            this.Comment = comment;
            this.ThreadId = threadId;
        }

        /// <summary>
        /// Gets the comment id.
        /// </summary>
        public short Id => this.Comment.Id;

        /// <summary>
        /// Gets the thread id.
        /// </summary>
        public int ThreadId { get; }

        /// <summary>
        /// Gets the content of the pull request comment.
        /// </summary>
        public string Content
        {
            get => this.Comment.Content;
            init => this.Comment.Content = value;
        }

        /// <summary>
        /// Gets a value indicating whether the comment is deleted.
        /// </summary>
        public bool IsDeleted
        {
            get => this.Comment.IsDeleted;
            init => this.Comment.IsDeleted = value;
        }

        /// <summary>
        /// Gets the comment type.
        /// </summary>
        public AzureDevOpsCommentType CommentType
        {
            get => (AzureDevOpsCommentType)this.Comment.CommentType;
            init => this.Comment.CommentType = (CommentType)value;
        }

        /// <summary>
        /// Gets the internal comment.
        /// </summary>
        internal Comment Comment { get; }
    }
}