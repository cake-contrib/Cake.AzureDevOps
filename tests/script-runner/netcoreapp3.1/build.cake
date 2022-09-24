#addin nuget:?package=Cake.AzureDevOps&prerelease

//////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////

var target = Argument("target", "Default");

//////////////////////////////////////////////////
// TARGETS
//////////////////////////////////////////////////

Task("Read-Build")
    .Does(() =>
{
    var build = AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

    Information(build.BuildId);
});

Task("Read-PullRequest")
    .WithCriteria((context) => context.BuildSystem().IsPullRequest, "Only supported for pull request builds.")
    .Does(() =>
{
    // var pullRequest =
    //     AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken();

    // Information(pullRequest.TargetRefName);
});


Task("Default")
    .IsDependentOn("Read-Build")
    .IsDependentOn("Read-PullRequest");

//////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////

RunTarget(target);