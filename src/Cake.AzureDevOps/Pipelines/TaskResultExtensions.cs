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
            return result switch
            {
                TaskResult.Succeeded => AzureDevOpsTaskResult.Succeeded,
                TaskResult.SucceededWithIssues => AzureDevOpsTaskResult.SucceededWithIssues,
                TaskResult.Failed => AzureDevOpsTaskResult.Failed,
                TaskResult.Canceled => AzureDevOpsTaskResult.Canceled,
                TaskResult.Skipped => AzureDevOpsTaskResult.Skipped,
                TaskResult.Abandoned => AzureDevOpsTaskResult.Abandoned,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
