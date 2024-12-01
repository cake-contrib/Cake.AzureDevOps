namespace Cake.AzureDevOps.Tests.Authentication
{
    using Cake.AzureDevOps.Authentication;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class AzureDevOpsBasicCredentialsTests
    {
        public sealed class TheCtor
        {
            [Theory]
            [InlineData("foo")]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Set_User_Name(string userName)
            {
                // When
                var credentials = new AzureDevOpsBasicCredentials(userName, "bar");

                // Then
                credentials.UserName.ShouldBe(userName);
            }

            [Theory]
            [InlineData("bar")]
            [InlineData(null)]
            [InlineData("")]
            [InlineData(" ")]
            public void Should_Set_Password_Name(string password)
            {
                // When
                var credentials = new AzureDevOpsBasicCredentials("foo", password);

                // Then
                credentials.Password.ShouldBe(password);
            }
        }
    }
}