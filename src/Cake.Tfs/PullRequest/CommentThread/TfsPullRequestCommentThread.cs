namespace Cake.Tfs.PullRequest.CommentThread
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.IO;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;

    /// <summary>
    /// Class for dealing with comments in pull requests.
    /// </summary>
    public class TfsPullRequestCommentThread
    {
        private readonly GitPullRequestCommentThread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestCommentThread"/> class.
        /// </summary>
        public TfsPullRequestCommentThread()
            : this(new GitPullRequestCommentThread())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequestCommentThread"/> class.
        /// </summary>
        /// <param name="thread">The original comment thread in TFS or Azue DevOps pull request.</param>
        internal TfsPullRequestCommentThread(GitPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            this.thread = thread;
        }

        /// <summary>
        /// Gets or sets the Id of the pull request comment thread.
        /// </summary>
        public int Id
        {
            get => this.thread.Id;
            set => this.thread.Id = value;
        }

        /// <summary>
        /// Gets or sets the status of the pull request comment thread.
        /// </summary>
        public TfsCommentThreadStatus Status
        {
            get => (TfsCommentThreadStatus)this.thread.Status;
            set => this.thread.Status = (CommentThreadStatus)value;
        }

        /// <summary>
        /// Gets or sets the path of the modified file the pull request comment thread belongs to.
        /// Returns 'null' for the comment threads not related to any particular file.
        /// </summary>
        public FilePath FilePath
        {
            get
            {
                FilePath filePath = null;
                if (this.thread.ThreadContext?.FilePath != null)
                {
                    filePath = this.thread.ThreadContext.FilePath.TrimStart('/');
                }

                return filePath;
            }

            set
            {
                if (this.thread.ThreadContext == null)
                {
                    this.thread.ThreadContext = new CommentThreadContext();
                }

                this.thread.ThreadContext.FilePath = value.FullPath;
            }
        }

        /// <summary>
        /// Gets or sets the collection of comments in the pull request comment thread.
        /// </summary>
        public IEnumerable<TfsComment> Comments
        {
            get
            {
                if (this.thread.Comments == null)
                {
                    throw new InvalidOperationException("Comments list is not created.");
                }

                return this.thread.Comments.Select(x => new TfsComment(x));
            }

            set
            {
                this.thread.Comments = value?.Select(c => new Comment { Content = c.Content, IsDeleted = c.IsDeleted, CommentType = (CommentType)c.CommentType }).ToList();
            }
        }

        /// <summary>
        /// Gets or sets the collection of properties of the pull request comment thread.
        /// </summary>
        public IDictionary<string, object> Properties
        {
            get => this.thread.Properties;
            set => this.thread.Properties = value != null ? new PropertiesCollection(value) : null;
        }

        /// <summary>
        /// Gets the inner Git comment thread object. Intended for the internal use only.
        /// </summary>
        internal GitPullRequestCommentThread InnerThread => this.thread;

        /// <summary>
        /// Gets the value of the thread property.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Value of the property.</returns>
        public T GetValue<T>(string propertyName)
        {
            propertyName.NotNullOrWhiteSpace(nameof(propertyName));

            if (this.thread.Properties == null)
            {
                throw new InvalidOperationException("Properties collection is not created.");
            }

            return this.thread.Properties.GetValue(propertyName, default(T));
        }

        /// <summary>
        /// Sets a value in the thread properties.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">Value to set.</param>
        public void SetValue<T>(string propertyName, T value)
        {
            propertyName.NotNullOrWhiteSpace(nameof(propertyName));

            if (this.thread.Properties == null)
            {
                throw new InvalidOperationException("Properties collection is not created.");
            }

            if (this.thread.Properties.ContainsKey(propertyName))
            {
                this.thread.Properties[propertyName] = value;
            }
            else
            {
                this.thread.Properties.Add(propertyName, value);
            }
        }
    }
}
