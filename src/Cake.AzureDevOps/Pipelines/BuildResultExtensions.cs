namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="BuildResult"/> class.
    /// </summary>
    internal static class BuildResultExtensions
    {
        /// <summary>
        /// Converts a <see cref="BuildResult"/> to an <see cref="AzureDevOpsBuildResult"/>.
        /// </summary>
        /// <param name="result">Build result to convert.</param>
        /// <returns>Converted build result.</returns>
        public static AzureDevOpsBuildResult ToAzureDevOpsBuildResult(this BuildResult result)
        {
            switch (result)
            {
                case BuildResult.None:
                    return AzureDevOpsBuildResult.None;
                case BuildResult.Succeeded:
                    return AzureDevOpsBuildResult.Succeeded;
                case BuildResult.PartiallySucceeded:
                    return AzureDevOpsBuildResult.PartiallySucceeded;
                case BuildResult.Failed:
                    return AzureDevOpsBuildResult.Failed;
                case BuildResult.Canceled:
                    return AzureDevOpsBuildResult.Canceled;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
