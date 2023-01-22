namespace Cake.AzureDevOps.Tests
{
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class BaseAzureDevOpsCredentialsSettingsTests
    {
        public sealed class TheCtor
        {
            public sealed class WithCredentialsParameter
            {
                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    const IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCredentialsSettingsImpl(credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsCredentialsSettingsImpl(credentials);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }
            }

            public sealed class WithSettingsParameter
            {
                [Fact]
                public void Should_Throw_If_Settings_Are_Null()
                {
                    // Given
                    const BaseAzureDevOpsCredentialsSettingsImpl settings = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCredentialsSettingsImpl(settings));

                    // Then
                    result.IsArgumentNullException("settings");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsCredentialsSettingsImpl(credentials);

                    // When
                    var result = new BaseAzureDevOpsCredentialsSettingsImpl(settings);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }
            }
        }
    }
}
