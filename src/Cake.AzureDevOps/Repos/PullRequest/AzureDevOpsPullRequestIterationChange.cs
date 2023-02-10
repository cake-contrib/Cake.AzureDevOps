namespace Cake.AzureDevOps.Repos.PullRequest
{
    using Cake.Core.IO;

    /// <summary>
    /// Class representing an iteration change of the pull request.
    /// </summary>
    public class AzureDevOpsPullRequestIterationChange
    {
        /// <summary>
        /// Gets the file path the iteration change is associated with.
        /// </summary>
        public FilePath ItemPath { get; init; }

        /// <summary>
        /// Gets the id of the iteration change.
        /// </summary>
        public int ChangeId { get; init; }

        /// <summary>
        /// Gets the tracking id of the iteration change.
        /// </summary>
        public int ChangeTrackingId { get; init; }
    }
}
