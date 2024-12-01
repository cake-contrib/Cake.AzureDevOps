namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Possible results of a build.
    /// </summary>
    public enum AzureDevOpsBuildResult : byte
    {
        /// <summary>
        /// Result is not set.
        /// </summary>
        None = 0,

        /// <summary>
        /// The build completed successfully.
        /// </summary>
        Succeeded = 2,

        /// <summary>
        /// The build completed compilation successfully but had other errors.
        /// </summary>
        PartiallySucceeded = 4,

        /// <summary>
        /// The build completed unsuccessfully.
        /// </summary>
        Failed = 8,

        /// <summary>
        /// The build was canceled before starting.
        /// </summary>
        Canceled = 32,
    }
}