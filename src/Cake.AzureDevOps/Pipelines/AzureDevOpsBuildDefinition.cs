namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Representation of a build definition.
    /// </summary>
    public class AzureDevOpsBuildDefinition
    {
        /// <summary>
        /// Gets the ID of the build definition.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Gets the name of the build definition.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the queue status.
        /// </summary>
        public AzureDevOpsDefinitionQueueStatus QueueStatus { get; internal set; }
    }
}
