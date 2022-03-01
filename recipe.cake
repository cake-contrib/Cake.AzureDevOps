#load nuget:?package=Cake.Recipe&version=2.2.1

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.AzureDevOps",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.AzureDevOps",
    appVeyorAccountName: "cakecontrib",
    shouldCalculateVersion: true,
    shouldRunDupFinder: false, // dupFinder is missing in 2021.3.0-eap
    shouldRunDotNetCorePack: true,
    shouldGenerateDocumentation: false, // Fails to restore tool on AppVeyor
    shouldRunCoveralls: false, // Fails to restore tool on AppVeyor
    shouldRunCodecov: false); // Fails to restore tool on AppVeyor

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    dupFinderExcludePattern: new string[] { BuildParameters.RootDirectoryPath + "/src/Cake.AzureDevOps.Tests/**/*.cs" },
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Common]* -[*.Tests]* -[Cake.Testing]* -[Moq]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

// Workaround until https://github.com/cake-contrib/Cake.Recipe/issues/862 has been fixed in Cake.Recipe
ToolSettings.SetToolPreprocessorDirectives(
    reSharperTools: "#tool nuget:?package=JetBrains.ReSharper.CommandLineTools&version=2021.3.1",
    gitVersionGlobalTool: "#tool dotnet:?package=GitVersion.Tool&version=5.8.1");

Build.RunDotNetCore();
