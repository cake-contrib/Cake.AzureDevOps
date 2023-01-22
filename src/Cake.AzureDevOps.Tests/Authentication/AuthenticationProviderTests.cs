namespace Cake.AzureDevOps.Tests.Authentication
{
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    public sealed class AuthenticationProviderTests
    {
        public sealed class TheAuthenticationNtlmMethod
        {
            [Fact]
            public void Should_Return_AzureDevOpsNtlmCredentials_Object()
            {
                // Given / When
                var credentials = AuthenticationProvider.AuthenticationNtlm();

                // Then
                credentials.ShouldBeOfType<AzureDevOpsNtlmCredentials>();
            }
        }

        public sealed class TheAuthenticationBasicMethod
        {
            [Fact]
            public void Should_Throw_If_User_Name_Is_Null()
            {
                // Given
                const string userName = null;
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentNullException("userName");
            }

            [Fact]
            public void Should_Throw_If_User_Name_Is_Empty()
            {
                // Given
                var userName = string.Empty;
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("userName");
            }

            [Fact]
            public void Should_Throw_If_User_Name_Is_WhiteSpace()
            {
                // Given
                const string userName = " ";
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("userName");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_Null()
            {
                // Given
                const string userName = "foo";
                const string password = null;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentNullException("password");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_Empty()
            {
                // Given
                const string userName = "foo";
                var password = string.Empty;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("password");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_WhiteSpace()
            {
                // Given
                const string userName = "foo";
                const string password = " ";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationBasic(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("password");
            }

            [Fact]
            public void Should_Return_AzureDevOpsBasicCredentials_Object()
            {
                // Given
                const string userName = "foo";
                const string password = "bar";

                // When
                var credentials = AuthenticationProvider.AuthenticationBasic(userName, password);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsBasicCredentials>();
            }

            [Fact]
            public void Should_Set_User_Name()
            {
                // Given
                const string userName = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationBasic(userName, "bar");

                // Then
                credentials.ShouldBeOfType<AzureDevOpsBasicCredentials>();
                ((AzureDevOpsBasicCredentials)credentials).UserName.ShouldBe(userName);
            }

            [Fact]
            public void Should_Set_Password()
            {
                // Given
                const string password = "bar";

                // When
                var credentials = AuthenticationProvider.AuthenticationBasic("foo", password);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsBasicCredentials>();
                ((AzureDevOpsBasicCredentials)credentials).Password.ShouldBe(password);
            }
        }

        public sealed class TheAuthenticationPersonalAccessTokenMethod
        {
            [Fact]
            public void Should_Throw_If_Personal_Access_Token_Is_Null()
            {
                // Given
                const string personalAccessToken = null;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken));

                // Then
                result.IsArgumentNullException("personalAccessToken");
            }

            [Fact]
            public void Should_Throw_If_Personal_Access_Token_Is_Empty()
            {
                // Given
                var personalAccessToken = string.Empty;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken));

                // Then
                result.IsArgumentOutOfRangeException("personalAccessToken");
            }

            [Fact]
            public void Should_Throw_If_Personal_Access_Token_Is_WhiteSpace()
            {
                // Given
                const string personalAccessToken = " ";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken));

                // Then
                result.IsArgumentOutOfRangeException("personalAccessToken");
            }

            [Fact]
            public void Should_Return_AzureDevOpsBasicCredentials_Object()
            {
                // Given
                const string personalAccessToken = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsBasicCredentials>();
            }

            [Fact]
            public void Should_Set_Personal_Access_Token()
            {
                // Given
                const string personalAccessToken = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationPersonalAccessToken(personalAccessToken);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsBasicCredentials>();
                ((AzureDevOpsBasicCredentials)credentials).UserName.ShouldBe(string.Empty);
                ((AzureDevOpsBasicCredentials)credentials).Password.ShouldBe(personalAccessToken);
            }
        }

        public sealed class TheAuthenticationOAuthMethod
        {
            [Fact]
            public void Should_Throw_If_Access_Token_Is_Null()
            {
                // Given
                const string accessToken = null;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationOAuth(accessToken));

                // Then
                result.IsArgumentNullException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_Empty()
            {
                // Given
                var accessToken = string.Empty;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationOAuth(accessToken));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Throw_If_Access_Token_Is_WhiteSpace()
            {
                // Given
                const string accessToken = " ";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationOAuth(accessToken));

                // Then
                result.IsArgumentOutOfRangeException("accessToken");
            }

            [Fact]
            public void Should_Return_AzureDevOpsOAuthCredentials_Object()
            {
                // Given
                const string accessToken = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationOAuth(accessToken);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsOAuthCredentials>();
            }

            [Fact]
            public void Should_Set_Access_Token()
            {
                // Given
                const string accessToken = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationOAuth(accessToken);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsOAuthCredentials>();
                ((AzureDevOpsOAuthCredentials)credentials).AccessToken.ShouldBe(accessToken);
            }
        }

        public sealed class TheAuthenticationAzureActiveDirectoryMethod
        {
            [Fact]
            public void Should_Throw_If_User_Name_Is_Null()
            {
                // Given
                const string userName = null;
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentNullException("userName");
            }

            [Fact]
            public void Should_Throw_If_User_Name_Is_Empty()
            {
                // Given
                var userName = string.Empty;
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("userName");
            }

            [Fact]
            public void Should_Throw_If_User_Name_Is_WhiteSpace()
            {
                // Given
                const string userName = " ";
                const string password = "foo";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("userName");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_Null()
            {
                // Given
                const string userName = "foo";
                const string password = null;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentNullException("password");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_Empty()
            {
                // Given
                const string userName = "foo";
                var password = string.Empty;

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("password");
            }

            [Fact]
            public void Should_Throw_If_Password_Is_WhiteSpace()
            {
                // Given
                const string userName = "foo";
                const string password = " ";

                // When
                var result = Record.Exception(() => AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password));

                // Then
                result.IsArgumentOutOfRangeException("password");
            }

            [Fact]
            public void Should_Return_AzureDevOpsAadCredentials_Object()
            {
                // Given
                const string userName = "foo";
                const string password = "bar";

                // When
                var credentials = AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, password);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsAadCredentials>();
            }

            [Fact]
            public void Should_Set_User_Name()
            {
                // Given
                const string userName = "foo";

                // When
                var credentials = AuthenticationProvider.AuthenticationAzureActiveDirectory(userName, "bar");

                // Then
                credentials.ShouldBeOfType<AzureDevOpsAadCredentials>();
                ((AzureDevOpsAadCredentials)credentials).UserName.ShouldBe(userName);
            }

            [Fact]
            public void Should_Set_Password()
            {
                // Given
                const string password = "bar";

                // When
                var credentials = AuthenticationProvider.AuthenticationAzureActiveDirectory("foo", password);

                // Then
                credentials.ShouldBeOfType<AzureDevOpsAadCredentials>();
                ((AzureDevOpsAadCredentials)credentials).Password.ShouldBe(password);
            }
        }
    }
}
