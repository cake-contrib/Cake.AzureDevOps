namespace Cake.Tfs.Tests.PullRequest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.IO;
    using Cake.Tfs.PullRequest;
    using Cake.Tfs.PullRequest.CommentThread;
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

        public sealed class Create
        {
            [Fact]
            public void Should_Throw_Exception_If_Target_Branch_Not_Found()
            {
                // Given
                var fixture =
                    new CreatePullRequestFixture(
                        BasePullRequestFixture.ValidTfsUrl,
                        "testBranch",
                        "NotExistingBranch",
                        "test",
                        "test");

                // When
                var result =
                    Record.Exception(() => TfsPullRequest.Create(fixture.Log, fixture.GitClientFactory, fixture.Settings));

                // Then
                result.ShouldNotBe(null);
                result.IsExpected("Create");
                result.IsTfsBranchNotFoundException();
                result.Message.ShouldBe($"Branch not found \"NotExistingBranch\"");
            }

            [Fact]
            public void Should_Return_A_PullRequest()
            {
                // Given
                var fixture =
                    new CreatePullRequestFixture(
                        BasePullRequestFixture.ValidTfsUrl,
                        "testBranch",
                        "master",
                        "test",
                        "test");

                // When
                var result =
                    TfsPullRequest.Create(fixture.Log, fixture.GitClientFactory, fixture.Settings);

                // Then
                result.ShouldBeOfType<TfsPullRequest>();
            }

            [Fact]
            public void Should_Return_A_PullRequest_With_Fallback_To_Master_As_DefaultBranch()
            {
                // Given
                var fixture =
                    new CreatePullRequestFixture(
                        BasePullRequestFixture.ValidTfsUrl,
                        "testBranch",
                        null,
                        "test",
                        "test");

                // When
                var result =
                    TfsPullRequest.Create(fixture.Log, fixture.GitClientFactory, fixture.Settings);

                // Then
                result.ShouldBeOfType<TfsPullRequest>();
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

        public sealed class SetCommentThreadStatus
        {
            [Fact]
            public void Should_Activate_Comment_Thread()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 12);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.ActivateCommentThread(321);

                // Then
                // ?? Nothing to validate here since the method returns void
            }

            [Fact]
            public void Should_Resolve_Comment_Thread()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 21);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.ResolveCommentThread(123);

                // Then
                // ?? Nothing to validate here since the method returns void
            }

            [Fact]
            public void Should_Not_Throw_If_Null_Is_Returned()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 11)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.ActivateCommentThread(35);
                pullRequest.ResolveCommentThread(53);

                // Then
                // ?? Nothing to validate here since the method returns void
            }
        }

        public sealed class GetCommentThreads
        {
            [Fact]
            public void Should_Not_Fail_If_Empty_List_Is_Returned()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 33)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var threads = pullRequest.GetCommentThreads();

                // Then
                threads.ShouldNotBeNull();
                threads.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Return_Valid_Comment_Threads()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 44);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var threads = pullRequest.GetCommentThreads();

                // Then
                threads.ShouldNotBeNull();
                threads.ShouldNotBeEmpty();
                threads.Count().ShouldBe(2);

                TfsPullRequestCommentThread thread1 = threads.First();
                thread1.Id.ShouldBe(11);
                thread1.Status.ShouldBe(TfsCommentThreadStatus.Active);
                thread1.FilePath.ShouldNotBeNull();
                thread1.FilePath.FullPath.ShouldBe("some/path/to/file.cs");

                thread1.Comments.ShouldNotBeNull();
                thread1.Comments.ShouldNotBeEmpty();
                thread1.Comments.Count().ShouldBe(2);

                TfsComment comment11 = thread1.Comments.First();
                comment11.ShouldNotBeNull();
                comment11.Content.ShouldBe("Hello");
                comment11.IsDeleted.ShouldBe(false);
                comment11.CommentType.ShouldBe(TfsCommentType.CodeChange);

                TfsComment comment12 = thread1.Comments.Last();
                comment12.ShouldNotBeNull();
                comment12.Content.ShouldBe("Goodbye");
                comment12.IsDeleted.ShouldBe(true);
                comment12.CommentType.ShouldBe(TfsCommentType.Text);

                TfsPullRequestCommentThread thread2 = threads.Last();
                thread2.Id.ShouldBe(22);
                thread2.Status.ShouldBe(TfsCommentThreadStatus.Fixed);
                thread2.FilePath.ShouldBeNull();
                thread2.Comments.ShouldNotBeNull();
                thread2.Comments.ShouldBeEmpty();
            }
        }

        public sealed class CreateCommentThread
        {
            [Fact]
            public void Should_Throw_If_Input_Thread_Is_Null()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 100);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var result = Record.Exception(() => pullRequest.CreateCommentThread(null));

                // Then
                Assert.IsType<NullReferenceException>(result);
            }

            [Fact]
            public void Should_Not_Throw_If_Null_Is_Returned()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 100)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.CreateCommentThread(new TfsPullRequestCommentThread());

                // Then
                // ?? Nothing to validate here since the method returns void
            }

            [Fact]
            public void Should_Create_Valid_Comment_Thread()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 200);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                pullRequest.CreateCommentThread(new TfsPullRequestCommentThread { Id = 300, Status = TfsCommentThreadStatus.Pending, FilePath = "/index.html" });

                // Then
                // ?? Nothing to validate here since the method returns void
            }
        }

        public sealed class GetLatestIterationId
        {
            [Fact]
            public void Should_Throw_If_Null_Is_Returned()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 11)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var result = Record.Exception(() => pullRequest.GetLatestIterationId());

                // Then
                result.IsTfsException();
            }

            [Fact]
            public void Should_Return_Valid_Iteration_Id()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 12);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                int id = pullRequest.GetLatestIterationId();

                // Then
                id.ShouldBe(42);
            }

            [Fact]
            public void Should_Return_Invalid_Id_If_Something_Is_Wrong_With_Iteration()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 13);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                int id = pullRequest.GetLatestIterationId();

                // Then
                id.ShouldBe(-1);
            }
        }

        public sealed class GetIterationChanges
        {
            [Fact]
            public void Should_Not_Throw_If_Null_Is_Returned()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidAzureDevOpsUrl, 21)
                {
                    GitClientFactory = new FakeNullForMethodsGitClientFactory()
                };
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var changes = pullRequest.GetIterationChanges(42);

                // Then
                changes.ShouldBeNull();
            }

            [Fact]
            public void Should_Return_Collection_Of_Valid_Iteration_Changes()
            {
                // Given
                var fixture = new PullRequestFixture(PullRequestFixture.ValidTfsUrl, 22);
                var pullRequest = new TfsPullRequest(fixture.Log, fixture.Settings, fixture.GitClientFactory);

                // When
                var changes = pullRequest.GetIterationChanges(500);

                // Then
                changes.ShouldNotBeNull();
                changes.ShouldNotBeEmpty();
                changes.Count().ShouldBe(2);

                changes.First().ShouldNotBeNull();
                changes.First().ShouldBeOfType<TfsPullRequestIterationChange>();
                changes.First().ChangeId.ShouldBe(100);
                changes.First().ChangeTrackingId.ShouldBe(1);
                changes.First().ItemPath.ShouldBeOfType<FilePath>();
                changes.First().ItemPath.FullPath.ShouldBe("/src/my/class1.cs");

                changes.Skip(1).First().ShouldNotBeNull();
                changes.Skip(1).First().ShouldBeOfType<TfsPullRequestIterationChange>();
                changes.Skip(1).First().ChangeId.ShouldBe(200);
                changes.Skip(1).First().ChangeTrackingId.ShouldBe(2);
                changes.Skip(1).First().ItemPath.ShouldBeNull();
            }
        }
    }
}
