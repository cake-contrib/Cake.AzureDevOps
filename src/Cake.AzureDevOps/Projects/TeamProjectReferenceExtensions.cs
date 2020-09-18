namespace Cake.AzureDevOps.Projects
{
    using Microsoft.TeamFoundation.Core.WebApi;

    /// <summary>
    /// Extensions for the <see cref="TeamProjectReference"/> class.
    /// </summary>
    internal static class TeamProjectReferenceExtensions
    {
        /// <summary>
        /// Converts a <see cref="TeamProjectReference"/> to an <see cref="AzureDevOpsProject"/>.
        /// </summary>
        /// <param name="teamProjectReference">Team project to convert.</param>
        /// <returns>Converted team project.</returns>
        public static AzureDevOpsProject ToAzureDevOpsProject(this TeamProjectReference teamProjectReference)
        {
            teamProjectReference.NotNull(nameof(teamProjectReference));

            return
                new AzureDevOpsProject
                {
                   Id = teamProjectReference.Id,
                   Name = teamProjectReference.Name,
                };
        }
    }
}
