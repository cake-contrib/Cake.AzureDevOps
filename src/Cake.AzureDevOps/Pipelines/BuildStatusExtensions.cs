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
            return status switch
            {
                BuildStatus.None => AzureDevOpsBuildStatus.None,
                BuildStatus.InProgress => AzureDevOpsBuildStatus.InProgress,
                BuildStatus.Completed => AzureDevOpsBuildStatus.Completed,
                BuildStatus.Cancelling => AzureDevOpsBuildStatus.Cancelling,
                BuildStatus.Postponed => AzureDevOpsBuildStatus.Postponed,
                BuildStatus.NotStarted => AzureDevOpsBuildStatus.NotStarted,
                BuildStatus.All => AzureDevOpsBuildStatus.All,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
