namespace Cake.AzureDevOps.Tests.Authentication
{
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AzureDevOpsNtlmCredentialsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Not_Throw()
            {
                // Given / When
                var credentials = new AzureDevOpsNtlmCredentials();

                // Then
                credentials.ShouldBeOfType<AzureDevOpsNtlmCredentials>();
            }
        }
    }
}
