namespace Cake.AzureDevOps.Tests.WorkItemTracking
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.WorkItemTracking;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsWorkItemSettingsTests
    {
        public sealed class TheCtorForProjectGuid
        {
            [Fact]
            public void Should_Throw_If_CollectionUrl_Is_Null()
            {
                // Given
                Uri collectionUrl = null;
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials));

                // Then
                result.IsArgumentNullException("collectionUrl");
            }

            [Fact]
            public void Should_Throw_If_ProjectGuid_Is_Empty()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.Empty;
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectGuid");
            }

            [Fact]
            public void Should_Throw_If_WorkItemId_Is_Zero()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 0;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_WorkItemId_Is_Negative(int workItemId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_WorkItemId(int workItemId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // Then
                result.WorkItemId.ShouldBe(workItemId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

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
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentNullException("collectionUrl");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                string projectName = null;
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentNullException("projectName");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_Empty()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = string.Empty;
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_ProjectName_Is_WhiteSpace()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = " ";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("projectName");
            }

            [Fact]
            public void Should_Throw_If_WorkItemId_Is_Zero()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 0;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_WorkItemId_Is_Negative(int workItemId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_WorkItemId(int workItemId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

                // Then
                result.WorkItemId.ShouldBe(workItemId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

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
                AzureDevOpsWorkItemSettings settings = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(settings));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Set_Collection_Url()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.CollectionUrl.ShouldBe(collectionUrl);
            }

            [Fact]
            public void Should_Set_ProjectGuid()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.ProjectGuid.ShouldBe(projectGuid);
            }

            [Fact]
            public void Should_Set_ProjectName()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.ProjectName.ShouldBe(projectName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_WorkItemId(int workItemId)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectGuid = Guid.NewGuid();
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectGuid, workItemId, credentials);

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.WorkItemId.ShouldBe(workItemId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials);

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.Credentials.ShouldBe(credentials);
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Set_ThrowExceptionIfWorkItemCouldNotBeFound(bool value)
            {
                // Given
                var collectionUrl = new Uri("http://example.com/collection");
                var projectName = "MyProject";
                var workItemId = 42;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsWorkItemSettings(collectionUrl, projectName, workItemId, credentials)
                {
                    ThrowExceptionIfWorkItemCouldNotBeFound = value,
                };

                // When
                var result = new AzureDevOpsWorkItemSettings(settings);

                // Then
                result.ThrowExceptionIfWorkItemCouldNotBeFound.ShouldBe(value);
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
                IAzureDevOpsCredentials creds = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

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

                // When
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

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
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

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
                var result = Record.Exception(() => AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(42));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

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
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(42, creds));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_WorkItem_Id_Value_Zero()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(0, creds));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Fact]
            public void Should_Throw_If_WorkItem_Id_Value_Negative()
            {
                // Given
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => new AzureDevOpsWorkItemSettings(-1, creds));

                // Then
                result.IsArgumentOutOfRangeException("workItemId");
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Not_Set_With_OAuthToken()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = Record.Exception(() => AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(42));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Empty_With_OAuthToken()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = Record.Exception(() => AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(42));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace_With_OAuthToken()
            {
                // Given
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = Record.Exception(() => AzureDevOpsWorkItemSettings.UsingAzurePipelinesOAuthToken(42));

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
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

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
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

                // Then
                settings.ProjectName.ShouldBe(projectName);
            }

            [Fact]
            public void Should_Set_WorkItem_Id()
            {
                // Given
                var workItemId = 42;
                var creds = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONCOLLECTIONURI", "https://example.com/collection");
                Environment.SetEnvironmentVariable("SYSTEM_TEAMPROJECT", "MyProject");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = new AzureDevOpsWorkItemSettings(workItemId, creds);

                // Then
                settings.WorkItemId.ShouldBe(workItemId);
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
                var settings = new AzureDevOpsWorkItemSettings(42, creds);

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