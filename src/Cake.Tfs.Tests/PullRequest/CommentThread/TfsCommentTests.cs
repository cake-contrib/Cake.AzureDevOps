namespace Cake.Tfs.Tests.PullRequest.CommentThread
{
    using Cake.Tfs.PullRequest.CommentThread;
    using Microsoft.TeamFoundation.SourceControl.WebApi;
    using Shouldly;
    using Xunit;

    public sealed class TfsCommentTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Comment_Is_Null()
            {
                // Given, When
                var result = Record.Exception(() => new TfsComment(null));

                // Then
                result.IsArgumentNullException("comment");
            }

            [Fact]
            public void Should_Throw_If_Comment_Content_Is_Null()
            {
                // Given, When
                var result = Record.Exception(() => new TfsComment(null, false));

                // Then
                result.IsArgumentNullException("content");
            }

            [Fact]
            public void Should_Throw_If_Comment_Content_Is_Empty()
            {
                // Given, When
                var result = Record.Exception(() => new TfsComment(string.Empty, false));

                // Then
                result.IsArgumentOutOfRangeException("content");
            }

            [Fact]
            public void Should_Return_Empty_Comment()
            {
                // Given, When
                var comment = new TfsComment();

                // Then
                comment.ShouldNotBeNull();
                comment.Content.ShouldBe(default(string));
                comment.IsDeleted.ShouldBe(default(bool));
                comment.CommentType.ShouldBe(default(TfsCommentType));
            }

            [Fact]
            public void Should_Return_Valid_Comment_With_Default_Comment_Type()
            {
                // Given, When
                var comment = new TfsComment("Hello", false);

                // Then
                comment.ShouldNotBeNull();
                comment.Content.ShouldBe("Hello");
                comment.IsDeleted.ShouldBeFalse();
                comment.CommentType.ShouldBe(default(TfsCommentType));
            }

            [Fact]
            public void Should_Return_Valid_Comment_Via_Initializers()
            {
                // Given, When
                var comment = new TfsComment { Content = "All good", IsDeleted = false, CommentType = TfsCommentType.CodeChange };

                // Then
                comment.ShouldNotBeNull();
                comment.Content.ShouldBe("All good");
                comment.IsDeleted.ShouldBeFalse();
                comment.CommentType.ShouldBe(TfsCommentType.CodeChange);
            }
        }
    }
}
