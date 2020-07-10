namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling builds.
    /// </summary>
    public class AzureDevOpsBuildsSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(Uri collectionUrl, Guid projectGuid, IAzureDevOpsCredentials credentials)
        {
            collectionUrl.NotNull(nameof(collectionUrl));
            credentials.NotNull(nameof(credentials));

            if (projectGuid == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(projectGuid));
            }

            this.Credentials = credentials;
            this.ProjectGuid = projectGuid;
            this.CollectionUrl = collectionUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(Uri collectionUrl, string projectName, IAzureDevOpsCredentials credentials)
        {
            collectionUrl.NotNull(nameof(collectionUrl));
            projectName.NotNullOrWhiteSpace(nameof(projectName));
            credentials.NotNull(nameof(credentials));

            this.CollectionUrl = collectionUrl;
            this.ProjectName = projectName;
            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsBuildsSettings(AzureDevOpsBuildsSettings settings)
        {
            settings.NotNull(nameof(settings));

            this.CollectionUrl = settings.CollectionUrl;
            this.ProjectGuid = settings.ProjectGuid;
            this.ProjectName = settings.ProjectName;
            this.Credentials = settings.Credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(IAzureDevOpsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            this.Credentials = credentials;
            this.CollectionUrl = EnvironmentVariableHelper.GetSystemTeamFoundationCollectionUri();
            this.ProjectName = EnvironmentVariableHelper.GetSystemTeamProject();
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
        /// Gets or sets the name of the build definition.
        /// Can be <c>null</c> or <see cref="string.Empty"/>.
        /// </summary>
        public string BuildDefinitionName { get; set; }

        /// <summary>
        /// Gets or sets the name of the branch.
        /// Can be <c>null</c> or <see cref="string.Empty"/>.
        /// </summary>
        public string BranchName { get; set; }

        /// <summary>
        /// Gets or sets the build status.
        /// </summary>
        public AzureDevOpsBuildStatus? BuildStatus { get; set; }

        /// <summary>
        /// Gets or sets the build result.
        /// </summary>
        public AzureDevOpsBuildResult? BuildResult { get; set; }

        /// <summary>
        /// Gets or sets the build query order.
        /// </summary>
        public AzureDevOpsBuildQueryOrder? BuildQueryOrder { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of builds per definition.
        /// </summary>
        public int? MaxBuildsPerDefinition { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of builds.
        /// </summary>
        public int? Top { get; set; }

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsBuildsSettings"/> class.</returns>
        public static AzureDevOpsBuildsSettings UsingAzurePipelinesOAuthToken()
        {
            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsBuildsSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
