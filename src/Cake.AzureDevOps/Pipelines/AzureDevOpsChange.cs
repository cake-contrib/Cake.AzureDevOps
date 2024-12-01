// ReSharper disable UnusedAutoPropertyAccessor.Global
namespace Cake.AzureDevOps.Pipelines
{
    using System;

    /// <summary>
    /// Represents a change associated with a build.
    /// </summary>
    public class AzureDevOpsChange
    {
        /// <summary>
        /// Gets the identifier for the change.
        /// For a commit, this would be the SHA1.
        /// For a TFVC changeset, this would be the changeset ID.
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// Gets the description of the change.
        /// This might be a commit message or changeset description.
        /// </summary>
        public string Message { get; internal set; }

        /// <summary>
        /// Gets the type of change. "commit", "changeset", etc.
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the author of the change.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Gets the timestamp for the change.
        /// </summary>
        public DateTime? Timestamp { get; internal set; }

        /// <summary>
        /// Gets the location of the full representation of the resource.
        /// </summary>
        public Uri Location { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether the message was truncated.
        /// </summary>
        public bool MessageTruncated { get; internal set; }

        /// <summary>
        /// Gets the location of a user-friendly representation of the resource.
        /// </summary>
        public Uri DisplayUri { get; internal set; }

        /// <summary>
        /// Gets the person or process that pushed the change.
        /// </summary>
        public string Pusher { get; internal set; }
    }
}