namespace Cake.AzureDevOps
{
    using Cake.AzureDevOps.Authentication;
    using Cake.Core;
    using Cake.Core.Annotations;

    /// <content>
    /// Contains functionality related to authenticating to Azure DevOps.
    /// </content>
    public static partial class AzureDevOpsAliases
    {
        /// <summary>
        /// Returns credentials for integrated / NTLM authentication.
        /// Can only be used for on-premise Azure DevOps Server.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Credentials for integrated / NTLM authentication.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Authentication")]
        [CakeNamespaceImport("Cake.AzureDevOps.Authentication")]
        public static IAzureDevOpsCredentials AzureDevOpsAuthenticationNtlm(
            this ICakeContext context)
        {
            context.NotNull(nameof(context));

            return AuthenticationProvider.AuthenticationNtlm();
        }

        /// <summary>
        /// Returns credentials for basic authentication.
        /// Can only be used for on-premise Azure DevOps Server configured for basic authentication.
        /// See https://www.visualstudio.com/en-us/docs/integrate/get-started/auth/tfs-basic-auth.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for basic authentication.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Authentication")]
        [CakeNamespaceImport("Cake.AzureDevOps.Authentication")]
        public static IAzureDevOpsCredentials AzureDevOpsAuthenticationBasic(
            this ICakeContext context,
            string userName,
            string password)
        {
            context.NotNull(nameof(context));
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return AuthenticationProvider.AuthenticationBasic(userName, password);
        }

        /// <summary>
        /// Returns credentials for authentication with a personal access token.
        /// Can be used for Azure DevOps Server and Azure DevOps Service.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="personalAccessToken">Personal access token.</param>
        /// <returns>Credentials for authentication with a personal access token.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Authentication")]
        [CakeNamespaceImport("Cake.AzureDevOps.Authentication")]
        public static IAzureDevOpsCredentials AzureDevOpsAuthenticationPersonalAccessToken(
            this ICakeContext context,
            string personalAccessToken)
        {
            context.NotNull(nameof(context));
            personalAccessToken.NotNullOrWhiteSpace(nameof(personalAccessToken));

            return AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken);
        }

        /// <summary>
        /// Returns credentials for OAuth authentication.
        /// Can only be used with Azure DevOps.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="accessToken">OAuth access token.</param>
        /// <returns>Credentials for OAuth authentication.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Authentication")]
        [CakeNamespaceImport("Cake.AzureDevOps.Authentication")]
        public static IAzureDevOpsCredentials AzureDevOpsAuthenticationOAuth(
            this ICakeContext context,
            string accessToken)
        {
            context.NotNull(nameof(context));
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            return AuthenticationProvider.AuthenticationOAuth(accessToken);
        }

        /// <summary>
        /// Returns credentials for authentication with an Azure Active Directory.
        /// Can only be used with Azure DevOps.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for authentication with an Azure Active Directory.</returns>
        [CakeMethodAlias]
        [CakeAliasCategory("Authentication")]
        [CakeNamespaceImport("Cake.AzureDevOps.Authentication")]
        public static IAzureDevOpsCredentials AzureDevOpsAuthenticationAzureActiveDirectory(
            this ICakeContext context,
            string userName,
            string password)
        {
            context.NotNull(nameof(context));
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password);
        }
    }
}
