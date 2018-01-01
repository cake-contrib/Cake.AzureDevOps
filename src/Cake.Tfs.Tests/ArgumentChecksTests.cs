namespace Cake.Tfs.Tests.Authentication
{
    using Xunit;

    public sealed class ArgumentChecksTests
    {
        public sealed class TheNotNullMethod
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                object value = null;
                var parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNull(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Not_Throw_If_Value_Is_Not_Null()
            {
                // Given
                var value = "foo";
                var parameterName = "foo";

                // When
                value.NotNull(parameterName);

                // Then
            }
        }

        public sealed class TheNotNullOrWhiteSpaceMethod
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                string value = null;
                var parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = " ";
                var parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_WhiteSpace()
            {
                // Given
                var value = " ";
                var parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Not_Throw_If_Value_Is_Not_Null()
            {
                // Given
                var value = "foo";
                var parameterName = "foo";

                // When
                value.NotNullOrWhiteSpace(parameterName);

                // Then
            }
        }
    }
}
