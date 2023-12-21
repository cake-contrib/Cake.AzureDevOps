namespace Cake.AzureDevOps.Boards.WorkItemTracking
{
    using System;

    /// <summary>
    /// Represents an error if a work item was not found.
    /// </summary>
    [Serializable]
    public class AzureDevOpsWorkItemNotFoundException : AzureDevOpsWorkItemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemNotFoundException"/> class.
        /// </summary>
        public AzureDevOpsWorkItemNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemNotFoundException"/> class.
        /// </summary>
        /// <param name="workItemId">ID of the work item that was searched.</param>
        public AzureDevOpsWorkItemNotFoundException(int workItemId)
            : this("Work item with ID " + workItemId + " not found")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemNotFoundException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error. </param>
        public AzureDevOpsWorkItemNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsWorkItemNotFoundException"/> class with a specified error message
        /// and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null
        /// reference if no inner exception is specified.</param>
        public AzureDevOpsWorkItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
