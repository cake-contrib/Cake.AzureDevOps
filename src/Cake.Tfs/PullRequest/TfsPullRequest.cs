namespace Cake.Tfs.PullRequest
{
    using System;
    using System.Linq;
    using System.Threading;
    using Cake.Core.Diagnostics;
    using Cake.Tfs.Authentication;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.Identity;
    using Microsoft.VisualStudio.Services.WebApi;
    using TfsUrlParser;

    /// <summary>
    /// Class for writing issues to Team Foundation Server or Azure DevOps pull requests.
    /// </summary>
    public sealed class TfsPullRequest
    {
        private readonly ICakeLog log;
        private readonly TfsPullRequestSettings settings;
        private readonly RepositoryDescription repositoryDescription;
        private readonly GitPullRequest pullRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsPullRequest"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing TFS.</param>
        /// <exception cref="TfsPullRequestNotFoundException">If <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// is set to <c>true</c> and no pull request could be found.</exception>
        public TfsPullRequest(ICakeLog log, TfsPullRequestSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            this.log = log;
            this.settings = settings;

            this.repositoryDescription = new RepositoryDescription(settings.RepositoryUrl);

            this.log.Verbose(
                "Repository information:\n  CollectionName: {0}\n  CollectionUrl: {1}\n  ProjectName: {2}\n  RepositoryName: {3}",
                this.repositoryDescription.CollectionName,
                this.repositoryDescription.CollectionUrl,
                this.repositoryDescription.ProjectName,
                this.repositoryDescription.RepositoryName);

            using (var gitClient = this.CreateGitClient(out var authorizedIdenity))
            {
                this.log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdenity.Id,
                     authorizedIdenity.DisplayName);

                if (settings.PullRequestId.HasValue)
                {
                    this.log.Verbose("Read pull request with ID {0}", settings.PullRequestId.Value);
                    this.pullRequest =
                        gitClient.GetPullRequestAsync(
                            this.repositoryDescription.ProjectName,
                            this.repositoryDescription.RepositoryName,
                            settings.PullRequestId.Value).Result;
                }
                else if (!string.IsNullOrWhiteSpace(settings.SourceBranch))
                {
                    this.log.Verbose("Read pull request for branch {0}", settings.SourceBranch);

                    var pullRequestSearchCriteria =
                        new GitPullRequestSearchCriteria()
                        {
                            Status = PullRequestStatus.Active,
                            SourceRefName = settings.SourceBranch
                        };

                    this.pullRequest =
                        gitClient.GetPullRequestsAsync(
                            this.repositoryDescription.ProjectName,
                            this.repositoryDescription.RepositoryName,
                            pullRequestSearchCriteria,
                            top: 1).Result.SingleOrDefault();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(settings),
                        "Either PullRequestId or SourceBranch needs to be set");
                }
            }

            if (this.pullRequest == null)
            {
                if (this.settings.ThrowExceptionIfPullRequestCouldNotBeFound)
                {
                    throw new TfsPullRequestNotFoundException("Pull request not found");
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
        /// Gets the Url of the Team Foundation Server or Azure DevOps.
        /// </summary>
        public Uri ServerUrl => this.repositoryDescription.ServerUrl;

        /// <summary>
        /// Gets the name of the Team Foundation Server or Azure DevOps collection.
        /// </summary>
        public string CollectionName => this.repositoryDescription.CollectionName;

        /// <summary>
        /// Gets the URL for accessing the web portal of the Team Foundation Server or
        /// Azure DevOps collection.
        /// </summary>
        public Uri CollectionUrl => this.repositoryDescription.CollectionUrl;

        /// <summary>
        /// Gets the name of the Team Foundation Server or Azure DevOps project.
        /// </summary>
        public string ProjectName => this.repositoryDescription.ProjectName;

        /// <summary>
        /// Gets the name of the Git repository.
        /// </summary>
        public string RepositoryName => this.repositoryDescription.RepositoryName;

        /// <summary>
        /// Gets the ID of the repository.
        /// Returns <see cref="Guid.Empty"/> if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public Guid RepositoryId
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return Guid.Empty;
                }

                return this.pullRequest.Repository.Id;
            }
        }

        /// <summary>
        /// Gets the ID of the pull request.
        /// Returns 0 if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int PullRequestId
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return 0;
                }

                return this.pullRequest.PullRequestId;
            }
        }

        /// <summary>
        /// Gets the ID of the code review.
        /// Returns 0 if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int CodeReviewId
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return 0;
                }

                return this.pullRequest.CodeReviewId;
            }
        }

        /// <summary>
        /// Gets the name of the target branch.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string TargetRefName
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return string.Empty;
                }

                return this.pullRequest.TargetRefName;
            }
        }

        /// <summary>
        /// Gets the commit at the head of the source branch at the time of the last pull request merge.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string LastSourceCommitId
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return string.Empty;
                }

                return this.pullRequest.LastMergeSourceCommit.CommitId;
            }
        }

        /// <summary>
        /// Gets the commit at the head of the target branch at the time of the last pull request merge.
        /// Returns <see cref="string.Empty"/> if no pull request could be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string LastTargetCommitId
        {
            get
            {
                if (!this.ValidatePullRequest())
                {
                    return string.Empty;
                }

                return this.pullRequest.LastMergeTargetCommit.CommitId;
            }
        }

        /// <summary>
        /// Votes for the pullrequest.
        /// </summary>
        /// <param name="vote">The vote for the pull request.</param>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void Vote(TfsPullRequestVote vote)
        {
            if (!this.ValidatePullRequest())
            {
                return;
            }

            using (var gitClient = this.CreateGitClient(out var authorizedIdenity))
            {
                var request =
                    gitClient.CreatePullRequestReviewerAsync(
                        new IdentityRefWithVote() { Vote = (short)vote },
                        this.pullRequest.Repository.Id,
                        this.pullRequest.PullRequestId,
                        authorizedIdenity.Id.ToString(),
                        CancellationToken.None);

                var createdReviewer = request.Result;
                var createdVote = (TfsPullRequestVote)createdReviewer.Vote;
                this.log.Verbose("Voted for pull request with '{0}'.", createdVote.ToString());
            }
        }

        /// <summary>
        /// Sets a status on the pull request.
        /// </summary>
        /// <param name="status">The description of the status which should be set.</param>
        /// <exception cref="TfsPullRequestNotFoundException">If pull request could not be found and
        /// <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public void SetStatus(TfsPullRequestStatus status)
        {
            status.NotNull(nameof(status));

            if (!this.ValidatePullRequest())
            {
                return;
            }

            using (var gitClient = this.CreateGitClient())
            {
                var request =
                    gitClient.CreatePullRequestStatusAsync(
                        new GitPullRequestStatus
                        {
                            State = status.State.ToGitStatusState(),
                            Description = status.Description,
                            TargetUrl = status.TargetUrl?.ToString(),
                            Context = new GitStatusContext()
                            {
                                Name = status.Name,
                                Genre = status.Genre
                            }
                        },
                        this.pullRequest.Repository.Id,
                        this.pullRequest.PullRequestId);

                var postedStatus = request.Result;
                this.log.Verbose("Posted status '{0}'.", postedStatus.Context.Name);
            }
        }

        /// <summary>
        /// Validates if a pull request could be found.
        /// Depending on <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// the pull request instance can be null for subsequent calls.
        /// </summary>
        /// <returns>True if a valid pull request instance exists.</returns>
        /// <exception cref="TfsPullRequestNotFoundException">If <see cref="TfsPullRequestSettings.ThrowExceptionIfPullRequestCouldNotBeFound"/>
        /// is set to <c>true</c> and no pull request could be found.</exception>
        private bool ValidatePullRequest()
        {
            if (this.HasPullRequestLoaded)
            {
                return true;
            }

            if (this.settings.ThrowExceptionIfPullRequestCouldNotBeFound)
            {
                throw new TfsPullRequestNotFoundException("Pull request not found");
            }

            this.log.Verbose("Skipping, since no pull request instance could be found.");
            return false;
        }

        /// <summary>
        /// Creates a client object for communicating with Team Foundation Server or Azure DevOps.
        /// </summary>
        /// <param name="authorizedIdentity">Returns identity which is authorized.</param>
        /// <returns>Client object for communicating with Team Foundation Server or Azure DevOps</returns>
        private GitHttpClient CreateGitClient(out Identity authorizedIdentity)
        {
            var connection =
                new VssConnection(
                    this.repositoryDescription.CollectionUrl,
                    this.settings.Credentials.ToVssCredentials());

            authorizedIdentity = connection.AuthorizedIdentity;

            var gitClient = connection.GetClient<GitHttpClient>();
            if (gitClient == null)
            {
                throw new TfsException("Could not retrieve the GitHttpClient object");
            }

            return gitClient;
        }

        /// <summary>
        /// Creates a client object for communicating with Team Foundation Server or Azure DevOps.
        /// </summary>
        /// <returns>Client object for communicating with Team Foundation Server or Azure DevOps</returns>
        private GitHttpClient CreateGitClient()
        {
            return this.CreateGitClient(out var identity);
        }
    }
}
