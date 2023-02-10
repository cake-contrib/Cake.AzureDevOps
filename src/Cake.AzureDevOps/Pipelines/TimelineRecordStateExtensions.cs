namespace Cake.AzureDevOps.Pipelines
{
    using System.ComponentModel;
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
            return state switch
            {
                TimelineRecordState.Pending => AzureDevOpsTimelineRecordState.Pending,
                TimelineRecordState.InProgress => AzureDevOpsTimelineRecordState.InProgress,
                TimelineRecordState.Completed => AzureDevOpsTimelineRecordState.Completed,
                _ => throw new InvalidEnumArgumentException(nameof(state), (int)state, typeof(TimelineRecordState)),
            };
        }
    }
}
