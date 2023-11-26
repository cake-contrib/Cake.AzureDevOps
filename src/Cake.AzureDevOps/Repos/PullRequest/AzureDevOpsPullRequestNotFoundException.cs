﻿namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;

    /// <summary>
    /// Represents an error if a pull request was not found.
    /// </summary>
    [Serializable]
    public class AzureDevOpsPullRequestNotFoundException : AzureDevOpsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestNotFoundException"/> class.
        /// </summary>
        public AzureDevOpsPullRequestNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestNotFoundException"/> class.
        /// </summary>
        /// <param name="repositoryId">ID of the repository where the pull request was searched.</param>
        /// <param name="pullRequestId">ID of the pull request which could not be found.</param>
        public AzureDevOpsPullRequestNotFoundException(Guid repositoryId, int pullRequestId)
            : this("Pull request with ID " + pullRequestId + " not found in repository with GUID " + repositoryId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public AzureDevOpsPullRequestNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestNotFoundException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public AzureDevOpsPullRequestNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
