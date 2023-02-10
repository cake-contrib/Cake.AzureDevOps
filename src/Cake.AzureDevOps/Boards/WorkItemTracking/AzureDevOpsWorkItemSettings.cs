namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling work items.
    /// </summary>
    public class AzureDevOpsWorkItemSettings : BaseAzureDevOpsProjectSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="workItemId">ID of the work item.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemSettings(Uri collectionUrl, Guid projectGuid, int workItemId, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectGuid, credentials)
        {
            workItemId.NotNegativeOrZero(nameof(workItemId));

            this.WorkItemId = workItemId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="workItemId">ID of the work item.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemSettings(Uri collectionUrl, string projectName, int workItemId, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectName, credentials)
        {
            workItemId.NotNegativeOrZero(nameof(workItemId));

            this.WorkItemId = workItemId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsWorkItemSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsWorkItemSettings(AzureDevOpsWorkItemSettings settings)
            : base(settings)
        {
            this.WorkItemId = settings.WorkItemId;
            this.ThrowExceptionIfWorkItemCouldNotBeFound = settings.ThrowExceptionIfWorkItemCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsWorkItemSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        /// <param name="workItemId">ID of the work item.</param>
        public AzureDevOpsWorkItemSettings(AzureDevOpsWorkItemSettings settings, int workItemId)
            : base(settings)
        {
            workItemId.NotNegativeOrZero(nameof(workItemId));

            this.WorkItemId = workItemId;
            this.ThrowExceptionIfWorkItemCouldNotBeFound = settings.ThrowExceptionIfWorkItemCouldNotBeFound;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="workItemId">ID of the work item.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemSettings(int workItemId, IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            workItemId.NotNegativeOrZero(nameof(workItemId));

            this.WorkItemId = workItemId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemSettings"/> class
        /// based on the instance of a <see cref="BaseAzureDevOpsProjectSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsWorkItemSettings(BaseAzureDevOpsProjectSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Gets the ID of the work item.
        /// </summary>
        public int WorkItemId { get; }

        /// <summary>
        /// Gets or sets a value indicating whether an exception should be thrown if
        /// work item for <see cref="WorkItemId"/> could not be found.
        /// </summary>
        public bool ThrowExceptionIfWorkItemCouldNotBeFound { get; set; } = true;

        /// <summary>
        /// Constructs the settings object for a specific work item using the access token provided by Azure Pipelines.
        /// </summary>
        /// <param name="workItemId">ID of the work item.</param>
        /// <returns>The instance of <see cref="AzureDevOpsWorkItemSettings"/> class.</returns>
        public static AzureDevOpsWorkItemSettings UsingAzurePipelinesOAuthToken(int workItemId)
        {
            workItemId.NotNegativeOrZero(nameof(workItemId));

            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsWorkItemSettings(workItemId, new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
