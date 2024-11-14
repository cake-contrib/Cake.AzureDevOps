#load nuget:?package=Cake.Recipe&version=3.1.1

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
    shouldGenerateDocumentation: false, // Fails to restore tool on AppVeyor
    shouldRunCoveralls: false, // Fails to restore tool on AppVeyor
    shouldRunCodecov: false); // Fails to restore tool on AppVeyor

BuildParameters.PrintParameters(Context);

ToolSettings.SetToolSettings(
    context: Context,
    testCoverageFilter: "+[*]* -[xunit.*]* -[Cake.Core]* -[Cake.Common]* -[*.Tests]* -[Cake.Testing]* -[Moq]* -[Shouldly]* -[DiffEngine]* -[EmptyFiles]*",
    testCoverageExcludeByAttribute: "*.ExcludeFromCodeCoverage*",
    testCoverageExcludeByFile: "*/*Designer.cs;*/*.g.cs;*/*.g.i.cs");

// Disable Upload-Coveralls-Report task since it fails to install the tool on AppVeyor
BuildParameters.Tasks.UploadCoverallsReportTask.WithCriteria(() => false);

Build.RunDotNetCore();
