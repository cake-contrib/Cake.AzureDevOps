namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Possible states of a build queue.
    /// </summary>
    public enum AzureDevOpsDefinitionQueueStatus : byte
    {
        /// <summary>
        /// When enabled the definition queue allows builds to be queued by users.
        /// The system will queue scheduled, gated and continuous integration builds,
        /// and the queued builds will be started by the system.
        /// </summary>
        Enabled = 0,

        /// <summary>
        /// When paused the definition queue allows builds to be queued by users and the
        /// system will queue scheduled, gated and continuous integration builds.
        /// Builds in the queue will not be started by the system.
        /// </summary>
        Paused = 1,

        /// <summary>
        /// When disabled the definition queue will not allow builds to be queued by users
        /// and the system will not queue scheduled, gated or continuous integration builds.
        /// Builds already in the queue will not be started by the system.
        /// <summary>
        Disabled = 2,
    }
}
