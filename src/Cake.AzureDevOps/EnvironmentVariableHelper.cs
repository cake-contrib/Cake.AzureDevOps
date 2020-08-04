namespace Cake.AzureDevOps
{
    using System;

    /// <summary>
    /// Class for reading environment variables.
    /// </summary>
    internal static class EnvironmentVariableHelper
    {
        /// <summary>
        /// Gets the value for the environment variable 'SYSTEM_ACCESSTOKEN'.
        /// </summary>
        /// <returns>The value for the environment variable 'SYSTEM_ACCESSTOKEN'.</returns>
        /// <exception cref="InvalidOperationException">If the environment variable was not found or the value is whitespace.</exception>
        public static string GetSystemAccessToken()
        {
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines build and that the 'Allow Scripts to access OAuth token' option is enabled.");
            }

            return accessToken;
        }

        /// <summary>
        /// Gets the value for the environment variable 'SYSTEM_TEAMFOUNDATIONCOLLECTIONURI'.
        /// </summary>
        /// <returns>The value for the environment variable 'SYSTEM_TEAMFOUNDATIONCOLLECTIONURI'.</returns>
        /// <exception cref="InvalidOperationException">If the environment variable was not found or the value is whitespace.</exception>
        public static Uri GetSystemTeamFoundationCollectionUri()
        {
            var collectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(collectionUrl))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_TEAMFOUNDATIONCOLLECTIONURI environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            return new Uri(collectionUrl);
        }

        /// <summary>
        /// Gets the value for the environment variable 'SYSTEM_TEAMPROJECT'.
        /// </summary>
        /// <returns>The value for the environment variable 'SYSTEM_TEAMPROJECT'.</returns>
        /// <exception cref="InvalidOperationException">If the environment variable was not found or the value is whitespace.</exception>
        public static string GetSystemTeamProject()
        {
            var projectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_TEAMPROJECT environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            return projectName;
        }

        /// <summary>
        /// Gets the value for the environment variable 'BUILD_BUILDID'.
        /// </summary>
        /// <returns>The value for the environment variable 'BUILD_BUILDID'.</returns>
        /// <exception cref="InvalidOperationException">If the environment variable was not found, the value is whitespace or less than 1.</exception>
        public static int GetBuildId()
        {
            var buildId = Environment.GetEnvironmentVariable("BUILD_BUILDID", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(buildId))
            {
                throw new InvalidOperationException(
                    "Failed to read the BUILD_BUILDID environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            if (!int.TryParse(buildId, out int buildIdValue))
            {
                throw new InvalidOperationException("BUILD_BUILDID environment variable should contain integer value");
            }

            if (buildIdValue <= 0)
            {
                throw new InvalidOperationException("BUILD_BUILDID environment variable should contain integer value and it should be greater than zero");
            }

            return buildIdValue;
        }
    }
}
