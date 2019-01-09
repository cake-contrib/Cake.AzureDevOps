namespace Cake.Tfs.PullRequest.CommentThread
{
    using Microsoft.TeamFoundation.SourceControl.WebApi;

    /// <summary>
    /// Class representing a comment in the pull request comment thread.
    /// </summary>
    public class TfsComment
    {
        private readonly Comment comment;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsComment"/> class.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <param name="isDeleted">Equals 'true' if a comment is deleted. Otherwise, 'false'.</param>
        public TfsComment(string content, bool isDeleted)
        {
            content.NotNullOrWhiteSpace(nameof(content));

            this.comment = new Comment { Content = content, IsDeleted = isDeleted };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsComment"/> class.
        /// </summary>
        /// <param name="content">The content of the comment.</param>
        /// <param name="isDeleted">Equals 'true' if a comment is deleted. Otherwise, 'false'.</param>
        /// <param name="commentType">The type of the comment.</param>
        public TfsComment(string content, bool isDeleted, CommentType commentType)
        {
            content.NotNullOrWhiteSpace(nameof(content));

            this.comment = new Comment { Content = content, IsDeleted = isDeleted, CommentType = commentType };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsComment"/> class with empty Git comment.
        /// </summary>
        public TfsComment()
            : this(new Comment())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsComment"/> class with real Git comment.
        /// </summary>
        /// <param name="comment">The original TFS or Azure DevOps pull request comment.</param>
        internal TfsComment(Comment comment)
        {
            comment.NotNull(nameof(comment));

            this.comment = comment;
        }

        /// <summary>
        /// Gets or sets the content of the pull request comment.
        /// </summary>
        public string Content
        {
            get => this.comment.Content;
            set => this.comment.Content = value;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the comment is deleted.
        /// </summary>
        public bool IsDeleted
        {
            get => this.comment.IsDeleted;
            set => this.comment.IsDeleted = value;
        }

        /// <summary>
        /// Gets or sets the comment type.
        /// </summary>
        public TfsCommentType CommentType
        {
            get => (TfsCommentType)this.comment.CommentType;
            set => this.comment.CommentType = (CommentType)value;
        }
    }
}
