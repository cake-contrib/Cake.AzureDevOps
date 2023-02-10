namespace Cake.AzureDevOps.Tests
{
    using System;
    using Cake.AzureDevOps.Authentication;

    public class BaseAzureDevOpsProjectSettingsImpl : BaseAzureDevOpsProjectSettings
    {
        public BaseAzureDevOpsProjectSettingsImpl(Uri collectionUrl, Guid projectGuid, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectGuid, credentials)
        {
        }

        public BaseAzureDevOpsProjectSettingsImpl(Uri collectionUrl, string projectName, IAzureDevOpsCredentials credentials)
            : base(collectionUrl, projectName, credentials)
        {
        }

        public BaseAzureDevOpsProjectSettingsImpl(BaseAzureDevOpsProjectSettings settings)
            : base(settings)
        {
        }

        public BaseAzureDevOpsProjectSettingsImpl(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }
    }
}
