namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="DefinitionQueueStatus"/> class.
    /// </summary>
    internal static class DefinitionQueueStatusExtensions
    {
        /// <summary>
        /// Converts a <see cref="DefinitionQueueStatus"/> to an <see cref="AzureDevOpsDefinitionQueueStatus"/>.
        /// </summary>
        /// <param name="status">Status to convert.</param>
        /// <returns>Converted status.</returns>
        public static AzureDevOpsDefinitionQueueStatus ToAzureDevOpsDefinitionQueueStatus(this DefinitionQueueStatus status)
        {
            return status switch
            {
                DefinitionQueueStatus.Enabled => AzureDevOpsDefinitionQueueStatus.Enabled,
                DefinitionQueueStatus.Paused => AzureDevOpsDefinitionQueueStatus.Paused,
                DefinitionQueueStatus.Disabled => AzureDevOpsDefinitionQueueStatus.Disabled,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
