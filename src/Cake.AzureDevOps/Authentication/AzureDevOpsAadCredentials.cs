namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Credentials for authentication with an Azure Active Directory.
    /// </summary>
    public class AzureDevOpsAadCredentials : AzureDevOpsBasicCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsAadCredentials"/> class.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public AzureDevOpsAadCredentials(string userName, string password)
            : base(userName, password)
        {
        }
    }
}
