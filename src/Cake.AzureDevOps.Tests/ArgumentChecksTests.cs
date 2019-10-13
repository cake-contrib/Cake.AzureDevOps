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

        public sealed class TheNotNullOrWhiteSpaceExtension
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

        public sealed class TheNotNegativeOrZeroExtension
        {
            [Theory]
            [InlineData(-1)]
            [InlineData(int.MinValue)]
            public void Should_Throw_If_Value_Is_Negative(int value)
            {
                // Given
                var parameterName = "foo";

                // When
                var result = Record.Exception(() => value.NotNegativeOrZero(parameterName));

                // Then
                result.IsArgumentOutOfRangeException(parameterName);
            }

            [Fact]
            public void Should_Throw_If_Value_Is_Zero()
            {
                // Given
                var value = 0;
                var parameterName = "foo";

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
                var parameterName = "foo";

                // When
                value.NotNegativeOrZero(parameterName);

                // Then
            }
        }
    }
}
