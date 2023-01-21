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
            return result switch
            {
                BuildResult.None => AzureDevOpsBuildResult.None,
                BuildResult.Succeeded => AzureDevOpsBuildResult.Succeeded,
                BuildResult.PartiallySucceeded => AzureDevOpsBuildResult.PartiallySucceeded,
                BuildResult.Failed => AzureDevOpsBuildResult.Failed,
                BuildResult.Canceled => AzureDevOpsBuildResult.Canceled,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
