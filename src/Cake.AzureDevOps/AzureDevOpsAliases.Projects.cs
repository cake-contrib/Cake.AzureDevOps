namespace Cake.AzureDevOps
{
    using System.Collections.Generic;
    using Cake.AzureDevOps.Collections;
    using Cake.AzureDevOps.Projects;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to Azure DevOps projects.
    /// </content>
    public static partial class AzureDevOpsAliases
    {
        /// <summary>
        /// Gets all projects or a collection.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="settings">Settings for getting the projects.</param>
        /// <example>
        /// <para>Get the projects associated with an Azure DevOps collection:</para>
        /// <code>
        /// <![CDATA[
        /// var collectionSettings =
        ///     new AzureDevOpsCollectionSettings(
        ///         new Uri("http://myserver:8080/defaultcollection"),
        ///         AzureDevOpsAuthenticationNtlm());
        ///
        /// var projects =
        ///     AzureDevOpsProjects(
        ///         collectionSettings);
        /// ]]>
        /// </code>
        /// </example>
        /// <returns>The projects or an empty list of projects if no projects were found for the <paramref name="settings"/>.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Azure Pipelines")]
        [CakeNamespaceImport("Cake.AzureDevOps.Projects")]
        [CakeNamespaceImport("Cake.AzureDevOps.Collections")]
        public static IEnumerable<AzureDevOpsProject> AzureDevOpsProjects(
            this ICakeContext context,
            AzureDevOpsCollectionSettings settings)
        {
            context.NotNull(nameof(context));
            settings.NotNull(nameof(settings));

            return AzureDevOpsProjectsHelper.GetAzureDevOpsProjects(context.Log, settings);
        }
    }
}
