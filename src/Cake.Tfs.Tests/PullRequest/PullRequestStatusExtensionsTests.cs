namespace Cake.Tfs.Tests.PullRequest
{
    using Cake.Tfs.PullRequest;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Shouldly;
    using Xunit;

    public sealed class PullRequestStatusExtensionsTests
    {
        public sealed class TheToTfsPullRequestState
        {
            [Theory]
            [InlineData(PullRequestStatus.NotSet, TfsPullRequestState.NotSet)]
            [InlineData(PullRequestStatus.Active, TfsPullRequestState.Active)]
            [InlineData(PullRequestStatus.Completed, TfsPullRequestState.Completed)]
            [InlineData(PullRequestStatus.Abandoned, TfsPullRequestState.Abandoned)]
            public void Should_Return_Correct_Value(PullRequestStatus state, TfsPullRequestState expectedResult)
            {
                // Given

                // When
                var result = state.ToTfsPullRequestState();

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
                    Record.Exception(() => state.ToTfsPullRequestState());

                // Then
                result.IsArgumentOutOfRangeException("state");
            }
        }
    }
}
