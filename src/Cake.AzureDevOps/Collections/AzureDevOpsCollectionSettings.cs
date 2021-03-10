namespace Cake.AzureDevOps.Collections
{
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Settings for aliases handling collections.
    /// </summary>
    public class AzureDevOpsCollectionSettings : BaseAzureDevOpsCollectionSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsCollectionSettings"/> class
        /// based on the instance of a <see cref="AzureDevOpsCollectionSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        public AzureDevOpsCollectionSettings(AzureDevOpsCollectionSettings settings)
            : base(settings)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsCollectionSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        public AzureDevOpsCollectionSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }

        /// <summary>
        /// Constructs the settings object using the access token provided by Azure Pipelines.
        /// </summary>
        /// <returns>The instance of <see cref="AzureDevOpsCollectionSettings"/> class.</returns>
        public static AzureDevOpsCollectionSettings UsingAzurePipelinesOAuthToken()
        {
            var accessToken = EnvironmentVariableHelper.GetSystemAccessToken();
            return new AzureDevOpsCollectionSettings(new AzureDevOpsOAuthCredentials(accessToken));
        }
    }
}
