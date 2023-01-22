namespace Cake.AzureDevOps.Pipelines
{
    using System.ComponentModel;
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
            return result switch
            {
                AzureDevOpsBuildResult.None => BuildResult.None,
                AzureDevOpsBuildResult.Succeeded => BuildResult.Succeeded,
                AzureDevOpsBuildResult.PartiallySucceeded => BuildResult.PartiallySucceeded,
                AzureDevOpsBuildResult.Failed => BuildResult.Failed,
                AzureDevOpsBuildResult.Canceled => BuildResult.Canceled,
                _ => throw new InvalidEnumArgumentException(nameof(result), (int)result, typeof(AzureDevOpsBuildResult)),
            };
        }
    }
}
