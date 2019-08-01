namespace Cake.Tfs.Tests.Authentication
{
    using Cake.Tfs.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class TfsNtlmCredentialsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Not_Throw()
            {
                // Given / When
                var credentials = new TfsNtlmCredentials();

                // Then
                credentials.ShouldBeOfType<TfsNtlmCredentials>();
            }
        }
    }
}
