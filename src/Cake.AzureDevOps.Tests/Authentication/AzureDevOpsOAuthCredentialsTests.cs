namespace Cake.AzureDevOps.Tests.Authentication
{
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsOAuthCredentialsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Access_Token_Is_Null()
            {
                // Given
                const string accessToken = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsOAuthCredentials(accessToken));

                // Then
                result.IsArgumentNullException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_Empty()
            {
                // Given
                var accessToken = string.Empty;

                // When
                var result = Record.Exception(() => new AzureDevOpsOAuthCredentials(accessToken));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_WhiteSpace()
            {
                // Given
                const string accessToken = " ";

                // When
                var result = Record.Exception(() => new AzureDevOpsOAuthCredentials(accessToken));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Set_Access_Token()
            {
                // Given
                const string accessToken = "foo";

                // When
                var credentials = new AzureDevOpsOAuthCredentials(accessToken);

                // Then
                credentials.AccessToken.ShouldBe(accessToken);
            }
        }
    }
}
