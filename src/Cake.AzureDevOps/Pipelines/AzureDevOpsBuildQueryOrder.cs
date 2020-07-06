namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Possible order of a build query.
    /// </summary>
    public enum AzureDevOpsBuildQueryOrder : byte
    {
        /// <summary>
        /// Order by finish time ascending.
        /// </summary>
        FinishTimeAscending = 2,

        /// <summary>
        /// Order by finish time descending.
        /// </summary>
        FinishTimeDescending = 3,

        /// <summary>
        /// Order by queue time descending.
        /// </summary>
        QueueTimeDescending = 4,

        /// <summary>
        /// Order by queue time ascending.
        /// </summary>
        QueueTimeAscending = 5,

        /// <summary>
        /// Order by start time descending.
        /// </summary>
        StartTimeDescending = 6,

        /// <summary>
        /// Order by start time ascending.
        /// </summary>
        StartTimeAscending = 7,
    }
}
