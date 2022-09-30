namespace Cake.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Repos.PullRequest;
    using Xunit;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    public static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an exception is of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="ArgumentException"/> or one of its descendants.</typeparam>
        /// <param name="exception">Exception to check.</param>
        /// <param name="expectedExceptionType">The expected type of the exception.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentException<T>(this T exception, Type expectedExceptionType, string parameterName)
            where T : ArgumentException
        {
            Assert.IsType(expectedExceptionType, exception);
            Assert.Equal(parameterName, exception.ParamName);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal(parameterName, ((ArgumentException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Equal(parameterName, ((ArgumentOutOfRangeException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        public static void IsInvalidOperationException(this Exception exception)
        {
            Assert.IsType<InvalidOperationException>(exception);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="UriFormatException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        public static void IsUrlFormatException(this Exception exception)
        {
            Assert.IsType<UriFormatException>(exception);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="AzureDevOpsPullRequestNotFoundException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        public static void IsAzureDevOpsPullRequestNotFoundException(this Exception exception)
        {
            Assert.IsType<AzureDevOpsPullRequestNotFoundException>(exception);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="AzureDevOpsException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        public static void IsAzureDevOpsException(this Exception exception)
        {
            Assert.IsType<AzureDevOpsException>(exception);
        }

        /// <summary>
        /// Checks if an exception is of type <see cref="AzureDevOpsBranchNotFoundException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="message">Exception message which should be checked.</param>
        public static void IsAzureDevOpsBranchNotFoundException(this Exception exception, string message)
        {
            Assert.IsType<AzureDevOpsBranchNotFoundException>(exception);
            Assert.Equal(message, exception.Message);
        }
    }
}
