namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Represents of an build definition.
    /// </summary>
    public class AzureDevOpsBuildDefinition
    {
        /// <summary>
        /// Gets the attempt of the build.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Gets the name of the build definition reference.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the queue status.
        /// </summary>
        public AzureDevOpsDefinitionQueueStatus QueueStatus { get; internal set; }
    }
}
