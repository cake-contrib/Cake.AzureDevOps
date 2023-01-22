namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="AzureDevOpsBuildQueryOrder"/> class.
    /// </summary>
    internal static class AzureDevOpsBuildQueryOrderExtensions
    {
        /// <summary>
        /// Converts a <see cref="AzureDevOpsBuildQueryOrderExtensions"/> to an <see cref="BuildQueryOrder"/>.
        /// </summary>
        /// <param name="queryOrder">Query order to convert.</param>
        /// <returns>Converted query order.</returns>
        public static BuildQueryOrder ToBuildQueryOrder(this AzureDevOpsBuildQueryOrder queryOrder)
        {
            return queryOrder switch
            {
                AzureDevOpsBuildQueryOrder.FinishTimeAscending => BuildQueryOrder.FinishTimeAscending,
                AzureDevOpsBuildQueryOrder.FinishTimeDescending => BuildQueryOrder.FinishTimeDescending,
                AzureDevOpsBuildQueryOrder.QueueTimeDescending => BuildQueryOrder.QueueTimeDescending,
                AzureDevOpsBuildQueryOrder.QueueTimeAscending => BuildQueryOrder.QueueTimeAscending,
                AzureDevOpsBuildQueryOrder.StartTimeDescending => BuildQueryOrder.StartTimeDescending,
                AzureDevOpsBuildQueryOrder.StartTimeAscending => BuildQueryOrder.StartTimeAscending,
                _ => throw new System.Exception("Unknown value"),
            };
        }
    }
}
