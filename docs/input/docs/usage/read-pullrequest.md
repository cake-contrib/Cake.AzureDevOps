---
Order: 20
Title: Reading pull requests
Description: Example how to read pull request information using the Cake.AzureDevOps addin.
---
The [Cake.AzureDevOps addin] provides an alias for reading pull request information.

The following example will read details of a pull request:

```csharp
#addin "Cake.AzureDevOps"

Task("Read-PullRequest").Does(() =>
{
    var pullRequestSettings =
        new AzureDevOpsPullRequestSettings(
            new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            AzureDevOpsAuthenticationNtlm());

    var pullRequest =
        AzureDevOpsPullRequest(
            pullRequestSettings);

    Information(pullRequest.TargetRefName);
});
```

[Cake.AzureDevOps addin]: https://www.nuget.org/packages/Cake.TfAzureDevOpss