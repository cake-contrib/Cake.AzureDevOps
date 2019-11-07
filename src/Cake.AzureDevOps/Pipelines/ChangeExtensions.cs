namespace Cake.AzureDevOps.Pipelines
{
    using Microsoft.TeamFoundation.Build.WebApi;

    /// <summary>
    /// Extensions for the <see cref="Change"/> class.
    /// </summary>
    internal static class ChangeExtensions
    {
        /// <summary>
        /// Converts a <see cref="Change"/> to an <see cref="AzureDevOpsChange"/>.
        /// </summary>
        /// <param name="change">Change to convert.</param>
        /// <returns>Converted change.</returns>
        public static AzureDevOpsChange ToAzureDevOpsChange(this Change change)
        {
            change.NotNull(nameof(change));

            return
                new AzureDevOpsChange
                {
                    Author = change.Author?.DisplayName,
                    DisplayUri = change.DisplayUri,
                    Id = change.Id,
                    Location = change.Location,
                    Message = change.Message,
                    MessageTruncated = change.MessageTruncated,
                    Pusher = change.Pusher,
                    Timestamp = change.Timestamp,
                    Type = change.Type,
                };
        }
    }
}
