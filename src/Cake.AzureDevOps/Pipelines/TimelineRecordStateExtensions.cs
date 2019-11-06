namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="TimelineRecordState"/> class.
    /// </summary>
    internal static class TimelineRecordStateExtensions
    {
        /// <summary>
        /// Converts a <see cref="TimelineRecordState"/> to an <see cref="AzureDevOpsTimelineRecordState"/>.
        /// </summary>
        /// <param name="state">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static AzureDevOpsTimelineRecordState ToAzureDevOpsTimelineRecordState(this TimelineRecordState state)
        {
            switch (state)
            {
                case TimelineRecordState.Pending:
                    return AzureDevOpsTimelineRecordState.Pending;
                case TimelineRecordState.InProgress:
                    return AzureDevOpsTimelineRecordState.InProgress;
                case TimelineRecordState.Completed:
                    return AzureDevOpsTimelineRecordState.Completed;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
