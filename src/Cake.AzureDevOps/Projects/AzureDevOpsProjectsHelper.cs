namespace Cake.AzureDevOps.Projects
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Collections;
    using Cake.Core.Diagnostics;

    /// <summary>
    /// Provides functions for AzureDevOps projects.
    /// </summary>
    internal static class AzureDevOpsProjectsHelper
    {
        /// <summary>
        /// Gets the projects for the parameter <paramref name="settings"/>.
        /// </summary>
        /// <param name="log">The Cake log context.</param>
        /// <param name="settings">Settings for getting the collection.</param>
        /// <returns>The projects or an empty list of projects if no projects were found for the <paramref name="settings"/>.</returns>
        internal static IEnumerable<AzureDevOpsProject> GetAzureDevOpsProjects(
            ICakeLog log,
            AzureDevOpsCollectionSettings settings)
        {
            log.NotNull(nameof(log));
            settings.NotNull(nameof(settings));

            using (var projectHttpClient = new ProjectClientFactory().CreateProjectClient(settings.CollectionUrl, settings.Credentials))
            {
                var projects =
                    projectHttpClient
                        .GetProjects()
                        .ConfigureAwait(false)
                        .GetAwaiter()
                        .GetResult()
                        .Select(x => x.ToAzureDevOpsProject())
                        .ToList();

                return projects;
            }
        }
    }
}
