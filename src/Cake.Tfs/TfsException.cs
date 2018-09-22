namespace Cake.Tfs
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents errors that occur during connecting to Team Foundation Server or Azure DevOps.
    /// </summary>
    [Serializable]
    public class TfsException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsException"/> class.
        /// </summary>
        public TfsException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public TfsException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public TfsException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about
        /// the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about
        /// the source or destination. </param>
        protected TfsException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
