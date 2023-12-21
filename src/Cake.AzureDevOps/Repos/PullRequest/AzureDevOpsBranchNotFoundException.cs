namespace Cake.AzureDevOps.Repos.PullRequest
{
    using System;

    /// <summary>
    /// Represents an error if a branch was not found.
    /// </summary>
    [Serializable]
    public class AzureDevOpsBranchNotFoundException : AzureDevOpsException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBranchNotFoundException"/> class.
        /// </summary>
        public AzureDevOpsBranchNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBranchNotFoundException"/> class for a specific branch.
        /// </summary>
        /// <param name="branchName">The name of the branch which could not be found. </param>
        public AzureDevOpsBranchNotFoundException(string branchName)
            : base($"Branch not found \"{branchName}\"")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBranchNotFoundException"/> class for a specific branch
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="branchName">The name of the branch which could not be found. </param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public AzureDevOpsBranchNotFoundException(string branchName, Exception innerException)
            : base($"Branch not found \"{branchName}\"", innerException)
        {
        }
    }
}
