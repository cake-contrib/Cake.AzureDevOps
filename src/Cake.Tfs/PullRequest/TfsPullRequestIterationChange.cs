namespace Cake.Tfs.PullRequest
{
    /// <summary>
    /// Class representing an iteration change of the pull request.
    /// </summary>
    public class TfsPullRequestIterationChange
    {
        /// <summary>
        /// Gets or sets the file path the iteration change is associated with.
        /// </summary>
        public string ItemPath { get; set; }

        /// <summary>
        /// Gets or sets the id of the iteration change.
        /// </summary>
        public int ChangeId { get; set; }

        /// <summary>
        /// Gets or sets the tracking id of the iteration change.
        /// </summary>
        public int ChangeTrackingId { get; set; }
    }
}
