namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Class which contains settings for updating a build definition.
    /// </summary>
    public class AzureDevOpsUpdateBuildDefinitionSettings
    {
        /// <summary>
        /// Gets or sets the id of the build definition.
        /// </summary>
        public int Id;

        /// <summary>
        /// Gets or sets the status of the queue.
        /// </summary>
        public AzureDevOpsDefinitionQueueStatus QueueStatus;

        /// <summary>
        /// Gets or sets the save-time comment of the queue.
        /// </summary>
        public string Comment;
    }
}
