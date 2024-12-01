#load nuget:https://pkgs.dev.azure.com/cake-contrib/Home/_packaging/addins/nuget/v3/index.json?package=Cake.Recipe&version=4.0.0-alpha0126

Environment.SetVariableNames();

BuildParameters.SetParameters(
    context: Context,
    buildSystem: BuildSystem,
    sourceDirectoryPath: "./src",
    title: "Cake.AzureDevOps",
    repositoryOwner: "cake-contrib",
    repositoryName: "Cake.AzureDevOps",
    appVeyorAccountName: "cakecontrib",
    shouldUseDeterministicBuilds: true,
    shouldGenerateDocumentation: false, // Fails to restore tool on AppVeyor
    shouldRunCoveralls: false, // Fails to restore tool on AppVeyor
    shouldRunCodecov: false); // Fails to restore tool on AppVeyor

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Common]* -[*.Tests]* -[Cake.Testing]* -[Moq]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

Build.RunDotNetCore();
