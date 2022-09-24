namespace Cake.AzureDevOps.WorkItemTracking
{
    using Cake.AzureDevOps.Authentication;
    using Cake.Core.Diagnostics;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using TfsUrlParser;

    /// <summary>
    /// Class to work with work item tracking.
    /// </summary>
    public sealed class AzureDevOpsWorkItemTracking
    {
        private readonly ICakeLog log;
        private readonly IAzureDevOpsCredentials credentials;
        private readonly IWorkItemTrackingClientFactory workItemTrackingClientFactory;
    }
}

