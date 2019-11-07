namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="TaskResult"/> class.
    /// </summary>
    internal static class TaskResultExtensions
    {
        /// <summary>
        /// Converts a <see cref="TaskResult"/> to an <see cref="AzureDevOpsTaskResult"/>.
        /// </summary>
        /// <param name="result">Result to convert.</param>
        /// <returns>Converted result.</returns>
        public static AzureDevOpsTaskResult ToAzureDevOpsTaskResult(this TaskResult result)
        {
            switch (result)
            {
                case TaskResult.Succeeded:
                    return AzureDevOpsTaskResult.Succeeded;
                case TaskResult.SucceededWithIssues:
                    return AzureDevOpsTaskResult.SucceededWithIssues;
                case TaskResult.Failed:
                    return AzureDevOpsTaskResult.Failed;
                case TaskResult.Canceled:
                    return AzureDevOpsTaskResult.Canceled;
                case TaskResult.Skipped:
                    return AzureDevOpsTaskResult.Skipped;
                case TaskResult.Abandoned:
                    return AzureDevOpsTaskResult.Abandoned;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
