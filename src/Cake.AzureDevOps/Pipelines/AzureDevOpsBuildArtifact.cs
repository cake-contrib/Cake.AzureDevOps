// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Represents an artifact associated with a build.
    /// </summary>
    public class AzureDevOpsBuildArtifact
    {
        /// <summary>
        /// Gets the id of the artifact.
        /// </summary>
        public int Id { get; internal set; }

        /// <summary>
        /// Gets the name of the artifact.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the artifact resource.
        /// </summary>
        public AzureDevOpsArtifactResource Resource { get; internal set; }
    }
}