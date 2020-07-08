namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="ArtifactResource"/> class.
    /// </summary>
    internal static class ArtifactResourceExtensions
    {
        /// <summary>
        /// Converts a <see cref="ArtifactResource"/> to an <see cref="AzureDevOpsBuildArtifact"/>.
        /// </summary>
        /// <param name="artifactResource">Artifact resource record to convert.</param>
        /// <returns>Converted artifact resorce record.</returns>
        public static AzureDevOpsArtifactResource ToAzureDevOpsArtifactResource(this ArtifactResource artifactResource)
        {
            artifactResource.NotNull(nameof(artifactResource));

            return
                new AzureDevOpsArtifactResource
                {
                    Data = artifactResource.Data,
                    DownloadUrl = artifactResource.DownloadUrl,
                    Type = artifactResource.Type,
                    Url = artifactResource.Url,
                    Properties = artifactResource.Properties,
                };
        }
    }
}
