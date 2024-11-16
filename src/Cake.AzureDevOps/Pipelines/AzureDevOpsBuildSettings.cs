namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling builds.
    /// </summary>
    public class AzureDevOpsBuildSettings : BaseAzureDevOpsProjectSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="buildId">ID of the build.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildSettings(Uri collectionUrl, Guid projectGuid, int buildId, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectGuid, credentials)
        {
            buildId.NotNegativeOrZero(nameof(buildId));

            this.BuildId = buildId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="buildId">ID of the build.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildSettings(Uri collectionUrl, string projectName, int buildId, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectName, credentials)
        {
            buildId.NotNegativeOrZero(nameof(buildId));

            this.BuildId = buildId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsBuildSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsBuildSettings(AzureDevOpsBuildSettings settings)
            : base(settings)
        {
            this.BuildId = settings.BuildId;
            this.ThrowExceptionIfBuildCouldNotBeFound = settings.ThrowExceptionIfBuildCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            this.BuildId = EnvironmentVariableHelper.GetBuildId();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="buildId">ID of the build.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildSettings(int buildId, IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            buildId.NotNegativeOrZero(nameof(buildId));

            this.BuildId = buildId;
        }

        /// <summary>
        /// Gets the ID of the build.
        /// </summary>
        public int BuildId { get; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if
        /// build for <see cref="BuildId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfBuildCouldNotBeFound { get; set; } = true;

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsBuildSettings"/> class.</returns>
        public static AzureDevOpsBuildSettings UsingAzurePipelinesOAuthToken()
        {
            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsBuildSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }

        /// <summary>
        /// Constructs the settings object for a specific build using the access token provided by Azure Pipelines.
        /// </summary>
        /// <param name="buildId">ID of the build.</param>
        /// <returns>The instance of <see cref="AzureDevOpsBuildSettings"/> class.</returns>
        public static AzureDevOpsBuildSettings UsingAzurePipelinesOAuthToken(int buildId)
        {
            buildId.NotNegativeOrZero(nameof(buildId));

            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsBuildSettings(buildId, new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}