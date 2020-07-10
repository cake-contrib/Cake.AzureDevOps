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
            switch (queryOrder)
            {
                case AzureDevOpsBuildQueryOrder.FinishTimeAscending:
                    return BuildQueryOrder.FinishTimeAscending;
                case AzureDevOpsBuildQueryOrder.FinishTimeDescending:
                    return BuildQueryOrder.FinishTimeDescending;
                case AzureDevOpsBuildQueryOrder.QueueTimeDescending:
                    return BuildQueryOrder.QueueTimeDescending;
                case AzureDevOpsBuildQueryOrder.QueueTimeAscending:
                    return BuildQueryOrder.QueueTimeAscending;
                case AzureDevOpsBuildQueryOrder.StartTimeDescending:
                    return BuildQueryOrder.StartTimeDescending;
                case AzureDevOpsBuildQueryOrder.StartTimeAscending:
                    return BuildQueryOrder.StartTimeAscending;
                default:
                    throw new System.Exception("Unknown value");
            }
        }
    }
}
