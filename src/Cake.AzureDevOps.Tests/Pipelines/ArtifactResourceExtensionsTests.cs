namespace Cake.AzureDevOps.Tests.Pipelines
{
    using System.Collections.Generic;
    using System.Linq;
    using Cake.AzureDevOps.Pipelines;
    using Cake.Common.Build.AzurePipelines.Data;
    using Microsoft.TeamFoundation.Build.WebApi;
    using Shouldly;
    using Xunit;

    // ReSharper disable once ClassNeverInstantiated.Global
    public sealed class ArtifactResourceExtensionsTests
    {
        public sealed class TheToAzureDevOpsArtifactResourceExtensionMethod
        {
            [Fact]
            public void Should_Return_The_Correct_AzureDevOpsArtifactResource_For_ArtifactResource()
            {
                // Given
                var artifactResource = new ArtifactResource()
                {
                    Data = "data",
                    DownloadUrl = "downloadUrl",
                    Url = "url",
                    Type = "FilePath",
                    Properties = new Dictionary<string, string>()
                    {
                        { "foo", "bar" },
                    },
                };

                // When
                var result = artifactResource.ToAzureDevOpsArtifactResource();

                // Then
                result.Data.ShouldBe(artifactResource.Data);
                result.DownloadUrl.ShouldBe(artifactResource.DownloadUrl);
                result.Url.ShouldBe(artifactResource.Url);
                result.Type.ShouldBe(AzurePipelinesArtifactType.FilePath);
                result.Properties.Count.ShouldBe(1);
                result.Properties.Single().Key.ShouldBe(artifactResource.Properties.Single().Key);
                result.Properties.Single().Value.ShouldBe(artifactResource.Properties.Single().Value);
            }

            [Theory]
            [InlineData("container", AzurePipelinesArtifactType.Container)]
            [InlineData("Container", AzurePipelinesArtifactType.Container)]
            [InlineData("FilePath", AzurePipelinesArtifactType.FilePath)]
            [InlineData("filepath", AzurePipelinesArtifactType.FilePath)]
            [InlineData("GitRef", AzurePipelinesArtifactType.GitRef)]
            [InlineData("gitref", AzurePipelinesArtifactType.GitRef)]
            [InlineData("TFVCLabel", AzurePipelinesArtifactType.TFVCLabel)]
            [InlineData("tfvclabel", AzurePipelinesArtifactType.TFVCLabel)]
            [InlineData("VersionControl", AzurePipelinesArtifactType.VersionControl)]
            [InlineData("versioncontrol", AzurePipelinesArtifactType.VersionControl)]

            public void Should_Return_The_Correct_AzureDevOpsArtifactResource_Type_EnumValue_Independent_The_ArtifactResource_Type_String_Casing(string typeString, AzurePipelinesArtifactType expectedResult)
            {
                // Given
                var artifactResource = new ArtifactResource()
                {
                    Type = typeString,
                };

                // When
                var result = artifactResource.ToAzureDevOpsArtifactResource();

                // Then
                result.Type.ShouldBe(expectedResult);
            }
        }
    }
}
