// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Cake.AzureDevOps.Repos
{
    using System.Collections.Generic;

    /// <summary>
    /// Description of a commit.
    /// </summary>
    public class AzureDevOpsCommit
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsCommit"/> class.
        /// </summary>
        internal AzureDevOpsCommit()
        {
        }

        /// <summary>
        /// Gets the ID (SHA-1) of the commit.
        /// </summary>
        public string Id { get; internal init; }

        /// <summary>
        /// Gets the Comment or message of the commit.
        /// </summary>
        public string Message { get; internal init; }

        /// <summary>
        /// Gets a value indicating whether the comment is truncated from the full Git
        /// commit comment message or not.
        /// </summary>
        public bool IsMessageTruncated { get; internal init; }

        /// <summary>
        /// Gets the parent commit IDs of the commit.
        /// </summary>
        public IEnumerable<string> ParentIds { get; internal init; }

        /// <summary>
        /// Gets the remote URL path to the commit.
        /// </summary>
        public string RemoteUrl { get; internal init; }
    }
}
