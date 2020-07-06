namespace Cake.AzureDevOps.Pipelines
{
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
            switch (status)
            {
                case AzureDevOpsBuildStatus.None:
                    return BuildStatus.None;
                case AzureDevOpsBuildStatus.InProgress:
                    return BuildStatus.InProgress;
                case AzureDevOpsBuildStatus.Completed:
                    return BuildStatus.Completed;
                case AzureDevOpsBuildStatus.Cancelling:
                    return BuildStatus.Cancelling;
                case AzureDevOpsBuildStatus.Postponed:
                    return BuildStatus.Postponed;
                case AzureDevOpsBuildStatus.NotStarted:
                    return BuildStatus.NotStarted;
                case AzureDevOpsBuildStatus.All:
                    return BuildStatus.All;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
