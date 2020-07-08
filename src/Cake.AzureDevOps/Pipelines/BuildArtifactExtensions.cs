namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="BuildArtifact"/> class.
    /// </summary>
    internal static class BuildArtifactExtensions
    {
        /// <summary>
        /// Converts a <see cref="BuildArtifact"/> to an <see cref="AzureDevOpsBuildArtifact"/>.
        /// </summary>
        /// <param name="buildArtifact">Build artifact record to convert.</param>
        /// <returns>Converted build artifact record.</returns>
        public static AzureDevOpsBuildArtifact ToAzureDevOpsBuildArtifact(this BuildArtifact buildArtifact)
        {
            buildArtifact.NotNull(nameof(buildArtifact));

            return
                new AzureDevOpsBuildArtifact
                {
                    Id = buildArtifact.Id,
                    Name = buildArtifact.Name,
                    Resource = buildArtifact.Resource.ToAzureDevOpsArtifactResource(),
                };
        }
    }
}
