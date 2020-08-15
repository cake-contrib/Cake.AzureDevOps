namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using Cake.AzureDevOps.Repos.PullRequest;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Shouldly;
    using Xunit;

    public sealed class PullRequestStatusExtensionsTests
    {
        public sealed class TheToAzureDevOpsPullRequestState
        {
            [Theory]
            [InlineData(PullRequestStatus.NotSet, AzureDevOpsPullRequestState.NotSet)]
            [InlineData(PullRequestStatus.Active, AzureDevOpsPullRequestState.Active)]
            [InlineData(PullRequestStatus.Completed, AzureDevOpsPullRequestState.Completed)]
            [InlineData(PullRequestStatus.Abandoned, AzureDevOpsPullRequestState.Abandoned)]
            public void Should_Return_Correct_Value(PullRequestStatus state, AzureDevOpsPullRequestState expectedResult)
            {
                // Given

                // When
                var result = state.ToAzureDevOpsPullRequestState();

                // Then
                result.ShouldBe(expectedResult);
            }

            [Theory]
            [InlineData(PullRequestStatus.All)]
            public void Should_Throw_If_Invalid_Value_Is_Passed(PullRequestStatus state)
            {
                // Given

                // When
                var result =
                    Record.Exception(() => state.ToAzureDevOpsPullRequestState());

                // Then
                result.IsArgumentOutOfRangeException("state");
            }
        }
    }
}
