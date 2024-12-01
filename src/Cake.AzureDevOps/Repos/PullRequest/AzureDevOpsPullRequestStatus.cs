// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;

    /// <summary>
    /// Description of a pull request status.
    /// </summary>
    public class AzureDevOpsPullRequestStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsPullRequestStatus"/> class.
        /// </summary>
        /// <param name="name">Name of the status.</param>
        public AzureDevOpsPullRequestStatus(string name)
        {
            name.NotNullOrWhiteSpace(nameof(name));

            this.Name = name;
        }

        /// <summary>
        /// Gets the name of the status.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the genre of the status.
        /// </summary>
        public string Genre { get; init; }

        /// <summary>
        /// Gets the state of the pull request.
        /// </summary>
        public AzureDevOpsPullRequestStatusState State { get; init; }

        /// <summary>
        /// Gets the description of the status.
        /// </summary>
        public string Description { get; init; }

        /// <summary>
        /// Gets the URL of the status.
        /// </summary>
        public Uri TargetUrl { get; init; }
    }
}