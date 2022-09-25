namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Class to work with work item tracking.
    /// </summary>
    public sealed class AzureDevOpsWorkItemTracking
    {
        private readonly ICakeLog log;
        private readonly AzureDevOpsWorkItemTrackingSettings settings;
        private readonly IWorkItemTrackingClientFactory workItemTrackingClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTracking"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        public AzureDevOpsWorkItemTracking(ICakeLog log, AzureDevOpsWorkItemTrackingSettings settings)
            : this(log, settings, new WorkItemTrackingClientFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemTracking"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <param name="workItemTrackingClientFactory">A factory to communicate with work item tracking client.</param>
        internal AzureDevOpsWorkItemTracking(
            ICakeLog log,
            AzureDevOpsWorkItemTrackingSettings settings,
            IWorkItemTrackingClientFactory workItemTrackingClientFactory)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            workItemTrackingClientFactory.NotNull(nameof(workItemTrackingClientFactory));

            this.log = log;
            this.workItemTrackingClientFactory = workItemTrackingClientFactory;
            this.settings = settings;
        }

        /// <summary>
        /// Gets the specified work items (Maximum 200).
        /// </summary>
        /// <param name="workItemIds">List of work item ids.</param>
        /// <returns>The work items defined by the ids.</returns>
        public IEnumerable<AzureDevOpsWorkItem> GetWorkItems(IEnumerable<int> workItemIds)
        {
            using (var workItemTrackingClient = this.workItemTrackingClientFactory.CreateWorkItemTrackingClient(this.settings.CollectionUrl, this.settings.Credentials))
            {
                return
                    workItemTrackingClient
                        .GetWorkItemsAsync(workItemIds, expand: Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemExpand.Relations)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult()
                        .Select(x => new AzureDevOpsWorkItem(this.log, new AzureDevOpsWorkItemSettings(this.settings), x, this.workItemTrackingClientFactory));
            }
        }

        /// <summary>
        /// Gets the specified work item.
        /// </summary>
        /// <param name="workItemId">The ID of the work item.</param>
        /// <returns>The work item specified by the ID.</returns>
        public AzureDevOpsWorkItem GetWorkItem(int workItemId)
        {
            using (var workItemTrackingClient = this.workItemTrackingClientFactory.CreateWorkItemTrackingClient(this.settings.CollectionUrl, this.settings.Credentials))
            {
                var workItem =
                    workItemTrackingClient
                        .GetWorkItemAsync(workItemId, expand: Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models.WorkItemExpand.Relations)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                return new AzureDevOpsWorkItem(this.log, new AzureDevOpsWorkItemSettings(this.settings), workItem, this.workItemTrackingClientFactory);
            }
        }
    }
}