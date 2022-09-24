namespace Cake.AzureDevOps.Projects
{
    using System;

    /// <summary>
    /// Class for writing issues to Azure DevOps pull requests.
    /// </summary>
    public sealed class AzureDevOpsProject
    {
        /// <summary>
        /// Gets the project identifier.
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// Gets the project name.
        /// </summary>
        public string Name { get; internal set; }
    }
}
