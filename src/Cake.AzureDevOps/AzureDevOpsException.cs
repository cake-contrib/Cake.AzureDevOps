namespace Cake.AzureDevOps
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that occur during connecting to Azure DevOps.
    /// </summary>
    [Serializable]
    public class AzureDevOpsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsException"/> class.
        /// </summary>
        public AzureDevOpsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public AzureDevOpsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public AzureDevOpsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
