namespace Cake.Tfs.Tests.Authentication
{
    using Shouldly;
    using Tfs.Authentication;
    using Xunit;

    public sealed class TfsOAuthCredentialsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Access_Token_Is_Null()
            {
                // Given / When
                var result = Record.Exception(() => new TfsOAuthCredentials(null));

                // Then
                result.IsArgumentNullException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_Empty()
            {
                // Given / When
                var result = Record.Exception(() => new TfsOAuthCredentials(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_WhiteSpace()
            {
                // Given / When
                var result = Record.Exception(() => new TfsOAuthCredentials(" "));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Set_Access_Token()
            {
                // Given
                const string accessToken = "foo";

                // When
                var credentials = new TfsOAuthCredentials(accessToken);

                // Then
                credentials.AccessToken.ShouldBe(accessToken);
            }
        }
    }
}
