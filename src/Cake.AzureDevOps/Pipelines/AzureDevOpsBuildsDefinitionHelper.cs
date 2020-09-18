namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Provides functions for AzureDevOps build definitions.
    /// </summary>
    internal static class AzureDevOpsBuildsDefinitionHelper
    {
        /// <summary>
        /// Returns the build definitions for the <paramref name="settings"/>.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <returns>The build definitions for the the <paramref name="settings"/>.</returns>
        internal static IEnumerable<AzureDevOpsBuildDefinition> GetAzureDevOpsBuildDefinitions(
            ICakeLog log,
            AzureDevOpsBuildsSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            List<BuildDefinitionReference> buildDefinitions = null;

            using (var buildHttpClient = new BuildClientFactory().CreateBuildClient(settings.CollectionUrl, settings.Credentials))
            {
                if (settings.ProjectGuid != Guid.Empty)
                {
                    buildDefinitions =
                        buildHttpClient
                            .GetDefinitionsAsync(settings.ProjectGuid)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                }
                else if (!string.IsNullOrWhiteSpace(settings.ProjectName))
                {
                    buildDefinitions =
                        buildHttpClient
                            .GetDefinitionsAsync(settings.ProjectName)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(settings),
                        "Either ProjectGuid or ProjectName needs to be set");
                }

                log.Verbose(
                    "{0} Build definitions found",
                    buildDefinitions.Count);

                return buildDefinitions
                    .Select(x => x.ToAzureDevOpsBuildDefinition())
                    .ToList();
            }
        }

        /// <summary>
        /// Updates a build definition with the new build definition settings and returns the updated build definition.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <param name="updateBuildDefinitionSettings">The settings to update the build definition.</param>
        /// <returns>The updated Azure DevOps build definition.</returns>
        internal static AzureDevOpsBuildDefinition UpdateBuildDefinition(
            ICakeLog log,
            AzureDevOpsBuildsSettings settings,
            AzureDevOpsUpdateBuildDefinitionSettings updateBuildDefinitionSettings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));
            updateBuildDefinitionSettings.NotNull(nameof(updateBuildDefinitionSettings));

            using (var buildHttpClient = new BuildClientFactory().CreateBuildClient(settings.CollectionUrl, settings.Credentials))
            {
                BuildDefinition buildDefinitionToUpdate = null;

                if (settings.ProjectGuid != Guid.Empty)
                {
                    buildDefinitionToUpdate =
                        buildHttpClient
                            .GetDefinitionAsync(settings.ProjectGuid, updateBuildDefinitionSettings.Id)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                }
                else if (!string.IsNullOrWhiteSpace(settings.ProjectName))
                {
                    buildDefinitionToUpdate =
                       buildHttpClient
                           .GetDefinitionAsync(settings.ProjectName, updateBuildDefinitionSettings.Id)
                           .ConfigureAwait(false)
                           .GetAwaiter()
                           .GetResult();
                }
                else
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(settings),
                        "Either ProjectGuid or ProjectName needs to be set");
                }

                buildDefinitionToUpdate.QueueStatus = updateBuildDefinitionSettings.QueueStatus.ToDefinitionQueueStatus();
                buildDefinitionToUpdate.Comment = updateBuildDefinitionSettings.Comment;

                var updatedBuildDefinition =
                    buildHttpClient
                        .UpdateDefinitionAsync(buildDefinitionToUpdate)
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult();

                return updatedBuildDefinition.ToAzureDevOpsBuildDefinition();
            }
        }
    }
}
