namespace Cake.AzureDevOps
{
    using System;
    using Cake.AzureDevOps.Authentication;

    /// <summary>
    /// Base settings for aliases scoped to collection level.
    /// </summary>
    public abstract class BaseAzureDevOpsCollectionSettings : BaseAzureDevOpsCredentialsSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsCollectionSettings"/> class.
        /// </summary>
        /// <param name="collectionUrl">Full URL of the Azure DevOps collection,
        /// eg. <code>http://myserver:8080/defaultcollection</code>.</param>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsCollectionSettings(Uri collectionUrl, IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            collectionUrl.NotNull(nameof(collectionUrl));

            this.CollectionUrl = collectionUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsCollectionSettings"/> class
        /// based on the instance of a <see cref="BaseAzureDevOpsCollectionSettings"/> class.
        /// </summary>
        /// <param name="settings">Settings containing the parameters.</param>
        protected BaseAzureDevOpsCollectionSettings(BaseAzureDevOpsCollectionSettings settings)
            : base(settings)
        {
            this.CollectionUrl = settings.CollectionUrl;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAzureDevOpsCollectionSettings"/> class using environment variables
        /// as set by an Azure Pipelines build.
        /// </summary>
        /// <param name="credentials">Credentials to use to authenticate against Azure DevOps.</param>
        protected BaseAzureDevOpsCollectionSettings(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
            this.CollectionUrl = EnvironmentVariableHelper.GetSystemTeamFoundationCollectionUri();
        }

        /// <summary>
        /// Gets the full URL of the Azure DevOps collection, eg. <code>http://myserver:8080/defaultcollection</code>.
        /// </summary>
        public Uri CollectionUrl { get; private set; }
    }
}
