namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling for workitem tracking.
    /// </summary>
    public class AzureDevOpsWorkItemTrackingSettings : BaseAzureDevOpsProjectSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTrackingSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectGuid">ID of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemTrackingSettings(Uri collectionUrl, Guid projectGuid, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectGuid, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTrackingSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemTrackingSettings(Uri collectionUrl, string projectName, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectName, credentials)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTrackingSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsWorkItemTrackingSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsWorkItemTrackingSettings(AzureDevOpsWorkItemTrackingSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTrackingSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsWorkItemTrackingSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }

        /// <summary>
        /// Constructs the settings object for a specific work item using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsWorkItemTrackingSettings"/> class.</returns>
        public static AzureDevOpsWorkItemTrackingSettings UsingAzurePipelinesOAuthToken()
        {
            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsWorkItemTrackingSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
