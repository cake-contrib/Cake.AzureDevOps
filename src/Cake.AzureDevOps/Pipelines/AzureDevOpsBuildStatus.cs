namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Possible states of a build.
    /// </summary>
    public enum AzureDevOpsBuildStatus : byte
    {
        /// <summary>
        /// Status is not set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The build is currently in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// The build has been completed.
        /// </summary>
        Completed = 2,

        /// <summary>
        /// The build is cancelling.
        /// </summary>
        Cancelling = 4,

        /// <summary>
        /// The build is inactive in the queue.
        /// </summary>
        Postponed = 8,

        /// <summary>
        /// The build has not yet started.
        /// </summary>
        NotStarted = 32,

        /// <summary>
        /// All status.
        /// </summary>
        All = 47,
    }
}
