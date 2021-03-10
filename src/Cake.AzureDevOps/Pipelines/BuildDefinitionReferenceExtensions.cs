namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="BuildDefinitionReference"/> class.
    /// </summary>
    internal static class BuildDefinitionReferenceExtensions
    {
        /// <summary>
        /// Converts a <see cref="BuildDefinitionReference"/> to an <see cref="AzureDevOpsBuildDefinition"/>.
        /// </summary>
        /// <param name="buildDefinitionReference">Build definition reference to convert.</param>
        /// <returns>Converted build definition.</returns>
        public static AzureDevOpsBuildDefinition ToAzureDevOpsBuildDefinition(this BuildDefinitionReference buildDefinitionReference)
        {
            buildDefinitionReference.NotNull(nameof(buildDefinitionReference));

            return
                new AzureDevOpsBuildDefinition
                {
                    Id = buildDefinitionReference.Id,
                    Name = buildDefinitionReference.Name,
                    QueueStatus = buildDefinitionReference.QueueStatus.ToAzureDevOpsDefinitionQueueStatus(),
                };
        }
    }
}
