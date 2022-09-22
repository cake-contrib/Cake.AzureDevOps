namespace Cake.AzureDevOps.Tests.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Pipelines;
    using Cake.AzureDevOps.Tests.Fakes;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsBuildTests
    {
        public sealed class TheGetTestRunsMethod
        {
            [Fact]
            public void Should_Return_Empty_List_If_Build_Is_Invalid()
            {
                // Given
                var fixture = new BuildFixture(BuildFixture.ValidAzureDevOpsCollectionUrl, "Foo", 42);
                fixture.Settings.ThrowExceptionIfBuildCouldNotBeFound = false;
                fixture.BuildClientFactory = new FakeNullBuildClientFactory();

                var build = new AzureDevOpsBuild(
                    fixture.Log,
                    fixture.Settings,
                    fixture.BuildClientFactory,
                    fixture.TestManagementClientFactory);

                // When
                var result = build.GetTestRuns();

                // Then
                result.ShouldNotBeNull();
                result.ShouldBeEmpty();
                result.ShouldBeOfType<List<AzureDevOpsTestRun>>();
            }

            [Fact]
            public void Should_Return_Empty_List_If_Build_Does_Not_Contain_Test_Runs()
            {
                var fixture = new BuildFixture(BuildFixture.ValidAzureDevOpsCollectionUrl, "Foo", 1);
                var build = new AzureDevOpsBuild(
                    fixture.Log,
                    fixture.Settings,
                    fixture.BuildClientFactory,
                    fixture.TestManagementClientFactory);

                // When
                var result = build.GetTestRuns();

                // Then
                result.ShouldNotBeNull();
                result.ShouldBeEmpty();
                result.ShouldBeOfType<List<AzureDevOpsTestRun>>();
            }

            [Theory]
            [InlineData(0)]
            [InlineData(1)]
            public void Should_Return_List_Of_Test_Runs_With_X_Test_Results_If_X_Is_Less_Then_Total(int testRunsCount)
            {
                // Given
                var fixture = new BuildFixture(BuildFixture.ValidAzureDevOpsCollectionUrl, "Foo", 42);
                var build = new AzureDevOpsBuild(
                    fixture.Log,
                    fixture.Settings,
                    fixture.BuildClientFactory,
                    fixture.TestManagementClientFactory);

                // When
                var result = build.GetTestRuns(testRunsCount);

                // Then
                result.ShouldNotBeNull();
                result.ShouldHaveSingleItem();
                result.First().RunId.ShouldBe(1);

                var testResults = result.First().TestResults;
                testResults.Count().ShouldBe(testRunsCount);
            }

            [Fact]
            public void Should_Throw_If_Input_Test_Outcomes_Are_Invalid()
            {
                // Given
                var fixture = new BuildFixture(BuildFixture.ValidAzureDevOpsCollectionUrl, "Foo", 42);
                var build = new AzureDevOpsBuild(
                    fixture.Log,
                    fixture.Settings,
                    fixture.BuildClientFactory,
                    fixture.TestManagementClientFactory);

                // When
                var result = Record.Exception(() => build.GetTestRuns(null, new string[] { "FakeOutcome" }));

                // Then
                result.IsArgumentException(null);
            }

            [Fact]
            public void Should_Return_List_Of_Test_Runs_With_Test_Results()
            {
                // Given
                var fixture = new BuildFixture(BuildFixture.ValidAzureDevOpsCollectionUrl, "Foo", 42);
                var build = new AzureDevOpsBuild(
                    fixture.Log,
                    fixture.Settings,
                    fixture.BuildClientFactory,
                    fixture.TestManagementClientFactory);

                // When
                var result = build.GetTestRuns();

                // Then
                result.ShouldNotBeNull();
                result.ShouldHaveSingleItem();
                result.First().RunId.ShouldBe(1);

                var testResults = result.First().TestResults;
                testResults.ShouldNotBeNull();
                testResults.Count().ShouldBe(3);
                testResults.ElementAt(0).ShouldBeEquivalentTo(
                    new AzureDevOpsTestResult { AutomatedTestName = "t1", Outcome = "Passed", ErrorMessage = string.Empty });
                testResults.ElementAt(1).ShouldBeEquivalentTo(
                    new AzureDevOpsTestResult { AutomatedTestName = "t2", Outcome = "Failed", ErrorMessage = "Error" });
                testResults.ElementAt(2).ShouldBeEquivalentTo(
                    new AzureDevOpsTestResult { AutomatedTestName = "t3", Outcome = "Passed", ErrorMessage = string.Empty });
            }
        }
    }
}
