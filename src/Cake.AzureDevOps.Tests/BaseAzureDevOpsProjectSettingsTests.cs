namespace Cake.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class BaseAzureDevOpsProjectSettingsTests
    {
        // ReSharper disable once ClassNeverInstantiated.Global
        public sealed class TheCtor
        {
            public sealed class WithCollectionUrlAndProjectGuidAndCredentialsParameter
            {
                [Fact]
                public void Should_Throw_If_CollectionUrl_Is_Null()
                {
                    // Given
                    const Uri collectionUrl = null;
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials));

                    // Then
                    result.IsArgumentNullException("collectionUrl");
                }

                [Fact]
                public void Should_Throw_If_ProjectGuid_Is_Empty()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.Empty;
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials));

                    // Then
                    result.IsArgumentOutOfRangeException("projectGuid");
                }

                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    const IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_ProjectGuid()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // Then
                    result.ProjectGuid.ShouldBe(projectGuid);
                }

                [Fact]
                public void Should_Set_Collection_Url()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // Then
                    result.CollectionUrl.ShouldBe(collectionUrl);
                }
            }

            public sealed class WithCollectionUrlAndProjectNameAndCredentialsParameter
            {
                [Fact]
                public void Should_Throw_If_CollectionUrl_Is_Null()
                {
                    // Given
                    const Uri collectionUrl = null;
                    var projectName = "MyProject";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials));

                    // Then
                    result.IsArgumentNullException("collectionUrl");
                }

                [Fact]
                public void Should_Throw_If_ProjectName_Is_Null()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    const string projectName = null;
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials));

                    // Then
                    result.IsArgumentNullException("projectName");
                }

                [Fact]
                public void Should_Throw_If_ProjectName_Is_Empty()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = string.Empty;
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials));

                    // Then
                    result.IsArgumentOutOfRangeException("projectName");
                }

                [Fact]
                public void Should_Throw_If_ProjectName_Is_WhiteSpace()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = " ";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials));

                    // Then
                    result.IsArgumentOutOfRangeException("projectName");
                }

                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = "MyProject";
                    const IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = "MyProject";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_ProjectName()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = "MyProject";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials);

                    // Then
                    result.ProjectName.ShouldBe(projectName);
                }

                [Fact]
                public void Should_Set_Collection_Url()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = "MyProject";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials);

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
                    const BaseAzureDevOpsProjectSettingsImpl settings = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(settings));

                    // Then
                    result.IsArgumentNullException("settings");
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(settings);

                    // Then
                    result.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_ProjectGuid()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(settings);

                    // Then
                    result.ProjectGuid.ShouldBe(projectGuid);
                }

                [Fact]
                public void Should_Set_ProjectName()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectName = "MyProject";
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectName, credentials);

                    // When
                    var result = new BaseAzureDevOpsProjectSettingsImpl(settings);

                    // Then
                    result.ProjectName.ShouldBe(projectName);
                }

                [Fact]
                public void Should_Set_CollectionUrl()
                {
                    // Given
                    var collectionUrl = new Uri("http://example.com/collection");
                    var projectGuid = Guid.NewGuid();
                    var credentials = AuthenticationProvider.AuthenticationNtlm();
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(collectionUrl, projectGuid, credentials);

                    // When
                    var result = new BaseAzureDevOpsCollectionSettingsImpl(settings);

                    // Then
                    result.CollectionUrl.ShouldBe(collectionUrl);
                }
            }

            public sealed class WithCredentialsParameter : IDisposable
            {
                private readonly string originalCollectionUrl;
                private readonly string originalProjectName;

                public WithCredentialsParameter()
                {
                    this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                    this.originalProjectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
                }

                [Fact]
                public void Should_Throw_If_Credentials_Are_Null()
                {
                    // Given
                    const IAzureDevOpsCredentials credentials = null;

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsArgumentNullException("credentials");
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_Not_Set()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", null);
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_Empty()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", string.Empty);
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Collection_Url_Env_Var_Is_WhiteSpace()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", " ");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Project_Name_Env_Var_Is_Not_Set()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", null);

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Project_Name_Env_Var_Is_Empty()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", string.Empty);

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Throw_If_Project_Name_Env_Var_Is_WhiteSpace()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", " ");

                    // When
                    var result = Record.Exception(() => new BaseAzureDevOpsProjectSettingsImpl(credentials));

                    // Then
                    result.IsInvalidOperationException();
                }

                [Fact]
                public void Should_Set_Credentials()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                    // When
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(credentials);

                    // Then
                    settings.Credentials.ShouldBe(credentials);
                }

                [Fact]
                public void Should_Set_Collection_Url()
                {
                    // Given
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                    // When
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(credentials);

                    // Then
                    settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
                }

                [Fact]
                public void Should_Set_Project_Name()
                {
                    // Given
                    const string projectName = "MyProject";
                    var credentials = new AzureDevOpsNtlmCredentials();
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", projectName);

                    // When
                    var settings = new BaseAzureDevOpsProjectSettingsImpl(credentials);

                    // Then
                    settings.ProjectName.ShouldBe(projectName);
                }

                public void Dispose()
                {
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                    Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", this.originalProjectName);
                }
            }
        }
    }
}
