namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="BuildStatus"/> class.
    /// </summary>
    internal static class BuildStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="BuildStatus"/> to an <see cref="AzureDevOpsBuildStatus"/>.
        /// </summary>
        /// <param name="status">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static AzureDevOpsBuildStatus ToAzureDevOpsBuildStatus(this BuildStatus status)
        {
            switch (status)
            {
                case BuildStatus.None:
                    return AzureDevOpsBuildStatus.None;
                case BuildStatus.InProgress:
                    return AzureDevOpsBuildStatus.InProgress;
                case BuildStatus.Completed:
                    return AzureDevOpsBuildStatus.Completed;
                case BuildStatus.Cancelling:
                    return AzureDevOpsBuildStatus.Cancelling;
                case BuildStatus.Postponed:
                    return AzureDevOpsBuildStatus.Postponed;
                case BuildStatus.NotStarted:
                    return AzureDevOpsBuildStatus.NotStarted;
                case BuildStatus.All:
                    return AzureDevOpsBuildStatus.All;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
