namespace Cake.AzureDevOps.Tests.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using Cake.AzureDevOps.Authentication;
    using Microsoft.TeamFoundation.TestManagement.WebApi;
    using Moq;

    public class FakeAllSetTestManagementClientFactory : FakeTestManagementClientFactory
    {
        public override TestManagementHttpClient CreateTestManagementClient(Uri collectionUrl, IAzureDevOpsCredentials credentials)
        {
            var mock = new Mock<TestManagementHttpClient>(MockBehavior.Strict, collectionUrl, credentials.ToVssCredentials());

            mock.Setup(arg => arg.GetTestResultDetailsForBuildAsync(It.IsAny<Guid>(), It.Is<int>(id => id == 1), null, null, null, null, null, null, null, default))
                .ReturnsAsync(() => new TestResultsDetails { ResultsForGroup = new List<TestResultsDetailsForGroup>() });

            mock.Setup(arg => arg.GetTestResultDetailsForBuildAsync(It.IsAny<Guid>(), It.Is<int>(id => id > 1), null, null, null, null, null, null, null, default))
                .ReturnsAsync(() => new TestResultsDetails
                {
                    ResultsForGroup = new List<TestResultsDetailsForGroup>()
                    {
                        new ()
                        {
                            Results = new List<TestCaseResult>()
                            {
                                new () { Id = 11, TestRun = new ShallowReference { Id = "1" } },
                                new () { Id = 12, TestRun = new ShallowReference { Id = "1" } },
                                new () { Id = 13, TestRun = new ShallowReference { Id = "1" } },
                            },
                        },
                    },
                });

            mock.Setup(arg => arg.GetTestResultsAsync(It.IsAny<Guid>(), It.IsAny<int>(), null, It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<IEnumerable<TestOutcome>>(), null, default))
                .ReturnsAsync((Guid projectId, int testRunId, ResultDetails? details, int? skip, int? top, IEnumerable<TestOutcome> outcomes, object userState, CancellationToken token) => new List<TestCaseResult>()
                {
                    new () { AutomatedTestName = "t1", Outcome = "Passed", ErrorMessage = string.Empty },
                    new () { AutomatedTestName = "t2", Outcome = "Failed", ErrorMessage = "Error" },
                    new () { AutomatedTestName = "t3", Outcome = "Passed", ErrorMessage = string.Empty },
                }.Take(top.Value).ToList());

            mock = this.Setup(mock);

            return mock.Object;
        }
    }
}
