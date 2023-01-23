namespace Cake.AzureDevOps.Pipelines
{
    /// <summary>
    /// Represents a test result associated with a <see cref="AzureDevOpsTestRun" />.
    /// </summary>
    public class AzureDevOpsTestResult
    {
        /// <summary>
        /// Gets the automated test name.
        /// </summary>
        public string AutomatedTestName { get; internal set; }

        /// <summary>
        /// Gets the outcome of the test.
        /// </summary>
        public string Outcome { get; internal set; }

        /// <summary>
        /// Gets the message of the test.
        /// </summary>
        public string ErrorMessage { get; internal set; }
    }
}
