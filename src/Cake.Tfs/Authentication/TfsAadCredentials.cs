namespace Cake.Tfs.Authentication
{
    /// <summary>
    /// Credentials for authentication with an Azure Active Directory.
    /// </summary>
    public class TfsAadCredentials : TfsBasicCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TfsAadCredentials"/> class.
        /// </summary>
        /// <param name="userName">User name.</param>
        /// <param name="password">Password.</param>
        public TfsAadCredentials(string userName, string password)
            : base(userName, password)
        {
        }
    }
}
