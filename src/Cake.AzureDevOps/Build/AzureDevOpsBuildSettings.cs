namespace Cake.AzureDevOps.Build
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling builds.
    /// </summary>
    public class AzureDevOpsBuildSettings
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
        {
            collectionUrl.NotNull(nameof(collectionUrl));
            buildId.NotNegativeOrZero(nameof(buildId));
            credentials.NotNull(nameof(credentials));

            if (projectGuid == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(projectGuid));
            }

            this.CollectionUrl = collectionUrl;
            this.ProjectGuid = projectGuid;
            this.BuildId = buildId;
            this.Credentials = credentials;
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
        {
            collectionUrl.NotNull(nameof(collectionUrl));
            projectName.NotNullOrWhiteSpace(nameof(projectName));
            buildId.NotNegativeOrZero(nameof(buildId));
            credentials.NotNull(nameof(credentials));

            this.CollectionUrl = collectionUrl;
            this.ProjectName = projectName;
            this.BuildId = buildId;
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsBuildSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsBuildSettings(AzureDevOpsBuildSettings settings)
        {
            settings.NotNull(nameof(settings));

            this.CollectionUrl = settings.CollectionUrl;
            this.ProjectGuid = settings.ProjectGuid;
            this.ProjectName = settings.ProjectName;
            this.Credentials = settings.Credentials;
            this.BuildId = settings.BuildId;
            this.ThrowExceptionIfBuildCouldNotBeFound = settings.ThrowExceptionIfBuildCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildSettings"/> class using environment variables
        /// as set by a Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildSettings(IAzureDevOpsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            this.Credentials = credentials;

            var collectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(collectionUrl))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_TEAMFOUNDATIONCOLLECTIONURI environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            this.CollectionUrl = new Uri(collectionUrl);

            var projectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT", EnvironmentVariableTarget.Process);
            if (string.IsNullOrWhiteSpace(projectName))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_TEAMPROJECT environment variable. Make sure you are running in an Azure Pipelines build.");
            }

            this.ProjectName = projectName;

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

            this.BuildId = buildIdValue;
        }

        /// <summary>
        /// Gets the full URL of the Azure DevOps collection, eg. <code>http://myserver:8080/defaultcollection</code>.
        /// </summary>
        public Uri CollectionUrl { get; private set; }

        /// <summary>
        /// Gets the Guid of the project.
        /// Can be <see cref="Guid.Empty"/> if <see cref="ProjectName"/> is set.
        /// </summary>
        public Guid ProjectGuid { get; private set; }

        /// <summary>
        /// Gets the name of the project.
        /// Can be <c>null</c> if <see cref="ProjectGuid"/> is set.
        /// </summary>
        public string ProjectName { get; private set; }

        /// <summary>
        /// Gets the credentials used to authenticate against Azure DevOps.
        /// </summary>
        public IAzureDevOpsCredentials Credentials { get; private set; }

        /// <summary>
        /// Gets the ID of the build.
        /// </summary>
        public int BuildId { get; private set; }

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
            var accessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN", EnvironmentVariableTarget.Process);

            if (string.IsNullOrWhiteSpace(accessToken))
            {
                throw new InvalidOperationException(
                    "Failed to read the SYSTEM_ACCESSTOKEN environment variable. Make sure you are running in an Azure Pipelines build and that the 'Allow Scripts to access OAuth token' option is enabled.");
            }

            return new AzureDevOpsBuildSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
