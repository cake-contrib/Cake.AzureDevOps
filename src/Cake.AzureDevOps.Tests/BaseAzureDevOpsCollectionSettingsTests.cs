namespace Cake.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class BaseAzureDevOpsCollectionSettingsTests
    {
        public sealed class TheCtor
        {
            public sealed class WithCollectionUrlAndCredentialsParameter
            {
                [Fact]
                public void Should_Throw_If_CollectionUrl_Is_Null()
                {
                    // Given
                    Uri collectionUrl = null;
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials));

                    // Then
                    result.IsArgumentNullException("collectionUrl");
                }

                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_Collection_Url()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials);

                    // Then
                    result.CollectionUrl.ShouldBe(collectionUrl);
                }
            }

            public sealed class WithSettingsParameter
            {
                [Fact]
                public void Should_Throw_If_Settings_Are_Null()
                {
                    // Given
                    BaseAzureDevOpsCollectionSettingsImpl settings = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(settings));

                    // Then
                    result.IsArgumentNullException("settings");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials);

                    // When
                    var result = new BaseAzureDevOpsCollectionSettingsImpl(settings);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_CollectionUrl()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsCollectionSettingsImpl(collectionUrl, credentials);

                    // When
                    var result = new BaseAzureDevOpsCollectionSettingsImpl(settings);

                    // Then
                    result.CollectionUrl.ShouldBe(collectionUrl);
                }
            }

            public sealed class WithCredentialsParameter : IDisposable
            {
                private readonly string originalCollectionUrl;

                public WithCredentialsParameter()
                {
                    this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                }

                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_Not_Set()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", null);

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_Empty()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", string.Empty);

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_WhiteSpace()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", " ");

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsCollectionSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");

                    // When
                    var settings = new BaseAzureDevOpsCollectionSettingsImpl(credentials);

                    // Then
                    settings.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_Collection_Url()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");

                    // When
                    var settings = new BaseAzureDevOpsCollectionSettingsImpl(credentials);

                    // Then
                    settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
                }

                public void Dispose()
                {
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                }
            }
        }
    }
}
