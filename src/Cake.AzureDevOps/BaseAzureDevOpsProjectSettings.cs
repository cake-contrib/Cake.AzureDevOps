namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Base settings for aliases scoped to project level.
    /// </summary>
    public abstract class BaseAzureDevOpsProjectSettings : BaseAzureDevOpsCollectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsProjectSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsProjectSettings(Uri collectionUrl, Guid projectGuid, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, credentials)
        {
            if (projectGuid == Guid.Empty)
            {
                throw new ArgumentOutOfRangeException(nameof(projectGuid));
            }

            this.ProjectGuid = projectGuid;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsProjectSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsProjectSettings(Uri collectionUrl, string projectName, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, credentials)
        {
            projectName.NotNullOrWhiteSpace(nameof(projectName));

            this.ProjectName = projectName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsProjectSettings"/> class
        /// based on the instance of a <see cref="BaseAzureDevOpsProjectSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        protected BaseAzureDevOpsProjectSettings(BaseAzureDevOpsProjectSettings settings)
            : base(settings)
        {
            this.ProjectGuid = settings.ProjectGuid;
            this.ProjectName = settings.ProjectName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsProjectSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsProjectSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            this.ProjectName = EnvironmentVariableHelper.GetSystemTeamProject();
        }

        /// <summary>
        /// Gets the Guid of the project.
        /// Can be <see cref="Guid.Empty"/> if <see cref="ProjectName"/> is set.
        /// </summary>
        public Guid ProjectGuid { get; }

        /// <summary>
        /// Gets the name of the project.
        /// Can be <c>null</c> if <see cref="ProjectGuid"/> is set.
        /// </summary>
        public string ProjectName { get; }
    }
}
