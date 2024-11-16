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
        /// <param name="thread">The original comment thread in the Azure DevOps pull request.</param>
        internal AzureDevOpsPullRequestCommentThread(GitPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            this.InnerThread = thread;
        }

        /// <summary>
        /// Gets the Id of the pull request comment thread.
        /// </summary>
        public int Id
        {
            get => this.InnerThread.Id;
            init => this.InnerThread.Id = value;
        }

        /// <summary>
        /// Gets the status of the pull request comment thread.
        /// </summary>
        public AzureDevOpsCommentThreadStatus Status
        {
            get => (AzureDevOpsCommentThreadStatus)this.InnerThread.Status;
            init => this.InnerThread.Status = (CommentThreadStatus)value;
        }

        /// <summary>
        /// Gets the path of the modified file the pull request comment thread belongs to.
        /// Returns <c>null</c> for the comment threads not related to any particular file.
        /// </summary>
        public FilePath FilePath
        {
            get
            {
                FilePath filePath = null;
                if (this.InnerThread.ThreadContext?.FilePath != null)
                {
                    filePath = this.InnerThread.ThreadContext.FilePath.TrimStart('/');
                }

                return filePath;
            }

            init
            {
                this.InnerThread.ThreadContext ??= new CommentThreadContext();

                this.InnerThread.ThreadContext.FilePath = value.FullPath;
            }
        }

        /// <summary>
        /// Gets the line number of the right file.
        /// Returns <c>null</c> for the comment threads not related to any particular file and position.
        /// </summary>
        public int? LineNumber
        {
            get => this.InnerThread.ThreadContext?.RightFileStart != null
                ? this.InnerThread.ThreadContext?.RightFileStart.Line
                : null;

            init
            {
                if (value != null)
                {
                    this.EnsureRightFileStartExists();

                    this.InnerThread.ThreadContext.RightFileStart.Line = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets the character offset inside of a line.
        /// Returns <c>null</c> for the comment threads not related to any particular file and position.
        /// </summary>
        public int? Offset
        {
            get => this.InnerThread.ThreadContext?.RightFileStart != null
                ? this.InnerThread.ThreadContext?.RightFileStart.Offset
                : null;

            init
            {
                if (value != null)
                {
                    this.EnsureRightFileStartExists();

                    this.InnerThread.ThreadContext.RightFileStart.Offset = value.Value;
                }
            }
        }

        /// <summary>
        /// Gets the collection of comments in the pull request comment thread.
        /// </summary>
        public IEnumerable<AzureDevOpsComment> Comments
        {
            get => this.InnerThread.Comments == null
                ? throw new InvalidOperationException("Comments list is not created.")
                : this.InnerThread.Comments.Select(x => new AzureDevOpsComment(x, this.InnerThread.Id));

            init =>
                this.InnerThread.Comments =
                    value?
                        .Select(c =>
                            new Comment
                            {
                                Content = c.Content,
                                IsDeleted = c.IsDeleted,
                                CommentType = (CommentType)c.CommentType,
                            }).ToList();
        }

        /// <summary>
        /// Gets the collection of properties of the pull request comment thread.
        /// </summary>
        public IDictionary<string, object> Properties
        {
            get => this.InnerThread.Properties;
            init => this.InnerThread.Properties = value != null ? new PropertiesCollection(value) : null;
        }

        /// <summary>
        /// Gets the inner Git comment thread object. Intended for the internal use only.
        /// </summary>
        internal GitPullRequestCommentThread InnerThread { get; }

        /// <summary>
        /// Gets the value of the thread property.
        /// </summary>
        /// <typeparam name="T">Type of the value.</typeparam>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>Value of the property or default value for <typeparamref name="T"/> if property does not exist.</returns>
        public T GetValue<T>(string propertyName)
        {
            propertyName.NotNullOrWhiteSpace(nameof(propertyName));

            return this.InnerThread.Properties == null ? default : this.InnerThread.Properties.GetValue(propertyName, default(T));
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

            if (this.InnerThread.Properties == null)
            {
                throw new InvalidOperationException("Properties collection is not created.");
            }

            if (this.InnerThread.Properties.ContainsKey(propertyName))
            {
                this.InnerThread.Properties[propertyName] = value;
            }
            else
            {
                this.InnerThread.Properties.Add(propertyName, value);
            }
        }

        private void EnsureRightFileStartExists()
        {
            this.InnerThread.ThreadContext ??= new CommentThreadContext();
            this.InnerThread.ThreadContext.RightFileStart ??= new CommentPosition();
        }
    }
}