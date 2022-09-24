using Cake.Common.Build;
using Cake.Common.Diagnostics;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Frosting;
using Cake.AzureDevOps;

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
[IsDependentOn(typeof(ReadPullRequestTask))]
public class DefaultTask : FrostingTask
{
}
