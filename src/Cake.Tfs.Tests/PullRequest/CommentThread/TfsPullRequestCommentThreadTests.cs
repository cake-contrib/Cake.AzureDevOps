namespace Cake.Tfs.Tests.PullRequest.CommentThread
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Tfs.PullRequest.CommentThread;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Microsoft.VisualStudio.Services.WebApi;
    using Shouldly;
    using Xunit;

    public sealed class TfsPullRequestCommentThreadTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Input_Thread_IsNull()
            {
                // Given, When
                var result = Record.Exception(() => new TfsPullRequestCommentThread(null));

                // Then
                result.IsArgumentNullException("thread");
            }

            [Fact]
            public void Should_Return_Empty_Comment_Thread()
            {
                // Given, When
                var thread = new TfsPullRequestCommentThread();
                var getCommentsResult = Record.Exception(() => thread.Comments);

                // Then
                thread.ShouldNotBeNull();
                thread.InnerThread.ShouldBeOfType(typeof(GitPullRequestCommentThread));
                thread.Id.ShouldBe(default(int));
                thread.FilePath.ShouldBeNull();
                thread.Status.ShouldBe(default(TfsCommentThreadStatus));
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
                    Comments = new List<Comment> { new Comment { Content = "Hello", CommentType = CommentType.Text, IsDeleted = false } },
                    Properties = new PropertiesCollection(),
                };

                // When
                var tfsCommentThread = new TfsPullRequestCommentThread(gitCommentThread);

                // Then
                tfsCommentThread.ShouldNotBeNull();
                tfsCommentThread.Id.ShouldBe(42);
                tfsCommentThread.Status.ShouldBe(TfsCommentThreadStatus.Pending);
                tfsCommentThread.FilePath.ShouldNotBeNull();
                tfsCommentThread.FilePath.FullPath.ShouldBe("src/myclass.cs");

                tfsCommentThread.Comments.ShouldNotBeNull();
                tfsCommentThread.Comments.ShouldNotBeEmpty();
                tfsCommentThread.Comments.ShouldHaveSingleItem();
                tfsCommentThread.Comments.First().ShouldNotBeNull();
                tfsCommentThread.Comments.First().Content.ShouldBe("Hello");
                tfsCommentThread.Comments.First().CommentType.ShouldBe(TfsCommentType.Text);
                tfsCommentThread.Comments.First().IsDeleted.ShouldBeFalse();

                tfsCommentThread.Properties.ShouldNotBeNull();
                tfsCommentThread.Properties.ShouldBeEmpty();
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
                var tfsThread = new TfsPullRequestCommentThread(thread);
                var filePath = tfsThread.FilePath;

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
                var tfsThread = new TfsPullRequestCommentThread(thread)
                {
                    FilePath = "/path/to/myclass.cs",
                };

                // Then
                tfsThread.FilePath.ShouldNotBeNull();
                tfsThread.FilePath.FullPath.ShouldBe("path/to/myclass.cs");
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
                var tfsThread = new TfsPullRequestCommentThread(thread);
                var result = Record.Exception(() => tfsThread.Comments);

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
                var tfsThread = new TfsPullRequestCommentThread(thread)
                {
                    Comments = new List<TfsComment> { new TfsComment("hi", false, CommentType.Text) },
                };

                // Then
                tfsThread.Comments.ShouldNotBeNull();
                tfsThread.Comments.ShouldHaveSingleItem();
                tfsThread.Comments.First().ShouldNotBeNull();
                tfsThread.Comments.First().ShouldBeOfType<TfsComment>();
                tfsThread.Comments.First().Content.ShouldBe("hi");
                tfsThread.Comments.First().CommentType.ShouldBe(TfsCommentType.Text);
                tfsThread.Comments.First().IsDeleted.ShouldBeFalse();
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
                var tfsThread = new TfsPullRequestCommentThread(thread);

                // Then
                tfsThread.Properties.ShouldBeNull();
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
                var tfsThread = new TfsPullRequestCommentThread(thread)
                {
                    Properties = new Dictionary<string, object>(),
                };

                // Then
                tfsThread.Properties.ShouldNotBeNull();
                tfsThread.Properties.ShouldBeOfType<PropertiesCollection>();
                tfsThread.Properties.ShouldBeEmpty();
            }

            [Fact]
            public void Should_Set_Colletion_With_Single_Element()
            {
                // Given
                var thread = new GitPullRequestCommentThread
                {
                    Id = 16,
                    Status = CommentThreadStatus.Active,
                    Properties = new PropertiesCollection(),
                };

                // When
                var tfsThread = new TfsPullRequestCommentThread(thread) { Properties = { ["fake"] = "value" } };

                // Then
                tfsThread.Properties.ShouldNotBeNull();
                tfsThread.Properties.ShouldNotBeEmpty();
                tfsThread.Properties.ShouldHaveSingleItem();

                tfsThread.Properties["fake"].ShouldNotBeNull();
                tfsThread.Properties["fake"].ShouldBe("value");
            }
        }

        public sealed class TheGetValueMethod
        {
            [Fact]
            public void Should_Throw_If_Property_Name_Is_Null()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => tfsThread.GetValue<string>(null));

                // Then
                result.IsArgumentNullException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Name_Is_Empty()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => tfsThread.GetValue<object>(string.Empty));

                // Then
                result.IsArgumentOutOfRangeException("propertyName");
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Collection_Is_Null_For_String_Value()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = tfsThread.GetValue<string>("key");

                // Then
                result.ShouldBe(default(string));
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Collection_Is_Null_For_Integer_Value()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = tfsThread.GetValue<int>("key");

                // Then
                result.ShouldBe(default(int));
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Does_Not_Exist_For_String_Value()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection(),
                    });

                // When
                var result = tfsThread.GetValue<string>("key");

                // Then
                result.ShouldBe(default(string));
            }

            [Fact]
            public void Should_Return_Default_Value_If_Property_Does_Not_Exist_For_Int_Value()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection(),
                    });

                // When
                var result = tfsThread.GetValue<int>("key");

                // Then
                result.ShouldBe(default(int));
            }

            [Fact]
            public void Should_Get_Valid_Properties()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection { { "one", "way" }, { "two", 2 } },
                    });

                // When
                var prop1 = tfsThread.GetValue<string>("one");
                var prop2 = tfsThread.GetValue<int>("two");
                var prop3 = tfsThread.GetValue<object>("ghost");

                // Then
                prop1.ShouldNotBeNull();
                prop1.ShouldBeOfType<string>();
                prop1.ShouldNotBeEmpty();
                prop1.ShouldBe("way");

                prop2.ShouldNotBeNull();
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
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => tfsThread.SetValue(null, string.Empty));

                // Then
                result.IsArgumentNullException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Name_Is_Empty()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => tfsThread.SetValue(string.Empty, new object()));

                // Then
                result.IsArgumentOutOfRangeException("propertyName");
            }

            [Fact]
            public void Should_Throw_If_Property_Collection_Is_Null()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread();

                // When
                var result = Record.Exception(() => tfsThread.SetValue("key", 1));

                // Then
                result.IsInvalidOperationException();
            }

            [Fact]
            public void Should_Set_Valid_Properties()
            {
                // Given
                var tfsThread = new TfsPullRequestCommentThread(
                    new GitPullRequestCommentThread
                    {
                        Id = 42,
                        Status = CommentThreadStatus.Active,
                        Properties = new PropertiesCollection { { "one", 1 } },
                    });

                // When
                tfsThread.SetValue("one", "string");
                tfsThread.SetValue("two", 2);

                // Then
                tfsThread.Properties.ShouldNotBeNull();
                tfsThread.Properties.ShouldNotBeEmpty();
                tfsThread.Properties.ShouldContainKeyAndValue("one", "string");
                tfsThread.Properties.ShouldContainKeyAndValue("two", 2);
            }
        }
    }
}
