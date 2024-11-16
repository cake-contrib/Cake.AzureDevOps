namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="TimelineRecord"/> class.
    /// </summary>
    internal static class TimelineRecordExtensions
    {
        /// <summary>
        /// Converts a <see cref="TimelineRecord"/> to an <see cref="AzureDevOpsTimelineRecord"/>.
        /// </summary>
        /// <param name="timelineRecord">Timeline record to convert.</param>
        /// <returns>Converted timeline record.</returns>
        public static AzureDevOpsTimelineRecord ToAzureDevOpsTimelineRecord(this TimelineRecord timelineRecord)
        {
            timelineRecord.NotNull(nameof(timelineRecord));

            return
                new AzureDevOpsTimelineRecord
                {
                    Attempt = timelineRecord.Attempt,
                    ChangeId = timelineRecord.ChangeId,
                    CurrentOperation = timelineRecord.CurrentOperation,
                    ErrorCount = timelineRecord.ErrorCount,
                    FinishTime = timelineRecord.FinishTime,
                    Id = timelineRecord.Id,
                    LastModified = timelineRecord.LastModified,
                    Name = timelineRecord.Name,
                    Order = timelineRecord.Order,
                    ParentId = timelineRecord.ParentId,
                    PercentComplete = timelineRecord.PercentComplete,
                    RecordType = timelineRecord.RecordType,
                    Result = timelineRecord.Result?.ToAzureDevOpsTaskResult(),
                    ResultCode = timelineRecord.ResultCode,
                    StartTime = timelineRecord.StartTime,
                    State = timelineRecord.State?.ToAzureDevOpsTimelineRecordState(),
                    WarningCount = timelineRecord.WarningCount,
                    WorkerName = timelineRecord.WorkerName,
                };
        }
    }
}