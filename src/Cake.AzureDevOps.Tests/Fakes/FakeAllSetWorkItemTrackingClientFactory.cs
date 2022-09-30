namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
    using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
    using Moq;

    public class FakeAllSetWorkItemTrackingClientFactory : FakeWorkItemTrackingClientFactory
    {
        public override WorkItemTrackingHttpClient CreateWorkItemTrackingClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<WorkItemTrackingHttpClient>(MockBehavior.Strict, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetWorkItemsAsync(It.IsAny<IEnumerable<int>>(), It.IsAny<IEnumerable<string>>(), It.IsAny<DateTime?>(), It.IsAny<WorkItemExpand?>(), It.IsAny<WorkItemErrorPolicy?>(), null, default))
                .ReturnsAsync((IEnumerable<int> workItemIds, IEnumerable<string> fields, DateTime? asOf, WorkItemExpand? expand, WorkItemErrorPolicy? errorPolicy, object userState, CancellationToken token) =>
                {
                    var result = new List<WorkItem>();

                    foreach (var workItemId in workItemIds)
                    {
                        result.Add(new WorkItem { Id = workItemId });
                    }

                    return result;
                });

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}