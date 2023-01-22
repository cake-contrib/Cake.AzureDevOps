namespace Cake.AzureDevOps.Repos.PullRequest.CommentThread
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
    public class AzureDevOpsPullRequestCommentThread
    {
        private readonly GitPullRequestCommentThread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestCommentThread"/> class.
        /// </summary>
        public AzureDevOpsPullRequestCommentThread()
            : this(new GitPullRequestCommentThread())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestCommentThread"/> class.
        /// </summary>
        /// <param name="thread">The original comment thread in the Azue DevOps pull request.</param>
        internal AzureDevOpsPullRequestCommentThread(GitPullRequestCommentThread thread)
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
        public AzureDevOpsCommentThreadStatus Status
        {
            get => (AzureDevOpsCommentThreadStatus)this.thread.Status;
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
                this.thread.ThreadContext ??= new CommentThreadContext();

                this.thread.ThreadContext.FilePath = value.FullPath;
            }
        }

        /// <summary>
        /// Gets or sets the line number of the right file.
        /// Returns 'null' for the comment threads not related to any particular file and position.
        /// </summary>
        public int? LineNumber
        {
            get
            {
                if (this.thread.ThreadContext?.RightFileStart != null)
                {
                    return this.thread.ThreadContext?.RightFileStart.Line;
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    this.EnsureRightFileStartExists();

                    this.thread.ThreadContext.RightFileStart.Line = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the character offset inside of a line.
        /// Returns 'null' for the comment threads not related to any particular file and position.
        /// </summary>
        public int? Offset
        {
            get
            {
                if (this.thread.ThreadContext?.RightFileStart != null)
                {
                    return this.thread.ThreadContext?.RightFileStart.Offset;
                }

                return null;
            }

            set
            {
                if (value != null)
                {
                    this.EnsureRightFileStartExists();

                    this.thread.ThreadContext.RightFileStart.Offset = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the collection of comments in the pull request comment thread.
        /// </summary>
        public IEnumerable<AzureDevOpsComment> Comments
        {
            get
            {
                if (this.thread.Comments == null)
                {
                    throw new InvalidOperationException("Comments list is not created.");
                }

                return this.thread.Comments.Select(x => new AzureDevOpsComment(x, this.thread.Id));
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
        /// <returns>Value of the property or default value for <typeparamref name="T"/> if property does not exist.</returns>
        public T GetValue<T>(string propertyName)
        {
            propertyName.NotNullOrWhiteSpace(nameof(propertyName));

            if (this.thread.Properties == null)
            {
                return default;
            }

            return this.thread.Properties.GetValue(propertyName, default(T));
        }

        /// <summary>
        /// Sets a value in the thread properties.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">Value to set.</param>
        /// <exception cref="InvalidOperationException">If properties collection is not created.</exception>
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

        private void EnsureRightFileStartExists()
        {
            this.thread.ThreadContext ??= new CommentThreadContext();

            if (this.thread.ThreadContext.RightFileStart == null)
            {
                this.thread.ThreadContext.RightFileStart = new CommentPosition();
            }
        }
    }
}
