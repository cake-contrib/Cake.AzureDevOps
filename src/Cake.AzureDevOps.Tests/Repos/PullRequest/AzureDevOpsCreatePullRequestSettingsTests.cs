namespace Cake.AzureDevOps.Tests.Repos.PullRequest
{
    using System;
    using Cake.AzureDevOps.Authentication;
    using Cake.AzureDevOps.Repos.PullRequest;
    using Shouldly;
    using Xunit;

    public sealed class AzureDevOpsCreatePullRequestSettingsTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_RepositoryUrl_Is_Null()
            {
                // Given
                Uri repositoryUrl = null;
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentNullException("repositoryUrl");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                string sourceRefName = null;
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentNullException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = string.Empty;
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_SourceRefName_Is_WhiteSpace()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = " ";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentOutOfRangeException("sourceRefName");
            }

            [Fact]
            public void Should_Throw_If_Title_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                string title = null;
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentNullException("title");
            }

            [Fact]
            public void Should_Throw_If_Title_Is_Empty()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = string.Empty;
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentOutOfRangeException("title");
            }

            [Fact]
            public void Should_Throw_If_Title_Is_WhiteSpace()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = " ";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentOutOfRangeException("title");
            }

            [Fact]
            public void Should_Throw_If_Description_Is_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                string description = null;
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentNullException("description");
            }

            [Fact]
            public void Should_Throw_If_Credentials_Are_Null()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                IAzureDevOpsCredentials credentials = null;

                // When
                var result =
                    Record.Exception(() =>
                        new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials));

                // Then
                result.IsArgumentNullException("credentials");
            }

            [Fact]
            public void Should_Set_Repository_Url()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.RepositoryUrl.ShouldBe(repositoryUrl);
            }

            [Fact]
            public void Should_Set_SourceRefName()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.SourceRefName.ShouldBe(sourceRefName);
            }

            [Theory]
            [InlineData("master")]
            [InlineData(null)]
            public void Should_Set_TargetRefName(string targetRefName)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.TargetRefName.ShouldBe(targetRefName);
            }

            [Fact]
            public void Should_Set_Title()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.Title.ShouldBe(title);
            }

            [Theory]
            [InlineData("Foo")]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Set_Description(string description)
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.Description.ShouldBe(description);
            }

            [Fact]
            public void Should_Set_Credentials()
            {
                // Given
                var repositoryUrl = new Uri("http://example.com");
                var sourceRefName = "foo";
                var targetRefName = "master";
                var title = "foo";
                var description = "bar";
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // When
                var result =
                    new AzureDevOpsCreatePullRequestSettings(repositoryUrl, sourceRefName, targetRefName, title, description, credentials);

                // Then
                result.Credentials.ShouldBe(credentials);
            }
        }
    }
}
