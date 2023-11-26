namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Credentials for basic authentication.
    /// </summary>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    public class AzureDevOpsBasicCredentials(string userName, string password) : IAzureDevOpsCredentials
    {
        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string UserName { get; } = userName;

        /// <summary>
        /// Gets the password.
        /// </summary>
        public string Password { get; } = password;
    }
}
