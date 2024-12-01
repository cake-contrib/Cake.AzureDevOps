namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Class providing credentials for different authentication schemas.
    /// </summary>
    internal static class AuthenticationProvider
    {
        /// <summary>
        /// Returns credentials for integrated / NTLM authentication.
        /// Can only be used for on-premise Azure DevOps Server.
        /// </summary>
        /// <returns>Credentials for integrated / NTLM authentication.</returns>
        public static IAzureDevOpsCredentials AuthenticationNtlm()
        {
            return new AzureDevOpsNtlmCredentials();
        }

        /// <summary>
        /// Returns credentials for basic authentication.
        /// Can only be used for on-premise Azure DevOps Server configured for basic authentication.
        /// See https://www.visualstudio.com/en-us/docs/integrate/get-started/auth/tfs-basic-auth.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for basic authentication.</returns>
        public static IAzureDevOpsCredentials AuthenticationBasic(
            string userName,
            string password)
        {
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return new AzureDevOpsBasicCredentials(userName, password);
        }

        /// <summary>
        /// Returns credentials for authentication with a personal access token.
        /// Can be used for Azure DevOps and Azure DevOps Server.
        /// </summary>
        /// <param name="personalAccessToken">Personal access token.</param>
        /// <returns>Credentials for authentication with a personal access token.</returns>
        public static IAzureDevOpsCredentials AuthenticationPersonalAccessToken(
            string personalAccessToken)
        {
            personalAccessToken.NotNullOrWhiteSpace(nameof(personalAccessToken));

            return new AzureDevOpsBasicCredentials(string.Empty, personalAccessToken);
        }

        /// <summary>
        /// Returns credentials for OAuth authentication.
        /// Can only be used with Azure DevOps.
        /// </summary>
        /// <param name="accessToken">OAuth access token.</param>
        /// <returns>Credentials for OAuth authentication.</returns>
        public static IAzureDevOpsCredentials AuthenticationOAuth(
            string accessToken)
        {
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            return new AzureDevOpsOAuthCredentials(accessToken);
        }

        /// <summary>
        /// Returns credentials for authentication with an Azure Active Directory.
        /// Can only be used with Azure DevOps.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for authentication with an Azure Active Directory.</returns>
        public static IAzureDevOpsCredentials AuthenticationAzureActiveDirectory(
            string userName,
            string password)
        {
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return new AzureDevOpsAadCredentials(userName, password);
        }
    }
}