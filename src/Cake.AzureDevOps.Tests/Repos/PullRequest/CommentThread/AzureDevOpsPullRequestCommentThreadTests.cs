namespace Cake.AzureDevOps.Tests.Repos.PullRequest.CommentThread
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Repos.PullRequest.CommentThread;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AzureDevOpsPullRequestCommentThreadTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Input_Thread_IsNull()
            {
                // Given, When
                var result = Record.Exception(() => new AzureDevOpsPullRequestCommentThread(null));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Return_Empty_Comment_Thread()
            {
                // Given, When
                var thread = new AzureDevOpsPullRequestCommentThread();
                var getCommentsResult = Record.Exception(() => thread.Comments);

                // Then
                thread.ShouldNotBeNull();
                thread.InnerThread.ShouldBeOfType(typeof(GitPullRequestCommentThread));
                thread.Id.ShouldBe(default);
                thread.FilePath.ShouldBeNull();
                thread.Status.ShouldBe(default);
                thread.Properties.ShouldBeNull();

                getCommentsResult.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Return_Valid_Comment_Thread()
            {
                // Given
                var gitCommentThread = new GitPullRequestCommentThread
                {
                    Id = 42,
                    Status = CommentThreadStatus.Pending,
                    ThreadContext = new CommentThreadContext { FilePath = "/src/myclass.cs" },
                    Comments =
                        [
                            new () { Content = "Hello", CommentType = CommentType.Text, IsDeleted = false }
                        ],
                    Properties = [],
                };

                // When
                var azureDevOpsCommentThread = new AzureDevOpsPullRequestCommentThread(gitCommentThread);

                // Then
                azureDevOpsCommentThread.ShouldNotBeNull();
                azureDevOpsCommentThread.Id.ShouldBe(42);
                azureDevOpsCommentThread.Status.ShouldBe(AzureDevOpsCommentThreadStatus.Pending);
                azureDevOpsCommentThread.FilePath.ShouldNotBeNull();
                azureDevOpsCommentThread.FilePath.FullPath.ShouldBe("src/myclass.cs");

                azureDevOpsCommentThread.Comments.ShouldNotBeNull();
                azureDevOpsCommentThread.Comments.ShouldNotBeEmpty();
                azureDevOpsCommentThread.Comments.ShouldHaveSingleItem();
                azureDevOpsCommentThread.Comments.First().ShouldNotBeNull();
                azureDevOpsCommentThread.Comments.First().Content.ShouldBe("Hello");
                azureDevOpsCommentThread.Comments.First().CommentType.ShouldBe(AzureDevOpsCommentType.Text);
                azureDevOpsCommentThread.Comments.First().IsDeleted.ShouldBeFalse();

                azureDevOpsCommentThread.Properties.ShouldNotBeNull();
                azureDevOpsCommentThread.Properties.ShouldBeEmpty();
            }
        }

        public sealed class TheFilePathProperty
        {
            [Fact]
            public void Should_Return_Null_If_Not_Initialized()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 16,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread);
                var filePath = azureDevOpsThread.FilePath;

                // Then
                filePath.ShouldBeNull();
            }

            [Fact]
            public void Should_Set_And_Trimmed_Properly()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 100,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread)
                {
                    FilePath = "/path/to/myclass.cs",
                };

                // Then
                azureDevOpsThread.FilePath.ShouldNotBeNull();
                azureDevOpsThread.FilePath.FullPath.ShouldBe("path/to/myclass.cs");
            }
        }

        public sealed class TheCommentsProperty
        {
            [Fact]
            public void Should_Throw_If_Not_Set()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 15,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread);
                var result = Record.Exception(() => azureDevOpsThread.Comments);

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Properly()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 15,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread)
                {
                    Comments =
                        [
                            new ("hi", false)
                        ],
                };

                // Then
                azureDevOpsThread.Comments.ShouldNotBeNull();
                azureDevOpsThread.Comments.ShouldHaveSingleItem();
                azureDevOpsThread.Comments.First().ShouldNotBeNull();
                azureDevOpsThread.Comments.First().ShouldBeOfType<AzureDevOpsComment>();
                azureDevOpsThread.Comments.First().Content.ShouldBe("hi");
                azureDevOpsThread.Comments.First().CommentType.ShouldBe(AzureDevOpsCommentType.Unknown);
                azureDevOpsThread.Comments.First().IsDeleted.ShouldBeFalse();
            }
        }

        public sealed class ThePropertiesProperty
        {
            [Fact]
            public void Should_Return_Null_If_Not_Set()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 16,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread);

                // Then
                azureDevOpsThread.Properties.ShouldBeNull();
            }

            [Fact]
            public void Should_Set_Empty_Collection()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 16,
                    Status = CommentThreadStatus.Active,
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread)
                {
                    Properties = new Dictionary<string, object>(),
                };

                // Then
                azureDevOpsThread.Properties.ShouldNotBeNull();
                azureDevOpsThread.Properties.ShouldBeOfType<PropertiesCollection>();
                azureDevOpsThread.Properties.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Set_Collection_With_Single_Element()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 16,
                    Status = CommentThreadStatus.Active,
                    Properties = [],
                };

                // When
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(thread) { Properties = { ["fake"] = "value" } };

                // Then
                azureDevOpsThread.Properties.ShouldNotBeNull();
                azureDevOpsThread.Properties.ShouldNotBeEmpty();
                azureDevOpsThread.Properties.ShouldHaveSingleItem();

                azureDevOpsThread.Properties["fake"].ShouldNotBeNull();
                azureDevOpsThread.Properties["fake"].ShouldBe("value");
            }
        }

        public sealed class TheGetValueMethod
        {
            [Fact]
            public void Should_Throw_If_Property_Name_Is_Null()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => azureDevOpsThread.GetValue<string>(null));

                // Then
                result.IsArgumentNullException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Name_Is_Empty()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => azureDevOpsThread.GetValue<object>(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("propertyName");
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Collection_Is_Null_For_String_Value()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = azureDevOpsThread.GetValue<string>("key");

                // Then
                result.ShouldBe(default);
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Collection_Is_Null_For_Integer_Value()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = azureDevOpsThread.GetValue<int>("key");

                // Then
                result.ShouldBe(default);
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Does_Not_Exist_For_String_Value()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = [],
                    });

                // When
                var result = azureDevOpsThread.GetValue<string>("key");

                // Then
                result.ShouldBe(default);
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Does_Not_Exist_For_Int_Value()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = [],
                    });

                // When
                var result = azureDevOpsThread.GetValue<int>("key");

                // Then
                result.ShouldBe(default);
            }

            [Fact]
            public void Should_Get_Valid_Properties()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection { { "one", "way" }, { "two", 2 } },
                    });

                // When
                var prop1 = azureDevOpsThread.GetValue<string>("one");
                var prop2 = azureDevOpsThread.GetValue<int>("two");
                var prop3 = azureDevOpsThread.GetValue<object>("ghost");

                // Then
                prop1.ShouldNotBeNull();
                prop1.ShouldBeOfType<string>();
                prop1.ShouldNotBeEmpty();
                prop1.ShouldBe("way");

                prop2.ShouldBeOfType<int>();
                prop2.ShouldBe(2);

                prop3.ShouldBeNull();
            }
        }

        public sealed class TheSetValueMethod
        {
            [Fact]
            public void Should_Throw_If_Property_Name_Is_Null()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => azureDevOpsThread.SetValue(null, string.Empty));

                // Then
                result.IsArgumentNullException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Name_Is_Empty()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => azureDevOpsThread.SetValue(string.Empty, new object()));

                // Then
                result.IsArgumentOutOfRangeException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Collection_Is_Null()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => azureDevOpsThread.SetValue("key", 1));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Valid_Properties()
            {
                // Given
                var azureDevOpsThread = new AzureDevOpsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection { { "one", 1 } },
                    });

                // When
                azureDevOpsThread.SetValue("one", "string");
                azureDevOpsThread.SetValue("two", 2);

                // Then
                azureDevOpsThread.Properties.ShouldNotBeNull();
                azureDevOpsThread.Properties.ShouldNotBeEmpty();
                azureDevOpsThread.Properties.ShouldContainKeyAndValue("one", "string");
                azureDevOpsThread.Properties.ShouldContainKeyAndValue("two", 2);
            }
        }
    }
}
