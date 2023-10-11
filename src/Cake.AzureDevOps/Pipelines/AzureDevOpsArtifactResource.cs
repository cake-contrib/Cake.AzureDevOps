// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Cake.AzureDevOps.Pipelines
{
    using System.Collections.Generic;
    using Cake.Common.Build.AzurePipelines.Data;

    /// <summary>
    /// Represents a resource associated with a <see cref="AzureDevOpsBuildArtifact" />.
    /// </summary>
    public class AzureDevOpsArtifactResource
    {
        /// <summary>
        /// Gets the data of the resource.
        /// </summary>
        public string Data { get; init; }

        /// <summary>
        /// Gets the download url of the resource.
        /// </summary>
        public string DownloadUrl { get; init; }

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        public AzurePipelinesArtifactType Type { get; init; }

        /// <summary>
        /// Gets the full http link to the resource.
        /// </summary>
        public string Url { get; init; }

        /// <summary>
        /// Gets the properties for the resource.
        /// </summary>
        public Dictionary<string, string> Properties { get; init; }
    }
}
