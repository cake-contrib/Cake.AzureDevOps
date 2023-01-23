namespace Cake.AzureDevOps.Pipelines
{
    using System.ComponentModel;
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="AzureDevOpsBuildStatus"/> class.
    /// </summary>
    internal static class AzureDevOpsBuildStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsBuildStatus"/> to an <see cref="BuildStatus"/>.
        /// </summary>
        /// <param name="status">State to convert.</param>
        /// <returns>Converted state.</returns>
        public static BuildStatus ToBuildStatus(this AzureDevOpsBuildStatus status)
        {
            return status switch
            {
                AzureDevOpsBuildStatus.None => BuildStatus.None,
                AzureDevOpsBuildStatus.InProgress => BuildStatus.InProgress,
                AzureDevOpsBuildStatus.Completed => BuildStatus.Completed,
                AzureDevOpsBuildStatus.Cancelling => BuildStatus.Cancelling,
                AzureDevOpsBuildStatus.Postponed => BuildStatus.Postponed,
                AzureDevOpsBuildStatus.NotStarted => BuildStatus.NotStarted,
                AzureDevOpsBuildStatus.All => BuildStatus.All,
                _ => throw new InvalidEnumArgumentException(nameof(status), (int)status, typeof(AzureDevOpsBuildStatus)),
            };
        }
    }
}
