namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Credentials for basic authentication.
    /// </summary>
    public class AzureDevOpsBasicCredentials : IAzureDevOpsCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AzureDevOpsBasicCredentials"/> class.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public AzureDevOpsBasicCredentials(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName { get; private set; }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; private set; }
    }
}
