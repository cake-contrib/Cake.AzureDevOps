namespace Cake.AzureDevOps.Tests.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Pipelines;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsBuildsSettingsTests
    {
        public sealed class TheCtorForProjectGuid
        {
            [Fact]
            public void Should_Throw_If_CollectionUrl_Is_Null()
            {
                // Given
                const Uri collectionUrl = null;
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials));

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
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials));

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
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials);

                // Then
                result.Credentials.ShouldBe(credentials);
            }
        }

        public sealed class TheCtorForProjectName
        {
            [Fact]
            public void Should_Throw_If_CollectionUrl_Is_Null()
            {
                // Given
                const Uri collectionUrl = null;
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials));

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
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials));

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
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_WhiteSpace()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = " ";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                const IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials);

                // Then
                result.Credentials.ShouldBe(credentials);
            }
        }

        public sealed class TheCtorForSettings
        {
            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                const AzureDevOpsBuildsSettings settings = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(settings));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials);

                // When
                var result = new AzureDevOpsBuildsSettings(settings);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildsSettings(collectionUrl, projectGuid, credentials);

                // When
                var result = new AzureDevOpsBuildsSettings(settings);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials);

                // When
                var result = new AzureDevOpsBuildsSettings(settings);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                const string projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildsSettings(collectionUrl, projectName, credentials);

                // When
                var result = new AzureDevOpsBuildsSettings(settings);

                // Then
                result.Credentials.ShouldBe(credentials);
            }
        }

        public sealed class TheCtorForEnvironmentVariables : IDisposable
        {
            private readonly string originalCollectionUrl;
            private readonly string originalProjectName;

            public TheCtorForEnvironmentVariables()
            {
                this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                this.originalProjectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                const IAzureDevOpsCredentials creds = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_Not_Set()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", null);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_Empty()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_WhiteSpace()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_Not_Set()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", null);

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_Empty()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", string.Empty);

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_WhiteSpace()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", " ");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
            }

            [Fact]
            public void Should_Set_Project_Name()
            {
                // Given
                const string projectName = "MyProject";
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", projectName);

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.Credentials.ShouldBe(creds);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", this.originalProjectName);
            }
        }

        public sealed class TheUsingAzurePipelinesOAuthTokenMethod : IDisposable
        {
            private readonly string originalCollectionUrl;
            private readonly string originalProjectName;
            private readonly string originalAccessToken;

            public TheUsingAzurePipelinesOAuthTokenMethod()
            {
                this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                this.originalProjectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
                this.originalAccessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", null);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsBuildsSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_Empty()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_WhiteSpace()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_Not_Set()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", null);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_Empty()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Project_Name_Env_Var_Is_WhiteSpace()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", " ");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildsSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = Record.Exception(() => AzureDevOpsBuildsSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Empty()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = Record.Exception(() => AzureDevOpsBuildsSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = Record.Exception(() => AzureDevOpsBuildsSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
            }

            [Fact]
            public void Should_Set_Project_Name()
            {
                // Given
                const string projectName = "MyProject";
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", projectName);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildsSettings(creds);

                // Then
                settings.Credentials.ShouldBe(creds);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", this.originalProjectName);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", this.originalAccessToken);
            }
        }
    }
}
