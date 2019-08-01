namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Credentials for OAuth authentication.
    /// </summary>
    public class AzureDevOpsOAuthCredentials : IAzureDevOpsCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsOAuthCredentials"/> class.
        /// </summary>
        /// <param name="accessToken">OAuth access token.</param>
        public AzureDevOpsOAuthCredentials(string accessToken)
        {
            accessToken.NotNullOrWhiteSpace(nameof(accessToken));

            this.AccessToken = accessToken;
        }

        /// <summary>
        /// Gets the OAuth access token.
        /// </summary>
        public string AccessToken { get; private set; }
    }
}
