namespace Cake.AzureDevOps.Tests.Pipelines
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Pipelines;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsBuildSettingsTests
    {
        public sealed class TheCtorForProjectGuid
        {
            [Fact]
            public void Should_Throw_If_CollectionUrl_Is_Null()
            {
                // Given
                Uri collectionUrl = null;
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials));

                // Then
                result.IsArgumentNullException("collectionUrl");
            }

            [Fact]
            public void Should_Throw_If_ProjectGuid_Is_Empty()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.Empty;
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectGuid");
            }

            [Fact]
            public void Should_Throw_If_BuildId_Is_Zero()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 0;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("buildId");
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_BuildId_Is_Negative(int buildId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("buildId");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_BuildId(int buildId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // Then
                result.BuildId.ShouldBe(buildId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

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
                Uri collectionUrl = null;
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentNullException("collectionUrl");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                string projectName = null;
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentNullException("projectName");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_Empty()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = string.Empty;
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_WhiteSpace()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = " ";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_BuildId_Is_Zero()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 0;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("buildId");
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_BuildId_Is_Negative(int buildId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("buildId");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_BuildId(int buildId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

                // Then
                result.BuildId.ShouldBe(buildId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

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
                AzureDevOpsBuildSettings settings = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(settings));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_BuildId(int buildId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectGuid, buildId, credentials);

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.BuildId.ShouldBe(buildId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials);

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.Credentials.ShouldBe(credentials);
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Set_ThrowExceptionIfBuildCouldNotBeFound(bool value)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var buildId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsBuildSettings(collectionUrl, projectName, buildId, credentials)
                {
                    ThrowExceptionIfBuildCouldNotBeFound = value,
                };

                // When
                var result = new AzureDevOpsBuildSettings(settings);

                // Then
                result.ThrowExceptionIfBuildCouldNotBeFound.ShouldBe(value);
            }
        }

        public sealed class TheCtorForEnvironmentVariables : IDisposable
        {
            private readonly string originalCollectionUrl;
            private readonly string originalProjectName;
            private readonly string originalBuildId;

            public TheCtorForEnvironmentVariables()
            {
                this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                this.originalProjectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
                this.originalBuildId = Environment.GetEnvironmentVariable("BUILD_BUILDID");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                IAzureDevOpsCredentials creds = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Env_Var_Is_Not_Set()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", null);

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Env_Var_Is_Not_Integer()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Is_Zero()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "0");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
            }

            [Fact]
            public void Should_Set_Project_Name()
            {
                // Given
                var projectName = "MyProject";
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", projectName);
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Build_Id()
            {
                // Given
                var buildId = 42;
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", buildId.ToString());

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.BuildId.ShouldBe(buildId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.Credentials.ShouldBe(creds);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", this.originalProjectName);
                Environment.SetEnvironmentVariable("BUILD_BUILDID", this.originalBuildId);
            }
        }

        public sealed class TheUsingAzurePipelinesOAuthTokenMethod : IDisposable
        {
            private readonly string originalCollectionUrl;
            private readonly string originalProjectName;
            private readonly string originalBuildId;
            private readonly string originalAccessToken;

            public TheUsingAzurePipelinesOAuthTokenMethod()
            {
                this.originalCollectionUrl = Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI");
                this.originalProjectName = Environment.GetEnvironmentVariable("SYSTEM_TEAMPROJECT");
                this.originalBuildId = Environment.GetEnvironmentVariable("BUILD_BUILDID");
                this.originalAccessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
            }

            [Fact]
            public void Should_Throw_If_Collection_Url_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", null);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken());

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Env_Var_Is_Not_Set()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", null);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Env_Var_Is_Not_Integer()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "foo");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Build_Id_Is_Zero()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "0");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsBuildSettings(creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = Record.Exception(() => AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Empty()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = Record.Exception(() => AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken());

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = Record.Exception(() => AzureDevOpsBuildSettings.UsingAzurePipelinesOAuthToken());

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
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.CollectionUrl.ToString().ShouldBe(new Uri("https://example.com/collection").ToString());
            }

            [Fact]
            public void Should_Set_Project_Name()
            {
                // Given
                var projectName = "MyProject";
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", projectName);
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_Build_Id()
            {
                // Given
                var buildId = 42;
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", buildId.ToString());
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.BuildId.ShouldBe(buildId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("BUILD_BUILDID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsBuildSettings(creds);

                // Then
                settings.Credentials.ShouldBe(creds);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", this.originalCollectionUrl);
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", this.originalProjectName);
                Environment.SetEnvironmentVariable("BUILD_BUILDID", this.originalBuildId);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", this.originalAccessToken);
            }
        }
    }
}
