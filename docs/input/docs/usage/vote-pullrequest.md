---
Order: 30
Title: Voting for pull requests
Description: Example how to approve or vote for pull requests using the Cake.AzureDevOps addin.
---
The [Cake.AzureDevOps addin] provides an alias for approving or voting on pull requests.

The following example will approve a pull request on an Azure DevOps Server:

```csharp
#addin "Cake.AzureDevOps"

Task("Vote-PullRequest").Does(() =>
{
    var pullRequestSettings =
        new AzureDevOpsPullRequestSettings(
            new Uri("http://myserver:8080/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            AzureDevOpsAuthenticationNtlm());

    AzureDevOpsVotePullRequest(
        pullRequestSettings,
        AzureDevOpsPullRequestVote.Approved);
});
```

[Cake.AzureDevOps addin]: https://www.nuget.org/packages/Cake.AzureDevOps