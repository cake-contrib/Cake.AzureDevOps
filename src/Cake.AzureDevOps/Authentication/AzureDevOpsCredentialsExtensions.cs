namespace Cake.AzureDevOps.Authentication
{
    using Microsoft.VisualStudio.Services.Client;
    using Microsoft.VisualStudio.Services.Common;
    using Microsoft.VisualStudio.Services.OAuth;

    /// <summary>
    /// Extensions for the <see cref="IAzureDevOpsCredentials"/> interface.
    /// </summary>
    internal static class AzureDevOpsCredentialsExtensions
    {
        /// <summary>
        /// Returns the <see cref="VssCredentials"/> corresponding to a <see cref="IAzureDevOpsCredentials"/> object.
        /// </summary>
        /// <param name="credentials"><see cref="IAzureDevOpsCredentials"/> credential instance.</param>
        /// <returns><see cref="VssCredentials"/> instance.</returns>
        public static VssCredentials ToVssCredentials(this IAzureDevOpsCredentials credentials)
        {
            credentials.NotNull(nameof(credentials));

            switch (credentials.GetType().Name)
            {
                case nameof(AzureDevOpsNtlmCredentials):
                    return new VssCredentials();

                case nameof(AzureDevOpsBasicCredentials):
                    var basicCredentials = (AzureDevOpsBasicCredentials)credentials;
                    return new VssBasicCredential(basicCredentials.UserName, basicCredentials.Password);

                case nameof(AzureDevOpsOAuthCredentials):
                    var oAuthCredentials = (AzureDevOpsOAuthCredentials)credentials;
                    return new VssOAuthAccessTokenCredential(oAuthCredentials.AccessToken);

                case nameof(AzureDevOpsAadCredentials):
                    var aadCredentials = (AzureDevOpsAadCredentials)credentials;
                    return new VssAadCredential(aadCredentials.UserName, aadCredentials.Password);

                default:
                    throw new AzureDevOpsException("Unknown credential type.");
            }
        }
    }
}
