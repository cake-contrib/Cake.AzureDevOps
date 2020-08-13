namespace Cake.AzureDevOps.Pipelines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Cake.Core.Diagnostics;
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Provides functions for AzureDevOps builds.
    /// </summary>
    internal static class AzureDevOpsBuildsHelper
    {
        /// <summary>
        /// Gets the builds for the parameter <paramref name="settings"/>.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for getting the build.</param>
        /// <returns>The builds or an empty list of builds if no builds were found for the <paramref name="settings"/>.</returns>
        /// <exception cref="InvalidOperationException">If no build definition was found for the <paramref name="settings"/>.</exception>
        internal static IEnumerable<AzureDevOpsBuild> GetAzureDevOpsBuilds(
            ICakeLog log,
            AzureDevOpsBuildsSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            var azureDevOpsBuilds = new List<AzureDevOpsBuild>();

            using (var buildHttpClient = new BuildClientFactory().CreateBuildClient(settings.CollectionUrl, settings.Credentials, out var authorizedIdenity))
            {
                log.Verbose(
                     "Authorized Identity:\n  Id: {0}\n  DisplayName: {1}",
                     authorizedIdenity.Id,
                     authorizedIdenity.DisplayName);

                BuildDefinitionReference buildDefinition = null;

                if (!string.IsNullOrEmpty(settings.BuildDefinitionName))
                {
                    buildDefinition = GetBuildDefinition(log, buildHttpClient, settings);
                    if (buildDefinition == null)
                    {
                        throw new InvalidOperationException($"Build definition '{settings.BuildDefinitionName}' not found");
                    }
                }

                List<int> buildsDefinitionIds =
                    buildDefinition == null ? null : new List<int>() { buildDefinition.Id };

                List<Build> builds = null;

                if (settings.ProjectGuid != Guid.Empty)
                {
                    builds =
                        buildHttpClient
                            .GetBuildsAsync(
                                settings.ProjectGuid,
                                definitions: buildsDefinitionIds,
                                statusFilter: settings.BuildStatus?.ToBuildStatus(),
                                resultFilter: settings.BuildResult?.ToBuildResult(),
                                queryOrder: settings.BuildQueryOrder?.ToBuildQueryOrder(),
                                branchName: settings.BranchName,
                                top: settings.Top,
                                maxBuildsPerDefinition: settings.MaxBuildsPerDefinition)
                            .ConfigureAwait(false)
                            .GetAwaiter()
                            .GetResult();
                }
                else if (!string.IsNullOrWhiteSpace(settings.ProjectName))
                {
                    builds =
                        buildHttpClient
                            .GetBuildsAsync(
                                settings.ProjectName,
                                definitions: buildsDefinitionIds,
                                statusFilter: settings.BuildStatus?.ToBuildStatus(),
                                resultFilter: settings.BuildResult?.ToBuildResult(),
                                queryOrder: settings.BuildQueryOrder?.ToBuildQueryOrder(),
                                branchName: settings.BranchName,
                                top: settings.Top,
                                maxBuildsPerDefinition: settings.MaxBuildsPerDefinition)
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

                azureDevOpsBuilds.AddRange(builds.Select(x => new AzureDevOpsBuild(log, settings, x)));
            }

            log.Verbose(
                "{0} builds found",
                azureDevOpsBuilds.Count);

            return azureDevOpsBuilds;
        }

        /// <summary>
        /// Returns the build definition for the <paramref name="settings"/>.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="buildHttpClient">The Http build client.</param>
        /// <param name="settings">Settings for accessing AzureDevOps.</param>
        /// <returns>The build definition for the BuildDefinitionName on <paramref name="settings"/>.
        /// <c>null</c> if the BuildDefinitionName was not set or no build definition was found.</returns>
        private static BuildDefinitionReference GetBuildDefinition(
            ICakeLog log,
            BuildHttpClient buildHttpClient,
            AzureDevOpsBuildsSettings settings)
        {
            log.NotNull(nameof(log));
            buildHttpClient.NotNull(nameof(buildHttpClient));
            settings.NotNull(nameof(settings));

            List<BuildDefinitionReference> buildDefinitions = null;

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

            var buildDefinition =
                buildDefinitions
                    .SingleOrDefault(x => x.Name.Equals(settings.BuildDefinitionName, StringComparison.InvariantCultureIgnoreCase));

            if (buildDefinition == null)
            {
                log.Verbose(
                   "Build definition '{0}' not found",
                   settings.BuildDefinitionName);
            }

            log.Verbose(
                "Build definition '{0}' found",
                settings.BuildDefinitionName);

            return buildDefinition;
        }
    }
}
