namespace Cake.Tfs.Authentication
{
    /// <summary>
    /// Class providing credentials for different authentication schemas.
    /// </summary>
    internal static class AuthenticationProvider
    {
        /// <summary>
        /// Returns credentials for integrated / NTLM authentication.
        /// Can only be used for on-premise Team Foundation Server.
        /// </summary>
        /// <returns>Credentials for integrated / NTLM authentication.</returns>
        public static ITfsCredentials AuthenticationNtlm()
        {
            return new TfsNtlmCredentials();
        }

        /// <summary>
        /// Returns credentials for basic authentication.
        /// Can only be used for on-premise Team Foundation Server configured for basic authentication.
        /// See https://www.visualstudio.com/en-us/docs/integrate/get-started/auth/tfs-basic-auth.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for basic authentication.</returns>
        public static ITfsCredentials AuthenticationBasic(
            string userName,
            string password)
        {
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return new TfsBasicCredentials(userName, password);
        }

        /// <summary>
        /// Returns credentials for authentication with a personal access token.
        /// Can be used for Team Foundation Server and Visual Studio Team Services.
        /// </summary>
        /// <param name="personalAccessToken">Personal access token.</param>
        /// <returns>Credentials for authentication with a personal access token.</returns>
        public static ITfsCredentials AuthenticationPersonalAccessToken(
            string personalAccessToken)
        {
            personalAccessToken.NotNullOrWhiteSpace(nameof(personalAccessToken));

            return new TfsBasicCredentials(string.Empty, personalAccessToken);
        }

        /// <summary>
        /// Returns credentials for OAuth authentication.
        /// Can only be used with Visual Studio Team Services.
        /// </summary>
        /// <param name="accessToken">OAuth access token.</param>
        /// <returns>Credentials for OAuth authentication.</returns>
        public static ITfsCredentials AuthenticationOAuth(
            string accessToken)
        {
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            return new TfsOAuthCredentials(accessToken);
        }

        /// <summary>
        /// Returns credentials for authentication with an Azure Active Directory.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        /// <returns>Credentials for authentication with an Azure Active Directory.</returns>
        public static ITfsCredentials AuthenticationAzureActiveDirectory(
            string userName,
            string password)
        {
            userName.NotNullOrWhiteSpace(nameof(userName));
            password.NotNullOrWhiteSpace(nameof(password));

            return new TfsAadCredentials(userName, password);
        }
    }
}
