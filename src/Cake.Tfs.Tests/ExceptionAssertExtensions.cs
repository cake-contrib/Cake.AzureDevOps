﻿namespace Cake.Tfs
{
    using System;
    using Xunit;

    /// <summary>
    /// Extensions for asserting exceptions.
    /// </summary>
    public static class ExceptionAssertExtensions
    {
        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentNullException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentNullException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentNullException>(exception);
            Assert.Equal(parameterName, ((ArgumentNullException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentOutOfRangeException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Equal(parameterName, ((ArgumentOutOfRangeException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="ArgumentException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        /// <param name="parameterName">Expected name of the parameter which has caused the exception.</param>
        public static void IsArgumentException(this Exception exception, string parameterName)
        {
            Assert.IsType<ArgumentException>(exception);
            Assert.Equal(parameterName, ((ArgumentException)exception).ParamName);
        }

        /// <summary>
        /// Checks if an execption is of type <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="exception">Exception to check.</param>
        public static void IsInvalidOperationException(this Exception exception)
        {
            Assert.IsType<InvalidOperationException>(exception);
        }
    }
}
