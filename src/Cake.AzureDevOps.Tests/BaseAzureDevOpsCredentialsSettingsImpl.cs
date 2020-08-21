namespace Cake.AzureDevOps.Tests
{
    using Cake.AzureDevOps.Authentication;

    public class BaseAzureDevOpsCredentialsSettingsImpl : BaseAzureDevOpsCredentialsSettings
    {
        public BaseAzureDevOpsCredentialsSettingsImpl(IAzureDevOpsCredentials credentials)
            : base(credentials)
        {
        }

        public BaseAzureDevOpsCredentialsSettingsImpl(BaseAzureDevOpsCredentialsSettingsImpl settings)
            : base(settings)
        {
        }
    }
}
