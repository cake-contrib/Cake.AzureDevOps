using System.Collections.Generic;

namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Represents a resource associated with a <see cref="AzureDevOpsBuildArtifact" />.
    /// </summary>
    public class AzureDevOpsArtifactResource
    {
        /// <summary>
        /// Gets the data of the resource.
        /// </summary>
        public string Data { get; internal set; }

        /// <summary>
        /// Gets the download url of the resource.
        /// </summary>
        public string DownloadUrl { get; internal set; }

        /// <summary>
        /// Gets the type of the resource.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the full http link to the resource.
        /// </summary>
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the properties for the resource.
        /// </summary>
        public Dictionary<string, string> Properties { get; internal set; }
    }
}
