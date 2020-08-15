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
        /// Gets or sets the genre of the status.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the state of the pull request.
        /// </summary>
        public AzureDevOpsPullRequestStatusState State { get; set; }

        /// <summary>
        /// Gets or sets the description of the status.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the URL of the status.
        /// </summary>
        public Uri TargetUrl { get; set; }
    }
}
