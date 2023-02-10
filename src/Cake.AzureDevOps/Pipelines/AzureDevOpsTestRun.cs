namespace Cake.AzureDevOps.Pipelines
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a test run associated with a <see cref="AzureDevOpsBuild" />.
    /// </summary>
    public class AzureDevOpsTestRun
    {
        /// <summary>
        /// Gets the name of the test run.
        /// </summary>
        public int RunId { get; internal init; }

        /// <summary>
        /// Gets the test results of a test run.
        /// </summary>
        public IEnumerable<AzureDevOpsTestResult> TestResults { get; internal init; }
    }
}
