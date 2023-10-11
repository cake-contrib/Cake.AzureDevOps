namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AzureDevOpsPullRequestSettingsTests
    {
        public sealed class TheCtorForSourceRefName
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                const Uri repositoryUrl = null;
                const string sourceBranch = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, sourceBranch, credentials));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceRefName = null;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, sourceRefName, credentials));

                // Then
                result.IsArgumentNullException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = string.Empty;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, sourceRefName, credentials));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_WhiteSpace()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceRefName = " ";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, sourceRefName, credentials));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceBranch = "foo";
                const IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, sourceBranch, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceBranch = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, sourceBranch, credentials);

                // Then
                result.RepositoryUrl.ShouldBe(repositoryUrl);
            }

            [Fact]
            public void Should_Set_SourceRefName()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceRefName = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, sourceRefName, credentials);

                // Then
                result.SourceRefName.ShouldBe(sourceRefName);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceBranch = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, sourceBranch, credentials);

                // Then
                result.Credentials.ShouldBe(credentials);
            }
        }

        public sealed class TheCtorForPullRequestId
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                const Uri repositoryUrl = null;
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_PullRequestId_Is_Zero()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 0;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("pullRequestId");
            }

            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_PullRequestId_Is_Negative(int pullRequestId)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials));

                // Then
                result.IsArgumentOutOfRangeException("pullRequestId");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                const IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

                // Then
                result.RepositoryUrl.ShouldBe(repositoryUrl);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_PullRequestId(int pullRequestId)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

                // Then
                result.PullRequestId.ShouldBe(pullRequestId);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

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
                const AzureDevOpsPullRequestSettings settings = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(settings));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

                // When
                var result = new AzureDevOpsPullRequestSettings(settings);

                // Then
                result.RepositoryUrl.ShouldBe(repositoryUrl);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Set_PullRequestId(int pullRequestId)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

                // When
                var result = new AzureDevOpsPullRequestSettings(settings);

                // Then
                result.PullRequestId.ShouldBe(pullRequestId);
            }

            [Fact]
            public void Should_Set_SourceBranch()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const string sourceBranch = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsPullRequestSettings(repositoryUrl, sourceBranch, credentials);

                // When
                var result = new AzureDevOpsPullRequestSettings(settings);

                // Then
                result.SourceRefName.ShouldBe(sourceBranch);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials);

                // When
                var result = new AzureDevOpsPullRequestSettings(settings);

                // Then
                result.Credentials.ShouldBe(credentials);
            }

            [Theory]
            [InlineData(true)]
            [InlineData(false)]
            public void Should_Set_ThrowExceptionIfPullRequestCouldNotBeFound(bool value)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                const int pullRequestId = 41;
                var credentials = AuthenticationProvider.AuthenticationNtlm();
                var settings = new AzureDevOpsPullRequestSettings(repositoryUrl, pullRequestId, credentials)
                {
                    ThrowExceptionIfPullRequestCouldNotBeFound = value,
                };

                // When
                var result = new AzureDevOpsPullRequestSettings(settings);

                // Then
                result.ThrowExceptionIfPullRequestCouldNotBeFound.ShouldBe(value);
            }
        }

        public sealed class TheCtorForEnvironmentVariables : IDisposable
        {
            private readonly string originalRepositoryUri;
            private readonly string originalPullRequestId;

            public TheCtorForEnvironmentVariables()
            {
                this.originalRepositoryUri = Environment.GetEnvironmentVariable("BUILD_REPOSITORY_URI");
                this.originalPullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                const IAzureDevOpsCredentials credentials = null;

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Not_Set()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", null);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Empty()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_WhiteSpace()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Empty()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", null);

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_WhiteSpace()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", " ");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Set()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", string.Empty);

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Integer()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "hello");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Is_Zero()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "0");

                // When
                var result = Record.Exception(() => new AzureDevOpsPullRequestSettings(credentials));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");

                // When
                var settings = new AzureDevOpsPullRequestSettings(credentials);

                // Then
                settings.RepositoryUrl.ShouldBe(new Uri("http://example.com"));
            }

            [Fact]
            public void Should_Set_Pull_Request_Id()
            {
                // Given
                var credentials = new AzureDevOpsNtlmCredentials();
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");

                // When
                var settings = new AzureDevOpsPullRequestSettings(credentials);

                // Then
                settings.PullRequestId.ShouldBe(42);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", this.originalRepositoryUri);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", this.originalPullRequestId);
            }
        }

        public sealed class TheUsingAzurePipelinesOAuthTokenMethod : IDisposable
        {
            private readonly string originalRepositoryUri;
            private readonly string originalPullRequestId;
            private readonly string originalAccessToken;

            public TheUsingAzurePipelinesOAuthTokenMethod()
            {
                this.originalRepositoryUri = Environment.GetEnvironmentVariable("BUILD_REPOSITORY_URI");
                this.originalPullRequestId = Environment.GetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID");
                this.originalAccessToken = Environment.GetEnvironmentVariable("SYSTEM_ACCESSTOKEN");
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", null);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Not_Set_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", null);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Repository_Url_Env_Var_Is_Not_Set_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", null);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Empty()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_Empty_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Repository_Url_Env_Var_Is_Empty_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_WhiteSpace()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Repository_Url_Env_Var_Is_WhiteSpace_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Repository_Url_Env_Var_Is_WhiteSpace_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", " ");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", null);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Set_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", null);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Set_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", null);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Empty()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Empty_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Pull_Request_Id_Env_Var_Is_Empty_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", string.Empty);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_WhiteSpace()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", " ");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_WhiteSpace_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", " ");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Pull_Request_Id_Env_Var_Is_WhiteSpace_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", " ");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Integer()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "foo");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Integer_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "foo");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Pull_Request_Id_Env_Var_Is_Not_Integer_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "foo");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Is_Zero()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "0");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_Pull_Request_Id_Is_Zero_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "0");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_Pull_Request_Id_Is_Zero_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "0");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Not_Set()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Not_Set_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_System_Access_Token_Env_Var_Is_Not_Set_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", null);

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Empty()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_Empty_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_System_Access_Token_Env_Var_Is_Empty_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", string.Empty);

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = Record.Exception(AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace_And_Parameter_Is_True()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = Record.Exception(() => AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(true));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Not_Throw_If_System_Access_Token_Env_Var_Is_WhiteSpace_And_Parameter_Is_False()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", " ");

                // When
                var result = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken(false);

                // Then
                result.ShouldBeNull();
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken();

                // Then
                settings.RepositoryUrl.ShouldBe(new Uri("http://example.com"));
            }

            [Fact]
            public void Should_Set_Pull_Request_Id()
            {
                // Given
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", "http://example.com");
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", "42");
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", "foo");

                // When
                var settings = AzureDevOpsPullRequestSettings.UsingAzurePipelinesOAuthToken();

                // Then
                settings.PullRequestId.ShouldBe(42);
            }

            public void Dispose()
            {
                Environment.SetEnvironmentVariable("BUILD_REPOSITORY_URI", this.originalRepositoryUri);
                Environment.SetEnvironmentVariable("SYSTEM_PULLREQUEST_PULLREQUESTID", this.originalPullRequestId);
                Environment.SetEnvironmentVariable("SYSTEM_ACCESSTOKEN", this.originalAccessToken);
            }
        }
    }
}
