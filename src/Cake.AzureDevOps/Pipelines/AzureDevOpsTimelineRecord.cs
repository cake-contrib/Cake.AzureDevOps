namespace Cake.AzureDevOps.Pipelines
{
    using System;

    /// <summary>
    /// Represents of an entry in a build's timeline.
    /// </summary>
    public class AzureDevOpsTimelineRecord
    {
        /// <summary>
        /// Gets the attempt of the build.
        /// </summary>
        public int Attempt { get; internal set; }

        /// <summary>
        /// Gets the number of warnings in the build.
        /// </summary>
        public int? WarningCount { get; internal set; }

        /// <summary>
        /// Gets the number of errors in the build.
        /// </summary>
        public int? ErrorCount { get; internal set; }

        /// <summary>
        /// Gets an ordinal value relative to other records.
        /// </summary>
        public int? Order { get; internal set; }

        /// <summary>
        /// Gets the name of the agent running the operation.
        /// </summary>
        public string WorkerName { get; internal set; }

        /// <summary>
        /// Gets the time the record was last modified.
        /// </summary>
        public DateTime LastModified { get; internal set; }

        /// <summary>
        /// Gets the change ID.
        /// </summary>
        /// <remarks>
        /// This is a monotonically-increasing number used to ensure consistency in the UI.
        /// </remarks>
        public int ChangeId { get; internal set; }

        /// <summary>
        /// Gets the result code.
        /// </summary>
        public string ResultCode { get; internal set; }

        /// <summary>
        /// Gets the result.
        /// </summary>
        public AzureDevOpsTaskResult? Result { get; internal set; }

        /// <summary>
        /// Gets the state of the record.
        /// </summary>
        public AzureDevOpsTimelineRecordState? State { get; internal set; }

        /// <summary>
        /// Gets the current completion percentage.
        /// </summary>
        public int? PercentComplete { get; internal set; }

        /// <summary>
        /// Gets a string that indicates the current operation.
        /// </summary>
        public string CurrentOperation { get; internal set; }

        /// <summary>
        /// Gets the finish time.
        /// </summary>
        public DateTime? FinishTime { get; internal set; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        public DateTime? StartTime { get; internal set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the type of the record.
        /// </summary>
        public string RecordType { get; internal set; }

        /// <summary>
        /// Gets the ID of the record's parent.
        /// </summary>
        public Guid? ParentId { get; internal set; }

        /// <summary>
        /// Gets the ID of the record.
        /// </summary>
        public Guid Id { get; internal set; }
    }
}
