namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling builds.
    /// </summary>
    public class AzureDevOpsBuildsSettings : BaseAzureDevOpsProjectSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(Uri collectionUrl, Guid projectGuid, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectGuid, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(Uri collectionUrl, string projectName, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectName, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsBuildsSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsBuildsSettings(AzureDevOpsBuildsSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildsSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsBuildsSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }

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
