namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="AzureDevOpsBuildResult"/> class.
    /// </summary>
    internal static class AzureDevOpsBuildResultExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsBuildResult"/> to an <see cref="BuildResult"/>.
        /// </summary>
        /// <param name="result">Build result to convert.</param>
        /// <returns>Converted build result.</returns>
        public static BuildResult ToBuildResult(this AzureDevOpsBuildResult result)
        {
            switch (result)
            {
                case AzureDevOpsBuildResult.None:
                    return BuildResult.None;
                case AzureDevOpsBuildResult.Succeeded:
                    return BuildResult.Succeeded;
                case AzureDevOpsBuildResult.PartiallySucceeded:
                    return BuildResult.PartiallySucceeded;
                case AzureDevOpsBuildResult.Failed:
                    return BuildResult.Failed;
                case AzureDevOpsBuildResult.Canceled:
                    return BuildResult.Canceled;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
