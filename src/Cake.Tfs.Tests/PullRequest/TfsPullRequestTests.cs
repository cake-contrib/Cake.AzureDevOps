namespace Cake.Tfs.Tests.PullRequest
{
    using System;
    using Cake.Core.Diagnostics;
    using Cake.Testing;
    using Cake.Tfs.Authentication;
    using Cake.Tfs.PullRequest;
    using Shouldly;
    using Xunit;

    public sealed class TfsPullRequestTests
    {
        public sealed class TheCtor
        {
            [Fact]
            public void Should_Throw_If_Log_Is_Null()
            {
                // Given
                ICakeLog log = null;
                var settings = new TfsPullRequestSettings(new Uri("http://example.com"), "foo", AuthenticationProvider.AuthenticationNtlm());

                // When
                var result = Record.Exception(() => new TfsPullRequest(log, settings));

                // Then
                result.IsArgumentNullException("log");
            }

            [Fact]
            public void Should_Throw_If_Settings_Are_Null()
            {
                // Given
                var log = new FakeLog();
                TfsPullRequestSettings settings = null;

                // When
                var result = Record.Exception(() => new TfsPullRequest(log, settings));

                // Then
                result.IsArgumentNullException("settings");
            }
        }
    }
}
