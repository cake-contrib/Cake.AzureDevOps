namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using Cake.Common.Build.AzurePipelines.Data;
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
        /// <returns>Converted artifact resource record.</returns>
        public static AzureDevOpsArtifactResource ToAzureDevOpsArtifactResource(this ArtifactResource artifactResource)
        {
            artifactResource.NotNull(nameof(artifactResource));

            if (!Enum.TryParse(artifactResource.Type, true, out AzurePipelinesArtifactType type))
            {
                throw new InvalidOperationException($"Unexpected value for artifact type '{artifactResource.Type}'");
            }

            return
                new AzureDevOpsArtifactResource
                {
                    Data = artifactResource.Data,
                    DownloadUrl = artifactResource.DownloadUrl,
                    Type = type,
                    Url = artifactResource.Url,
                    Properties = artifactResource.Properties,
                };
        }
    }
}
