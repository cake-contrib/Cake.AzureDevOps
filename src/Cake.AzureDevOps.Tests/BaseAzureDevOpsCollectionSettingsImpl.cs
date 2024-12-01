namespace Cake.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Authentication;

    public class BaseAzureDevOpsCollectionSettingsImpl : BaseAzureDevOpsCollectionSettings
    {
        public BaseAzureDevOpsCollectionSettingsImpl(Uri collectionUrl, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, credentials)
        {
        }

        public BaseAzureDevOpsCollectionSettingsImpl(BaseAzureDevOpsCollectionSettings settings)
            : base(settings)
        {
        }

        public BaseAzureDevOpsCollectionSettingsImpl(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }
    }
}