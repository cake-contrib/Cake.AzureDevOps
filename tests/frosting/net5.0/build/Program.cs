using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.AzureDevOps;
using System.Linq;

public static class Program
{
    public static int Main(string[] args)
    {
        return new CakeHost()
            .UseContext<BuildContext>()
            .Run(args);
    }
}

public class BuildContext : FrostingContext
{
    public BuildContext(ICakeContext context)
        : base(context)
    {
    }
}

[TaskName("Read-Build")]
public sealed class ReadBuildTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var build =
            context.AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

        context.Information(build.BuildId);
    }
}

[TaskName("Read-BuildChanges")]
public sealed class ReadBuildChangesTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var build =
            context.AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

        var changes = build.GetChanges();
        if (!changes.Any())
        {
            context.Information("No changes found.");
        }
        else
        {
            foreach (var change in changes)
            {
                context.Information("{0}: {1} by {2}", change.Id, change.Message, change.Author);
            }
        }
    }
}

[TaskName("Read-BuildTimelineRecords")]
public sealed class ReadBuildTimelineRecordsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        var build =
            context.AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

        var timelineRecords = build.GetTimelineRecords();
        if (!timelineRecords.Any())
        {
            context.Information("No timeline records found.");
        }
        else
        {
            foreach (var timelineRecord in timelineRecords)
            {
                context.Information("{0}: {1}", timelineRecord.Id, timelineRecord.Name);
            }
        }
    }
}

[TaskName("Read-BuildArtifacts")]
public sealed class ReadBuildArtifactsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // var build =
        //     context.AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

        // var artifacts = build.GetArtifacts();
        // if (!artifacts.Any())
        // {
        //     context.Information("No artifacts found.");
        // }
        // else
        // {
        //     foreach (var artifact in artifacts)
        //     {
        //         context.Information("{0}: {1}", artifact.Id, artifact.Name);
        //     }
        // }
    }
}

[TaskName("Read-BuildTestRuns")]
public sealed class ReadBuildTestRunsTask : FrostingTask<BuildContext>
{
    public override void Run(BuildContext context)
    {
        // var build =
        //     context.AzureDevOpsBuildUsingAzurePipelinesOAuthToken();

        // var testRuns = build.GetTestRuns();
        // if (!testRuns.Any())
        // {
        //     context.Information("No test runs found.");
        // }
        // else
        // {
        //     foreach (var testRun in testRuns)
        //     {
        //         context.Information("{0}", testRun.RunId);
        //     }
        // }
    }
}

[TaskName("Read-PullRequest")]
public sealed class ReadPullRequestTask : FrostingTask<BuildContext>
{
    public override bool ShouldRun(BuildContext context)
    {
        return context.BuildSystem().IsPullRequest;
    }

    public override void Run(BuildContext context)
    {
        // var pullRequest =
        //     context.AzureDevOpsPullRequestUsingAzurePipelinesOAuthToken();

        // context.Information(pullRequest.TargetRefName);
    }
}

[TaskName("Default")]
[IsDependentOn(typeof(ReadBuildTask))]
[IsDependentOn(typeof(ReadBuildChangesTask))]
[IsDependentOn(typeof(ReadBuildTimelineRecordsTask))]
[IsDependentOn(typeof(ReadBuildArtifactsTask))]
[IsDependentOn(typeof(ReadBuildTestRunsTask))]
[IsDependentOn(typeof(ReadPullRequestTask))]
public class DefaultTask : FrostingTask
{
}
