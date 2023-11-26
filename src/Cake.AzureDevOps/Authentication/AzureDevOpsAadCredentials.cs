namespace Cake.AzureDevOps.Authentication
{
    /// <summary>
    /// Credentials for authentication with an Azure Active Directory.
    /// </summary>
    /// <param name="userName">User name.</param>
    /// <param name="password">Password.</param>
    public class AzureDevOpsAadCredentials(string userName, string password) : AzureDevOpsBasicCredentials(userName, password)
    {
    }
}
