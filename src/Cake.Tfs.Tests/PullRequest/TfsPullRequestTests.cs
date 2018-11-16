namespace Cake.Tfs.Tests.PullRequest
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Tfs.PullRequest;
    using Cake.Tfs.Tests.PullRequest.Fakes;
    using Microsoft.VisualStudio.Services.Common;
    using Shouldly;
    using Xunit;

    public sealed class TfsPullRequestTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, "foo") { Log = null };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Log_Is_Null_Overload()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, "foo") { Log = null };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42) { Settings = null };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null_Overload()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42) { Settings = null };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings));

                // Then
                result.IsArgumentNullException("settings");
            }

            [Fact]
            public void Should_Throw_If_Git_Client_Factory_Is_Null()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42) { GitClientFactory = null };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsArgumentNullException("gitClientFactory");
            }

            [Fact]
            public void Should_Throw_If_Tfs_Url_Is_Invalid()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.InvalidTfsUrl, 42);

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsUrlFormatException();
            }

            [Fact]
            public void Should_Return_Valid_Tfs_Pull_Request_By_Id()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42);

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(true);
                pullRequest.PullRequestId.ShouldBe(42);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("MyCollection");
                pullRequest.CodeReviewId.ShouldBe(123);
                pullRequest.ProjectName.ShouldBe("MyTeamProject");
                pullRequest.SourceRefName.ShouldBe("foo");
                pullRequest.TargetRefName.ShouldBe("master");
                pullRequest.LastSourceCommitId.ShouldBe("4a92b977");
                pullRequest.LastTargetCommitId.ShouldBe("78a3c113");
            }

            [Fact]
            public void Should_Return_Valid_Azure_DevOps_Pull_Request_By_Id()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 16);

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(true);
                pullRequest.PullRequestId.ShouldBe(16);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("DefaultCollection");
                pullRequest.CodeReviewId.ShouldBe(123);
                pullRequest.ProjectName.ShouldBe("MyProject");
                pullRequest.SourceRefName.ShouldBe("foo");
                pullRequest.TargetRefName.ShouldBe("master");
                pullRequest.LastSourceCommitId.ShouldBe("4a92b977");
                pullRequest.LastTargetCommitId.ShouldBe("78a3c113");
            }

            [Fact]
            public void Should_Return_Valid_Tfs_Pull_Request_By_Source_Branch()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, "feature");

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(true);
                pullRequest.PullRequestId.ShouldBe(777);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("MyCollection");
                pullRequest.CodeReviewId.ShouldBe(123);
                pullRequest.ProjectName.ShouldBe("MyTeamProject");
                pullRequest.SourceRefName.ShouldBe("feature");
                pullRequest.TargetRefName.ShouldBe("master");
                pullRequest.LastSourceCommitId.ShouldBe("4a92b977");
                pullRequest.LastTargetCommitId.ShouldBe("78a3c113");
            }

            [Fact]
            public void Should_Return_Valid_Azure_DevOps_Pull_Request_By_Source_Branch()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, "feature");

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(true);
                pullRequest.PullRequestId.ShouldBe(777);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("DefaultCollection");
                pullRequest.CodeReviewId.ShouldBe(123);
                pullRequest.ProjectName.ShouldBe("MyProject");
                pullRequest.SourceRefName.ShouldBe("feature");
                pullRequest.TargetRefName.ShouldBe("master");
                pullRequest.LastSourceCommitId.ShouldBe("4a92b977");
                pullRequest.LastTargetCommitId.ShouldBe("78a3c113");
            }

            [Fact]
            public void Should_Return_Null_Tfs_Pull_Request_By_Id()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 101)
                    {
                        GitClientFactory = new FakeNullGitClientFactory(),
                        Settings = { ThrowExceptionIfPullRequestCouldNotBeFound = false }
                    };

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(false);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("MyCollection");
                pullRequest.ProjectName.ShouldBe("MyTeamProject");
                pullRequest.PullRequestId.ShouldBe(0);
                pullRequest.CodeReviewId.ShouldBe(0);
                pullRequest.SourceRefName.ShouldBeEmpty();
                pullRequest.TargetRefName.ShouldBeEmpty();
                pullRequest.LastSourceCommitId.ShouldBeEmpty();
                pullRequest.LastTargetCommitId.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Null_Azure_DevOps_Pull_Request_By_Id()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 101)
                    {
                        GitClientFactory = new FakeNullGitClientFactory(),
                        Settings = { ThrowExceptionIfPullRequestCouldNotBeFound = false }
                    };

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(false);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("DefaultCollection");
                pullRequest.ProjectName.ShouldBe("MyProject");
                pullRequest.PullRequestId.ShouldBe(0);
                pullRequest.CodeReviewId.ShouldBe(0);
                pullRequest.SourceRefName.ShouldBeEmpty();
                pullRequest.TargetRefName.ShouldBeEmpty();
                pullRequest.LastSourceCommitId.ShouldBeEmpty();
                pullRequest.LastTargetCommitId.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Null_Tfs_Pull_Request_By_Branch()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidTfsUrl, "somebranch")
                    {
                        GitClientFactory = new FakeNullGitClientFactory(),
                        Settings = { ThrowExceptionIfPullRequestCouldNotBeFound = false }
                    };

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(false);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("MyCollection");
                pullRequest.ProjectName.ShouldBe("MyTeamProject");
                pullRequest.PullRequestId.ShouldBe(0);
                pullRequest.CodeReviewId.ShouldBe(0);
                pullRequest.SourceRefName.ShouldBeEmpty();
                pullRequest.TargetRefName.ShouldBeEmpty();
                pullRequest.LastSourceCommitId.ShouldBeEmpty();
                pullRequest.LastTargetCommitId.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Null_Azure_DevOps_Pull_Request_By_Branch()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, "somebranch")
                    {
                        GitClientFactory = new FakeNullGitClientFactory(),
                        Settings = { ThrowExceptionIfPullRequestCouldNotBeFound = false }
                    };

                // When
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // Then
                pullRequest.ShouldNotBe(null);
                pullRequest.HasPullRequestLoaded.ShouldBe(false);
                pullRequest.RepositoryName.ShouldBe("MyRepoName");
                pullRequest.CollectionName.ShouldBe("DefaultCollection");
                pullRequest.ProjectName.ShouldBe("MyProject");
                pullRequest.PullRequestId.ShouldBe(0);
                pullRequest.CodeReviewId.ShouldBe(0);
                pullRequest.SourceRefName.ShouldBeEmpty();
                pullRequest.TargetRefName.ShouldBeEmpty();
                pullRequest.LastSourceCommitId.ShouldBeEmpty();
                pullRequest.LastTargetCommitId.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Throw_If_Strict_Is_On_And_Pull_Request_Is_Null_By_Id()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 1)
                    {
                        GitClientFactory = new FakeNullGitClientFactory()
                    };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsTfsPullRequestNotFoundException();
            }

            [Fact]
            public void Should_Throw_If_Strict_Is_On_And_Pull_Request_Is_Null_By_Branch()
            {
                // Given
                var fixture =
                    new PullRequestFixture(PullRequestFixture.ValidTfsUrl, "feature")
                    {
                        GitClientFactory = new FakeNullGitClientFactory()
                    };

                // When
                var result = Record.Exception(() => new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory));

                // Then
                result.IsTfsPullRequestNotFoundException();
            }
        }

        public sealed class Vote
        {
            [Fact]
            public void Should_Set_Approved_Vote_On_Tfs_Pull_Request()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 23);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.Vote(TfsPullRequestVote.Approved);

                // Then
                // ?? Nothing to validate here since the method returns void
            }

            [Fact]
            public void Should_Throw_If_Vote_Value_Is_Invalid_On_Tfs_Pull_Request()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 23);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var result = Record.Exception(() => pullRequest.Vote((TfsPullRequestVote)3));

                // Then
                result.ShouldNotBe(null);
                result.IsExpected("Vote");
                result.Message.ShouldBe("Something went wrong");
            }

            [Fact]
            public void Should_Throw_If_Null_Is_Returned_On_Tfs_Pull_Request()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 23)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var result = Record.Exception(() => pullRequest.Vote(TfsPullRequestVote.WaitingForAuthor));

                // Then
                result.ShouldNotBe(null);
                result.IsExpected("Vote");
                result.IsTfsPullRequestNotFoundException();
            }
        }

        public sealed class SetStatus
        {
            [Fact]
            public void Should_Throw_If_Tfs_Pull_Request_Status_Is_Null()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 16);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var result = Record.Exception(() => pullRequest.SetStatus(null));

                // Then
                result.IsArgumentNullException("status");
            }

            [Fact]
            public void Should_Throw_If_Tfs_Pull_Request_State_Is_Invalid()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 16);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);
                var status = new TfsPullRequestStatus("whatever") { State = (TfsPullRequestStatusState)123 };

                // When
                var result = Record.Exception(() => pullRequest.SetStatus(status));

                // Then
                result.ShouldNotBe(null);
                result.IsExpected("SetStatus");
                result.Message.ShouldBe("Unknown value");
            }

            [Fact]
            public void Should_Set_Valid_Status_On_Tfs_Pull_Request()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 16);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);
                var status = new TfsPullRequestStatus("Hello") { State = TfsPullRequestStatusState.Succeeded };

                // When
                pullRequest.SetStatus(status);

                // Then
                // ?? Nothing to validate here since the method returns void
            }

            [Fact]
            public void Should_Throw_If_Null_Is_Returned_On_Tfs_Pull_Request()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 16)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);
                var status = new TfsPullRequestStatus("One") { State = TfsPullRequestStatusState.Failed };

                // When
                var result = Record.Exception(() => pullRequest.SetStatus(status));

                // Then
                result.ShouldNotBe(null);
                result.IsExpected("SetStatus");
                result.IsTfsPullRequestNotFoundException();
            }
        }

        public sealed class GetModifiedFiles
        {
            [Fact]
            public void Should_Return_Empty_Collection_If_No_Changes_Found()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42) { GitClientFactory = new FakeNullForMethodsGitClientFactory() };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var files = pullRequest.GetModifiedFiles();

                // Then
                files.ShouldBeOfType<List<FilePath>>();
                files.ShouldNotBeNull();
                files.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Valid_Collection_Of_Modified_Files()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 42);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var files = pullRequest.GetModifiedFiles();

                // Then
                files.ShouldNotBeNull();
                files.ShouldNotBeEmpty();
                files.ShouldHaveSingleItem();

                var filePath = files.First();
                filePath.ShouldBeOfType<FilePath>();
                filePath.ShouldNotBeNull();
                filePath.FullPath.ShouldNotBeEmpty();
                filePath.FullPath.ShouldBe("src/project/myclass.cs");
            }
        }
    }
}
