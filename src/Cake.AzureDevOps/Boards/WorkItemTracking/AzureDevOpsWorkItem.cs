namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Authentication;
    using Cake.Core.Diagnostics;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using Microsoft.VisualStudio.Services.Common;

    /// <summary>
    /// Class to work with work item tracking.
    /// </summary>
    public sealed class AzureDevOpsWorkItem
    {
        private readonly ICakeLog log;
        private readonly IAzureDevOpsCredentials credentials;
        private readonly bool throwExceptionIfWorkItemCouldNotBeFound;
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
        internal AzureDevOpsWorkItem(ICakeLog log, AzureDevOpsWorkItemSettings settings, WorkItem workItem)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            workItem.NotNull(nameof(workItem));

            this.log = log;
            this.workItem = workItem;
            this.workItemTrackingClientFactory = new WorkItemTrackingClientFactory();
            this.credentials = settings.Credentials;
            this.CollectionUrl = settings.CollectionUrl;
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
            this.credentials = settings.Credentials;
            this.CollectionUrl = settings.CollectionUrl;
            this.throwExceptionIfWorkItemCouldNotBeFound = settings.ThrowExceptionIfWorkItemCouldNotBeFound;

            using (var workItemTrackingClient = this.workItemTrackingClientFactory.CreateWorkItemTrackingClient(settings.CollectionUrl, settings.Credentials, out var authorizedIdenity))
            {
                this.log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdenity.Id,
                     authorizedIdenity.DisplayName);

                try
                {
                    if (settings.ProjectGuid != Guid.Empty)
                    {
                        this.log.Verbose("Read work item with ID {0} from project with ID {1}", settings.WorkItemId, settings.ProjectGuid);
                        this.workItem =
                            workItemTrackingClient
                                .GetWorkItemAsync(
                                    settings.ProjectGuid,
                                    settings.WorkItemId)
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
                                    settings.WorkItemId)
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
                    if (this.throwExceptionIfWorkItemCouldNotBeFound)
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
        public Uri CollectionUrl { get; }

        /// <summary>
        /// Gets the ID of the work item.
        /// Returns 0 if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public int WorkItemId
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return 0;
                }

                return this.workItem.Id ?? 0;
            }
        }

        /// <summary>
        /// Gets the title of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Title
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.Title");
            }
        }

        /// <summary>
        /// Gets the description of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Description
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.Description");
            }
        }

        /// <summary>
        /// Gets the area path of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string AreaPath
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.AreaPath");
            }
        }

        /// <summary>
        /// Gets the team project name of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string TeamProject
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.TeamProject");
            }
        }

        /// <summary>
        /// Gets the iteration path of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string IterationPath
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.IterationPath");
            }
        }

        /// <summary>
        /// Gets the type of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string WorkItemType
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.WorkItemType");
            }
        }

        /// <summary>
        /// Gets the state of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string State
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.State");
            }
        }

        /// <summary>
        /// Gets the reason of the state of the work item.
        /// Returns empty if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public string Reason
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return string.Empty;
                }

                return this.GetField("System.Reason");
            }
        }

        /// <summary>
        /// Gets the create date of the work item.
        /// Returns DateTime.MinValue if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public DateTime CreateDate
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return DateTime.MinValue;
                }

                return this.GetFieldAsDate("System.CreatedDate");
            }
        }

        /// <summary>
        /// Gets the modify date of the work item.
        /// Returns DateTime.MinValue if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public DateTime ChangeDate
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return DateTime.MinValue;
                }

                return this.GetFieldAsDate("System.ChangedDate");
            }
        }

        /// <summary>
        /// Gets the tags of the work item.
        /// Returns empty list if no work item could be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>false</c>.
        /// </summary>
        /// <exception cref="AzureDevOpsWorkItemNotFoundException">If work item could not be found and
        /// <see cref="AzureDevOpsWorkItemSettings.ThrowExceptionIfWorkItemCouldNotBeFound"/> is set to <c>true</c>.</exception>
        public IEnumerable<string> Tags
        {
            get
            {
                if (!this.ValidateWorkItem())
                {
                    return Array.Empty<string>();
                }

                return this.GetField("System.Tags").Split("; ", StringSplitOptions.RemoveEmptyEntries);
            }
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

            if (this.throwExceptionIfWorkItemCouldNotBeFound)
            {
                throw new AzureDevOpsWorkItemNotFoundException("Work item not found");
            }

            this.log.Verbose("Skipping, since no work item instance could be found.");
            return false;
        }

        private string GetField(string fieldName)
        {
            if (this.workItem.Fields.TryGetValue(fieldName, out var field))
            {
                return field.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private DateTime GetFieldAsDate(string fieldName)
        {
            if (this.workItem.Fields.TryGetValue(fieldName, out var field))
            {
                return (DateTime)field;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
