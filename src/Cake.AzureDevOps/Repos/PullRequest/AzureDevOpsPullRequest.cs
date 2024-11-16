namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos;
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;
    using Cake.Core.Diagnostics;
    using Cake.Core.IO;
    using Microsoft.TeamFoundation.Common;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using TfsUrlParser;

    /// <summary>
    /// Class for writing issues to Azure DevOps pull requests.
    /// </summary>
    public sealed class AzureDevOpsPullRequest
    {
        private readonly ICakeLog log;
        private readonly IAzureDevOpsCredentials credentials;
        private readonly bool throwExceptionIfPullRequestCouldNotBeFound;
        private readonly IGitClientFactory gitClientFactory;
        private readonly RepositoryDescription repositoryDescription;
        private readonly GitPullRequest pullRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequest"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// is set to <c>true</c> and no pull request could be found.</exception>
        public AzureDevOpsPullRequest(ICakeLog log, AzureDevOpsPullRequestSettings settings)
            : this(log, settings, new GitClientFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequest"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <param name="gitClientFactory">A factory to communicate with Git client.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// is set to <c>true</c> and no pull request could be found.</exception>
        internal AzureDevOpsPullRequest(ICakeLog log, AzureDevOpsPullRequestSettings settings, IGitClientFactory gitClientFactory)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            gitClientFactory.NotNull(nameof(gitClientFactory));

            this.log = log;
            this.gitClientFactory = gitClientFactory;
            this.credentials = settings.Credentials;
            this.throwExceptionIfPullRequestCouldNotBeFound = settings.ThrowExceptionIfPullRequestCouldNotBeFound;

            this.repositoryDescription = new RepositoryDescription(settings.RepositoryUrl);

            this.log.Verbose(
                "Repository information:\n  CollectionName: {0}\n  CollectionUrl: {1}\n  ProjectName: {2}\n  RepositoryName: {3}",
                this.repositoryDescription.CollectionName,
                this.repositoryDescription.CollectionUrl,
                this.repositoryDescription.ProjectName,
                this.repositoryDescription.RepositoryName);

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.repositoryDescription.CollectionUrl, settings.Credentials, out var authorizedIdentity))
            {
                this.log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdentity.Id,
                     authorizedIdentity.DisplayName);

                if (settings.PullRequestId.HasValue)
                {
                    this.log.Verbose("Read pull request with ID {0}", settings.PullRequestId.Value);
                    this.pullRequest =
                        gitClient
                            .GetPullRequestAsync(
                                this.repositoryDescription.ProjectName,
                                this.repositoryDescription.RepositoryName,
                                settings.PullRequestId.Value)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                }
                else if (!string.IsNullOrWhiteSpace(settings.SourceRefName))
                {
                    this.log.Verbose("Read pull request for branch {0}", settings.SourceRefName);

                    var pullRequestSearchCriteria =
                        new GitPullRequestSearchCriteria()
                        {
                            Status = Microsoft.TeamFoundation.SourceControl.WebApi.PullRequestStatus.Active,
                            SourceRefName = settings.SourceRefName,
                        };

                    this.pullRequest =
                        gitClient
                            .GetPullRequestsAsync(
                                this.repositoryDescription.ProjectName,
                                this.repositoryDescription.RepositoryName,
                                pullRequestSearchCriteria,
                                top: 1)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult()
                            .SingleOrDefault();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(settings),
                        "Either PullRequestId or SourceRefName needs to be set");
                }
            }

            if (this.pullRequest == null)
            {
                if (this.throwExceptionIfPullRequestCouldNotBeFound)
                {
                    throw new AzureDevOpsPullRequestNotFoundException("Pull request not found");
                }

                this.log.Warning("Could not find pull request");
                return;
            }

            this.log.Verbose(
                "Pull request information:\n  PullRequestId: {0}\n  RepositoryId: {1}\n  RepositoryName: {2}\n  SourceRefName: {3}",
                this.pullRequest.PullRequestId,
                this.pullRequest.Repository.Id,
                this.pullRequest.Repository.Name,
                this.pullRequest.SourceRefName);
        }

        /// <summary>
        /// Gets a value indicating whether a pull request has been successfully loaded.
        /// </summary>
        public bool HasPullRequestLoaded => this.pullRequest != null;

        /// <summary>
        /// Gets the Url of the Azure DevOps server.
        /// </summary>
        public Uri ServerUrl => this.repositoryDescription.ServerUrl;

        /// <summary>
        /// Gets the name of the Azure DevOps collection.
        /// </summary>
        public string CollectionName => this.repositoryDescription.CollectionName;

        /// <summary>
        /// Gets the URL for accessing the web portal of the Azure DevOps collection.
        /// </summary>
        public Uri CollectionUrl => this.repositoryDescription.CollectionUrl;

        /// <summary>
        /// Gets the name of the Azure DevOps project.
        /// </summary>
        public string ProjectName => this.repositoryDescription.ProjectName;

        /// <summary>
        /// Gets the name of the Git repository.
        /// </summary>
        public string RepositoryName => this.repositoryDescription.RepositoryName;

        /// <summary>
        /// Gets the ID of the repository.
        /// Returns <see cref="Guid.Empty"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public Guid RepositoryId => this.ValidatePullRequest() ? this.pullRequest.Repository.Id : Guid.Empty;

        /// <summary>
        /// Gets the ID of the pull request.
        /// Returns 0 if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int PullRequestId => this.ValidatePullRequest() ? this.pullRequest.PullRequestId : 0;

        /// <summary>
        /// Gets if the pull request is in draft mode.
        /// Returns <see cref="AzureDevOpsPullRequestState.NotSet"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public bool? IsDraft => this.ValidatePullRequest() ? this.pullRequest.IsDraft : null;

        /// <summary>
        /// Gets the status of the pull request.
        /// Returns <see cref="AzureDevOpsPullRequestState.NotSet"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsPullRequestState PullRequestStatus =>
            this.ValidatePullRequest()
                ? this.pullRequest.Status.ToAzureDevOpsPullRequestState()
                : AzureDevOpsPullRequestState.NotSet;

        /// <summary>
        /// Gets the ID of the code review.
        /// Returns 0 if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int CodeReviewId => this.ValidatePullRequest() ? this.pullRequest.CodeReviewId : 0;

        /// <summary>
        /// Gets the name of the source branch from the pull request.
        /// </summary>
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false.</c>.
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string SourceRefName => this.ValidatePullRequest() ? this.pullRequest.SourceRefName : string.Empty;

        /// <summary>
        /// Gets the name of the target branch.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string TargetRefName => this.ValidatePullRequest() ? this.pullRequest.TargetRefName : string.Empty;

        /// <summary>
        /// Gets the commit at the head of the source branch at the time of the last pull request merge.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string LastSourceCommitId =>
            this.ValidatePullRequest()
                ? this.pullRequest.LastMergeSourceCommit.CommitId
                : string.Empty;

        /// <summary>
        /// Gets the commit at the head of the target branch at the time of the last pull request merge.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string LastTargetCommitId =>
            this.ValidatePullRequest()
                ? this.pullRequest.LastMergeTargetCommit.CommitId
                : string.Empty;

        /// <summary>
        /// Create a pull request.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <returns>Instance of the created pull request.</returns>
        public static AzureDevOpsPullRequest Create(ICakeLog log, AzureDevOpsCreatePullRequestSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            return Create(log, new GitClientFactory(), settings);
        }

        /// <summary>
        /// Votes for the pull request.
        /// </summary>
        /// <param name="vote">The vote for the pull request.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void Vote(AzureDevOpsPullRequestVote vote)
        {
            if (!this.ValidatePullRequest())
            {
                return;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials, out var authorizedIdentity))
            {
                var createdReviewer =
                    gitClient
                        .CreatePullRequestReviewerAsync(
                            new IdentityRefWithVote() { Vote = (short)vote },
                            this.pullRequest.Repository.Id,
                            this.pullRequest.PullRequestId,
                            authorizedIdentity.Id.ToString())
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult() ??
                    throw new AzureDevOpsPullRequestNotFoundException(
                        this.pullRequest.Repository.Id,
                        this.pullRequest.PullRequestId);
                var createdVote = (AzureDevOpsPullRequestVote)createdReviewer.Vote;
                this.log.Verbose("Voted for pull request with '{0}'.", createdVote.ToString());
            }
        }

        /// <summary>
        /// Sets a status on the pull request.
        /// </summary>
        /// <param name="status">The description of the status which should be set.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void SetStatus(AzureDevOpsPullRequestStatus status)
        {
            status.NotNull(nameof(status));

            if (!this.ValidatePullRequest())
            {
                return;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var postedStatus =
                    gitClient
                        .CreatePullRequestStatusAsync(
                            new GitPullRequestStatus
                            {
                                State = status.State.ToGitStatusState(),
                                Description = status.Description,
                                TargetUrl = status.TargetUrl?.ToString(),
                                Context = new GitStatusContext()
                                {
                                    Name = status.Name,
                                    Genre = status.Genre,
                                },
                            },
                            this.pullRequest.Repository.Id,
                            this.pullRequest.PullRequestId)
                       .ConfigureAwait(false)
                       .GetAwaiter()
                       .GetResult() ??
                    throw new AzureDevOpsPullRequestNotFoundException(
                        this.pullRequest.Repository.Id,
                        this.pullRequest.PullRequestId);
                this.log.Verbose(
                    "Set status '{0}' to {1}.",
                    postedStatus.Context?.Name,
                    postedStatus.State.ToString());
            }
        }

        /// <summary>
        /// Gets the commits contained in the pull request.
        /// </summary>
        /// <returns>The commits contained in the pull request or an empty list if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<AzureDevOpsCommit> GetCommits()
        {
            if (!this.ValidatePullRequest())
            {
                return [];
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                return
                    gitClient
                        .GetPullRequestCommitsAsync(this.RepositoryId, this.PullRequestId)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult()
                        .Select(x => x.ToAzureDevOpsCommit());
            }
        }

        /// <summary>
        /// Gets the files modified by the pull request.
        /// </summary>
        /// <returns>The collection of the modified files paths or an empty list if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<FilePath> GetModifiedFiles()
        {
            if (!this.ValidatePullRequest())
            {
                return [];
            }

            var targetVersionDescriptor = new GitTargetVersionDescriptor
            {
                VersionType = GitVersionType.Commit,
                Version = this.LastSourceCommitId,
            };

            var baseVersionDescriptor = new GitBaseVersionDescriptor
            {
                VersionType = GitVersionType.Commit,
                Version = this.LastTargetCommitId,
            };

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var commitDiffs =
                    gitClient
                        .GetCommitDiffsAsync(
                        this.ProjectName,
                        this.RepositoryId,
                        true, // bool? diffCommonCommit
                        null, // int? top
                        null, // int? skip
                        baseVersionDescriptor,
                        targetVersionDescriptor)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                if (commitDiffs.ChangeCounts.Count == 0)
                {
                    this.log.Verbose("Found 0 changed files in the pull request");

                    return [];
                }

                var changes =
                    commitDiffs.Changes
                        .Where(x => x != null && !x.Item.IsFolder)
                        .Select(x => new FilePath(x.Item.Path.TrimStart('/')))
                        .ToList();

                this.log.Verbose(
                    "Found {0} changed file(s) in the pull request",
                    changes.Count);

                return changes;
            }
        }

        /// <summary>
        /// Gets the pull request comment threads.
        /// </summary>
        /// <returns>The list of comment threads of the pull request or an empty list if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<AzureDevOpsPullRequestCommentThread> GetCommentThreads()
        {
            if (!this.ValidatePullRequest())
            {
                return [];
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                return
                    gitClient
                        .GetThreadsAsync(this.RepositoryId, this.PullRequestId)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult()
                        .Select(x => new AzureDevOpsPullRequestCommentThread(x));
            }
        }

        /// <summary>
        /// Sets the pull request comment thread status to <see cref="CommentThreadStatus.Fixed"/>.
        /// </summary>
        /// <param name="threadId">The Id of the comment thread.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void ResolveCommentThread(int threadId)
        {
            this.SetCommentThreadStatus(threadId, CommentThreadStatus.Fixed);
        }

        /// <summary>
        /// Sets the pull request comment thread to <see cref="CommentThreadStatus.Active"/>.
        /// </summary>
        /// <param name="threadId">The Id of the comment thread.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void ActivateCommentThread(int threadId)
        {
            this.SetCommentThreadStatus(threadId, CommentThreadStatus.Active);
        }

        /// <summary>
        /// Sets the pull request comment thread to <see cref="CommentThreadStatus.Closed"/>.
        /// </summary>
        /// <param name="threadId">The Id of the comment thread.</param>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void CloseCommentThread(int threadId)
        {
            this.SetCommentThreadStatus(threadId, CommentThreadStatus.Closed);
        }

        /// <summary>
        /// Creates a new comment thread with a single comment in the pull request.
        /// </summary>
        /// <param name="comment">Comment which should be added.</param>
        /// <returns>A newly created comment thread, or <see langword="null"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsPullRequestCommentThread CreateComment(string comment)
        {
            comment.NotNullOrWhiteSpace(nameof(comment));

            var thread = new AzureDevOpsPullRequestCommentThread
            {
                Status = AzureDevOpsCommentThreadStatus.Active,
                Comments =
                    [
                        new ()
                        {
                            CommentType = AzureDevOpsCommentType.System,
                            IsDeleted = false,
                            Content = comment,
                        },
                    ],
            };

            return this.CreateCommentThread(thread);
        }

        /// <summary>
        /// Creates a new comment thread for the given file and position.
        /// </summary>
        /// <param name="comment">Comment which should be added.</param>
        /// <param name="filePath">Path to the file to create the comment for.</param>
        /// <param name="lineNumber">The line number of a thread's position. Starts at 1.</param>
        /// <param name="offset">The character offset of a thread's position inside of a line. Starts at 0.</param>
        /// <returns>A newly created comment thread, or <see langword="null"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsPullRequestCommentThread CreateComment(string comment, FilePath filePath, int lineNumber, int offset)
        {
            comment.NotNullOrWhiteSpace(nameof(comment));
            filePath.NotNullOrWhiteSpace(nameof(filePath));
            lineNumber.NotNegativeOrZero(nameof(lineNumber));
            offset.NotNegative(nameof(offset));

            var thread = new AzureDevOpsPullRequestCommentThread
            {
                Status = AzureDevOpsCommentThreadStatus.Active,
                Comments =
                    [
                        new ()
                        {
                            CommentType = AzureDevOpsCommentType.System,
                            IsDeleted = false,
                            Content = comment,
                        },
                    ],
                FilePath = filePath,
                LineNumber = lineNumber,
                Offset = offset,
            };

            return this.CreateCommentThread(thread);
        }

        /// <summary>
        /// Deletes the comment in the given thread.
        /// </summary>
        /// <param name="threadId">The id of the thread containing the comment.</param>
        /// <param name="commentId">The id of the comment to delete.</param>
        public void DeleteComment(int threadId, int commentId)
        {
            threadId.NotNegativeOrZero(nameof(threadId));
            commentId.NotNegativeOrZero(nameof(commentId));

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                gitClient
                    .DeleteCommentAsync(
                        this.RepositoryId,
                        this.PullRequestId,
                        threadId,
                        commentId)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        /// <summary>
        /// Deletes the comment.
        /// </summary>
        /// <param name="comment">The comment to delete.</param>am>
        public void DeleteComment(AzureDevOpsComment comment)
        {
            comment.NotNull(nameof(comment));

            this.DeleteComment(comment.ThreadId, comment.Id);
        }

        /// <summary>
        /// Updates the comment.
        /// </summary>
        /// <param name="comment">The updated comment.</param>
        /// <returns>The updated comment, or <see langword="null"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsComment UpdateComment(AzureDevOpsComment comment)
        {
            comment.NotNull(nameof(comment));

            if (!this.ValidatePullRequest())
            {
                return null;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var newComment = gitClient
                    .UpdateCommentAsync(
                        comment.Comment,
                        this.RepositoryId,
                        this.PullRequestId,
                        comment.ThreadId,
                        comment.Id)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                if (newComment != null)
                {
                    return new AzureDevOpsComment(newComment, comment.ThreadId);
                }
            }

            return null;
        }

        /// <summary>
        /// Creates a new comment thread in the pull request.
        /// </summary>
        /// <param name="thread">The instance of the thread.</param>
        /// <returns>A newly created comment thread, or <see langword="null"/> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsPullRequestCommentThread CreateCommentThread(AzureDevOpsPullRequestCommentThread thread)
        {
            thread.NotNull(nameof(thread));

            if (!this.ValidatePullRequest())
            {
                return null;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var newThread = gitClient.CreateThreadAsync(
                        thread.InnerThread,
                        this.RepositoryId,
                        this.PullRequestId)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();

                if (newThread != null)
                {
                    return new AzureDevOpsPullRequestCommentThread(newThread);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the Id of the latest pull request iteration.
        /// </summary>
        /// <returns>The Id of the pull request iteration or <c>-1</c> if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        /// <exception cref="AzureDevOpsException">If it is not possible to obtain a collection of <see cref="GitPullRequestIteration"/>.</exception>
        public int GetLatestIterationId()
        {
            if (!this.ValidatePullRequest())
            {
                return -1;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var iterations =
                    gitClient
                        .GetPullRequestIterationsAsync(
                            this.RepositoryId,
                            this.PullRequestId)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult() ??
                    throw new AzureDevOpsException("Could not retrieve the iterations");
                return iterations.Max(x => x.Id ?? -1);
            }
        }

        /// <summary>
        /// Gets all the pull request changes of the given iteration.
        /// </summary>
        /// <param name="iterationId">The id of the iteration.</param>
        /// <returns>The collection of the iteration changes of the given id or an empty list if no pull request could be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>..</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<AzureDevOpsPullRequestIterationChange> GetIterationChanges(int iterationId)
        {
            if (!this.ValidatePullRequest())
            {
                return [];
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var changes =
                    gitClient
                        .GetPullRequestIterationChangesAsync(
                            this.RepositoryId,
                            this.PullRequestId,
                            iterationId)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                return
                    changes?.ChangeEntries.Select(c =>
                        new AzureDevOpsPullRequestIterationChange
                        {
                            ChangeId = c.ChangeId,
                            ChangeTrackingId = c.ChangeTrackingId,
                            ItemPath = c.Item.Path.IsNullOrEmpty() ? null : new FilePath(c.Item.Path),
                        });
            }
        }

        /// <summary>
        /// Create a pull request.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="gitClientFactory">Git client factory.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <returns>Instance of the created pull request.</returns>
        internal static AzureDevOpsPullRequest Create(ICakeLog log, IGitClientFactory gitClientFactory, AzureDevOpsCreatePullRequestSettings settings)
        {
            log.NotNull(nameof(log));
            gitClientFactory.NotNull(nameof(gitClientFactory));
            settings.NotNull(nameof(settings));

            var repoDesc = new RepositoryDescription(settings.RepositoryUrl);

            using (var gitClient = gitClientFactory.CreateGitClient(repoDesc.CollectionUrl, settings.Credentials))
            {
                var repository =
                    gitClient
                        .GetRepositoryAsync(repoDesc.ProjectName, repoDesc.RepositoryName)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult() ??
                    throw new AzureDevOpsException("Could not read repository.");
                var targetBranchName = settings.TargetRefName;
                targetBranchName ??= repository.DefaultBranch;

                var refs =
                    gitClient
                        .GetRefsAsync(
                            repoDesc.ProjectName,
                            repoDesc.RepositoryName,
                            filter: targetBranchName.Replace("refs/", string.Empty))
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult() ??
                    throw new AzureDevOpsBranchNotFoundException(targetBranchName);
                var targetBranch =
                    refs.SingleOrDefault() ??
                    throw new AzureDevOpsBranchNotFoundException(targetBranchName);
                var pr = new GitPullRequest()
                {
                    SourceRefName = settings.SourceRefName,
                    TargetRefName = targetBranch.Name,
                    Title = settings.Title,
                    Description = settings.Description,
                };

                var createdPullRequest =
                    gitClient
                        .CreatePullRequestAsync(
                            pr,
                            repoDesc.ProjectName,
                            repoDesc.RepositoryName)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                var pullRequestReadSettings =
                    new AzureDevOpsPullRequestSettings(
                        settings.RepositoryUrl,
                        createdPullRequest.PullRequestId,
                        settings.Credentials);

                return new AzureDevOpsPullRequest(log, pullRequestReadSettings, gitClientFactory);
            }
        }

        /// <summary>
        /// Sets the pull request comment thread status.
        /// </summary>
        /// <param name="threadId">The Id of the comment thread.</param>
        /// <param name="status">The comment thread status.</param>
        private void SetCommentThreadStatus(int threadId, CommentThreadStatus status)
        {
            if (!this.ValidatePullRequest())
            {
                return;
            }

            using (var gitClient = this.gitClientFactory.CreateGitClient(this.CollectionUrl, this.credentials))
            {
                var newThread = new GitPullRequestCommentThread
                {
                    Status = status,
                };

                gitClient
                    .UpdateThreadAsync(
                        newThread,
                        this.RepositoryId,
                        this.PullRequestId,
                        threadId)
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult();
            }
        }

        /// <summary>
        /// Validates if a pull request could be found.
        /// Depending on <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// the pull request instance can be <see langword="null"/> for subsequent calls.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        /// <exception cref="AzureDevOpsPullRequestNotFoundException">If <see cref="AzureDevOpsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// is set to <c>true</c> and no pull request could be found.</exception>
        private bool ValidatePullRequest()
        {
            if (this.HasPullRequestLoaded)
            {
                return true;
            }

            if (this.throwExceptionIfPullRequestCouldNotBeFound)
            {
                throw new AzureDevOpsPullRequestNotFoundException("Pull request not found");
            }

            this.log.Verbose("Skipping, since no pull request instance could be found.");
            return false;
        }
    }
}
