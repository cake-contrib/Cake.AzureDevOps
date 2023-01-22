namespace Cake.AzureDevOps.Tests
{
    using Xunit;

    public sealed class ArgumentChecksTests
    {
        public sealed class TheNotNullExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const object value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNull(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Not_Throw_If_Value_Is_Not_Null()
            {
                // Given
                const string value = "foo";
                const string parameterName = "foo";

                // When
                value.NotNull(parameterName);

                // Then
            }
        }

        public sealed class TheNotNullOrWhiteSpaceExtension
        {
            [Fact]
            public void Should_Throw_If_Value_Is_Null()
            {
                // Given
                const string value = null;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentNullException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Empty()
            {
                // Given
                var value = string.Empty;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_WhiteSpace()
            {
                // Given
                const string value = " ";
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNullOrWhiteSpace(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Not_Throw_If_Value_Is_Not_Null()
            {
                // Given
                const string value = "foo";
                const string parameterName = "foo";

                // When
                value.NotNullOrWhiteSpace(parameterName);

                // Then
            }
        }

        public sealed class TheNotNegativeOrZeroExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_Value_Is_Negative(int value)
            {
                // Given
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Zero()
            {
                // Given
                const int value = 0;
                const string parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Theory]
            [InlineData(1)]
            [InlineData(int.MaxValue)]
            public void Should_Not_Throw_If_Value_Is_Positive(int value)
            {
                // Given
                const string parameterName = "foo";

                // When
                value.NotNegativeOrZero(parameterName);

                // Then
            }
        }
    }
}
