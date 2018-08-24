---
Order: 20
Title: Reading pull requests
Description: Example how to read pull request information using the Cake.Tfs addin.
---
The [Cake.Tfs addin] provides an alias for reading pull request information.

The following example will approve a pull request on a Team Foundation Server:

```csharp
#addin "Cake.Tfs"

Task("Read-PullRequest").Does(() =>
{
    var pullRequestSettings =
        new TfsPullRequestSettings(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm());

    var pullRequest =
        TfsPullRequest(
            pullRequestSettings,
            TfsPullRequestVote.Approved);

    Information(pullRequest.TargetRefName);
});
```

[Cake.Tfs addin]: https://www.nuget.org/packages/Cake.Tfs