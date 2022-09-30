﻿namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error if a build was not found.
    /// </summary>
    [Serializable]
    public class AzureDevOpsBuildNotFoundException : AzureDevOpsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildNotFoundException"/> class.
        /// </summary>
        public AzureDevOpsBuildNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public AzureDevOpsBuildNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildNotFoundException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public AzureDevOpsBuildNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBuildNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about
        /// the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about
        /// the source or destination. </param>
        protected AzureDevOpsBuildNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
