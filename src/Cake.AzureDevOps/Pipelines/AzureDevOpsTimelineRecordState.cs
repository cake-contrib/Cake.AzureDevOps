namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// State of an Azure Pipelines timeline record.
    /// </summary>
    public enum AzureDevOpsTimelineRecordState : byte
    {
        /// <summary>
        /// Task execution is pending.
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Task execution is in progress.
        /// </summary>
        InProgress = 1,

        /// <summary>
        /// Task has been completed.
        /// </summary>
        Completed = 2,
    }
}