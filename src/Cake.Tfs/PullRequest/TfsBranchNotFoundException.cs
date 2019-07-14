namespace Cake.Tfs.PullRequest
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Represents an error if a branch was not found.
    /// </summary>
    [Serializable]
    public class TfsBranchNotFoundException : TfsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBranchNotFoundException"/> class.
        /// </summary>
        public TfsBranchNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBranchNotFoundException"/> class for a specific branch.
        /// </summary>
        /// <param name="branchName">The name of the branch which could not be found. </param>
        public TfsBranchNotFoundException(string branchName)
            : base($"Branch not found \"{branchName}\"")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBranchNotFoundException"/> class for a specific branch
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="branchName">The name of the branch which could not be found. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public TfsBranchNotFoundException(string branchName, Exception innerException)
            : base($"Branch not found \"{branchName}\"", innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TfsBranchNotFoundException"/> class with serialized data.
        /// </summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about
        /// the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about
        /// the source or destination. </param>
        protected TfsBranchNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
