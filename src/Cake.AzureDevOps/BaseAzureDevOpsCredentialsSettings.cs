namespace Cake.AzureDevOps
{
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Base settings for aliases.
    /// </summary>
    public abstract class BaseAzureDevOpsCredentialsSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsCredentialsSettings"/> class.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsCredentialsSettings(IAzureDevOpsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            this.Credentials = credentials;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsCredentialsSettings"/> class
        /// based on the instance of a <see cref="BaseAzureDevOpsCredentialsSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        protected BaseAzureDevOpsCredentialsSettings(BaseAzureDevOpsCredentialsSettings settings)
        {
            settings.NotNull(nameof(settings));

            this.Credentials = settings.Credentials;
        }

        /// <summary>
        /// Gets the credentials used to authenticate against Azure DevOps.
        /// </summary>
        public IAzureDevOpsCredentials Credentials { get; }
    }
}
