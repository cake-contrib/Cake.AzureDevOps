namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System;
    using System.Collections.Generic;
    using Cake.Core.Diagnostics;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using Microsoft.VisualStudio.Services.Common;

    /// <summary>
    /// Class to work with work item tracking.
    /// </summary>
    public sealed class AzureDevOpsWorkItem
    {
        private readonly ICakeLog log;
        private readonly AzureDevOpsWorkItemSettings settings;
        private readonly IWorkItemTrackingClientFactory workItemTrackingClientFactory;
        private readonly WorkItem workItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItem"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/>
        /// is set to <c>true</c> and no work item could be found.</exception>
        public AzureDevOpsWorkItem(ICakeLog log, AzureDevOpsWorkItemSettings settings)
            : this(log, settings, new WorkItemTrackingClientFactory())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItem"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <param name="workItem">The work item.</param>
        /// <param name="workItemTrackingClientFactory">A factory to communicate with work item tracking client.</param>
        internal AzureDevOpsWorkItem(ICakeLog log, AzureDevOpsWorkItemSettings settings, WorkItem workItem, IWorkItemTrackingClientFactory workItemTrackingClientFactory)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            workItem.NotNull(nameof(workItem));

            this.log = log;
            this.workItem = workItem;
            this.workItemTrackingClientFactory = workItemTrackingClientFactory;
            this.settings = settings;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItem"/> class.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <param name="workItemTrackingClientFactory">A factory to communicate with work item tracking client.</param>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/>
        /// is set to <c>true</c> and no work item could be found.</exception>
        internal AzureDevOpsWorkItem(
            ICakeLog log,
            AzureDevOpsWorkItemSettings settings,
            IWorkItemTrackingClientFactory workItemTrackingClientFactory)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            workItemTrackingClientFactory.NotNull(nameof(workItemTrackingClientFactory));

            this.log = log;
            this.workItemTrackingClientFactory = workItemTrackingClientFactory;
            this.settings = settings;

            using (var workItemTrackingClient = this.workItemTrackingClientFactory.CreateWorkItemTrackingClient(settings.CollectionUrl, settings.Credentials, out var authorizedIdentity))
            {
                this.log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdentity.Id,
                     authorizedIdentity.DisplayName);

                try
                {
                    if (settings.ProjectGuid != Guid.Empty)
                    {
                        this.log.Verbose("Read work item with ID {0} from project with ID {1}", settings.WorkItemId, settings.ProjectGuid);
                        this.workItem =
                            workItemTrackingClient
                                .GetWorkItemAsync(
                                    settings.ProjectGuid,
                                    settings.WorkItemId,
                                    expand: WorkItemExpand.Relations)
                                .ConfigureAwait(false)
                                .GetAwaiter()
                                .GetResult();
                    }
                    else if (!string.IsNullOrWhiteSpace(settings.ProjectName))
                    {
                        this.log.Verbose("Read work item with ID {0} from project with name {1}", settings.WorkItemId, settings.ProjectName);
                        this.workItem =
                            workItemTrackingClient
                                .GetWorkItemAsync(
                                    settings.ProjectName,
                                    settings.WorkItemId,
                                    expand: WorkItemExpand.Relations)
                                .ConfigureAwait(false)
                                .GetAwaiter()
                                .GetResult();
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException(
                            nameof(settings),
                            "Either ProjectGuid or ProjectName needs to be set");
                    }
                }
                catch (VssServiceException ex)
                {
                    if (settings.ThrowExceptionIfWorkItemCouldNotBeFound)
                    {
                        throw new AzureDevOpsWorkItemNotFoundException("Work item not found", ex);
                    }

                    this.log.Warning("Could not find work item");
                    return;
                }
            }

            if (this.workItem != null)
            {
                this.log.Verbose(
                    "Work item information:\n  Id: {0}\n  Url: {1}",
                    this.workItem.Id,
                    this.workItem.Url);
            }
        }

        /// <summary>
        /// Gets a value indicating whether a work item has been successfully loaded.
        /// </summary>
        public bool HasWorkItemLoaded => this.workItem != null;

        /// <summary>
        /// Gets the URL for accessing the web portal of the Azure DevOps collection.
        /// </summary>
        public Uri CollectionUrl => this.settings.CollectionUrl;

        /// <summary>
        /// Gets the ID of the work item.
        /// Returns 0 if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int WorkItemId => this.ValidateWorkItem() ? this.workItem.Id ?? 0 : 0;

        /// <summary>
        /// Gets the title of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Title => this.ValidateWorkItem() ? this.GetField("System.Title") : string.Empty;

        /// <summary>
        /// Gets the description of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Description => this.ValidateWorkItem() ? this.GetField("System.Description") : string.Empty;

        /// <summary>
        /// Gets the area path of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string AreaPath => this.ValidateWorkItem() ? this.GetField("System.AreaPath") : string.Empty;

        /// <summary>
        /// Gets the team project name of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string TeamProject => this.ValidateWorkItem() ? this.GetField("System.TeamProject") : string.Empty;

        /// <summary>
        /// Gets the iteration path of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string IterationPath => this.ValidateWorkItem() ? this.GetField("System.IterationPath") : string.Empty;

        /// <summary>
        /// Gets the type of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string WorkItemType => this.ValidateWorkItem() ? this.GetField("System.WorkItemType") : string.Empty;

        /// <summary>
        /// Gets the state of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string State => this.ValidateWorkItem() ? this.GetField("System.State") : string.Empty;

        /// <summary>
        /// Gets the reason of the state of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Reason => this.ValidateWorkItem() ? this.GetField("System.Reason") : string.Empty;

        /// <summary>
        /// Gets the create date of the work item.
        /// Returns DateTime.MinValue if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public DateTime CreateDate => this.ValidateWorkItem() ? this.GetFieldAsDate("System.CreatedDate") : DateTime.MinValue;

        /// <summary>
        /// Gets the modify date of the work item.
        /// Returns DateTime.MinValue if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public DateTime ChangeDate => this.ValidateWorkItem() ? this.GetFieldAsDate("System.ChangedDate") : DateTime.MinValue;

        /// <summary>
        /// Gets the tags of the work item.
        /// Returns empty list if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<string> Tags => this.ValidateWorkItem()
            ? (IEnumerable<string>)this.GetField("System.Tags").Split("; ", StringSplitOptions.RemoveEmptyEntries)
            : [];

        /// <summary>
        /// Gets the id of the parent work item.
        /// Returns zero if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int ParentWorkItemId => this.ValidateWorkItem() ? this.GetFieldAsInt("System.Parent") : 0;

        /// <summary>
        /// Gets the parent work item or null of no parent exists.
        /// </summary>
        /// <returns>The parent work item.</returns>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public AzureDevOpsWorkItem GetParentWorkItem()
        {
            if (this.ParentWorkItemId > 0)
            {
                var parentSettings = new AzureDevOpsWorkItemSettings(this.settings, this.ParentWorkItemId);

                return new AzureDevOpsWorkItem(this.log, parentSettings, this.workItemTrackingClientFactory);
            }

            return null;
        }

        /// <summary>
        /// Validates if a work item could be found.
        /// Depending on <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/>
        /// the work item instance can be null for subsequent calls.
        /// </summary>
        /// <returns>True if a valid work item instance exists.</returns>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/>
        /// is set to <c>true</c> and no work item could be found.</exception>
        private bool ValidateWorkItem()
        {
            if (this.HasWorkItemLoaded)
            {
                return true;
            }

            if (this.settings.ThrowExceptionIfWorkItemCouldNotBeFound)
            {
                throw new AzureDevOpsWorkItemNotFoundException("Work item not found");
            }

            this.log.Verbose("Skipping, since no work item instance could be found.");
            return false;
        }

        private string GetField(string fieldName)
        {
            return this.workItem.Fields.TryGetValue(fieldName, out var field) ? field.ToString() : string.Empty;
        }

        private DateTime GetFieldAsDate(string fieldName)
        {
            return this.workItem.Fields.TryGetValue(fieldName, out var field) ? (DateTime)field : DateTime.MinValue;
        }

        private int GetFieldAsInt(string fieldName)
        {
            return this.workItem.Fields.TryGetValue(fieldName, out var field) ? Convert.ToInt32(field) : 0;
        }
    }
}