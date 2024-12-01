namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Results of an Azure Pipelines task.
    /// </summary>
    public enum AzureDevOpsTaskResult : byte
    {
        /// <summary>
        /// Task succeeded.
        /// </summary>
        Succeeded = 0,

        /// <summary>
        /// Task succeeded with issues.
        /// </summary>
        SucceededWithIssues = 1,

        /// <summary>
        /// Task failed.
        /// </summary>
        Failed = 2,

        /// <summary>
        /// Task has been canceled.
        /// </summary>
        Canceled = 3,

        /// <summary>
        /// Task has been skipped.
        /// </summary>
        Skipped = 4,

        /// <summary>
        /// Task was abandoned.
        /// </summary>
        Abandoned = 5,
    }
}