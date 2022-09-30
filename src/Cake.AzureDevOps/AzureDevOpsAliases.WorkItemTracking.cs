namespace Cake.AzureDevOps
{
    using Cake.AzureDevOps.Boards.WorkItemTracking;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to Azure DevOps work item tracking.
    /// </content>
    public static partial class AzureDevOpsAliases
    {
        /// <summary>
        /// Gets an Azure DevOps work item using the specified settings.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the work item.</param>
        /// <example>
        /// <para>Get a work item from Azure DevOps Server:</para>
        /// <code>
        /// <![CDATA[
        /// var workItemSettings =
        ///     new AzureDevOpsWorkItemSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         "MyProject",
        ///         42,
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var workItem =
        ///     AzureDevOpsWorkItem(
        ///         workItemSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the work item.
        /// Returns <c>null</c> if work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Boards")]
        [CakeNamespaceImport("Cake.AzureDevOps.Boards.WorkItemTracking")]
        public static AzureDevOpsWorkItem AzureDevOpsWorkItem(
            this ICakeContext context,
            AzureDevOpsWorkItemSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            var workItem = new AzureDevOpsWorkItem(context.Log, settings, new WorkItemTrackingClientFactory());

            if (workItem.HasWorkItemLoaded)
            {
                return workItem;
            }

            return null;
        }

        /// <summary>
        /// Gets the description of a specific work item the access token provided by Azure Pipelines.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="workItemId">ID of the work item.</param>
        /// <example>
        /// <para>Get an Azure DevOps work item:</para>
        /// <code>
        /// <![CDATA[
        /// var workItem =
        ///     AzureDevOpsBuildUsingAzurePipelinesOAuthToken(42);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the work item.
        /// Returns <c>null</c> if work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Boards")]
        [CakeNamespaceImport("Cake.AzureDevOps.Boards.WorkItemTracking")]
        public static AzureDevOpsWorkItem AzureDevOpsWorkItemUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            int workItemId)
        {
            context.NotNull(nameof(context));
            workItemId.NotNegativeOrZero(nameof(workItemId));

            var settings = AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(workItemId);

            return AzureDevOpsWorkItem(context, settings);
        }

        /// <summary>
        /// Gets the description of a specific work item the access token provided by Azure Pipelines.
        /// Make sure the build has the 'Allow Scripts to access OAuth token' option enabled.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="workItemId">ID of the work item.</param>
        /// <param name="throwExceptionIfWorkItemCouldNotBeFound">Value indicating whether an exception
        /// should be thrown if work item could not be found.</param>
        /// <example>
        /// <para>Get an Azure DevOps work item. Don't throw exception in case the work item is not found:</para>
        /// <code>
        /// <![CDATA[
        /// var workItem =
        ///     AzureDevOpsWorkItemUsingAzurePipelinesOAuthToken(42, false);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>Description of the work item.
        /// Returns <c>null</c> if work item could not be found and
        /// <paramref name="throwExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.</returns>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <paramref name="throwExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Boards")]
        [CakeNamespaceImport("Cake.AzureDevOps.Boards.WorkItemTracking")]
        public static AzureDevOpsWorkItem AzureDevOpsWorkItemUsingAzurePipelinesOAuthToken(
            this ICakeContext context,
            int workItemId,
            bool throwExceptionIfWorkItemCouldNotBeFound)
        {
            context.NotNull(nameof(context));
            workItemId.NotNegativeOrZero(nameof(workItemId));

            var settings = AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(workItemId);
            settings.ThrowExceptionIfWorkItemCouldNotBeFound = throwExceptionIfWorkItemCouldNotBeFound;

            return AzureDevOpsWorkItem(context, settings);
        }
    }
}