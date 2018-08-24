---
Order: 30
Title: Voting for pull requests
Description: Example how to approve or vote for pull requests using the Cake.Tfs addin.
---
The [Cake.Tfs addin] provides an alias for approving or voting on pull requests.

The following example will approve a pull request on a Team Foundation Server:

```csharp
#addin "Cake.Tfs"

Task("Vote-PullRequest").Does(() =>
{
    var pullRequestSettings =
        new TfsPullRequestSettings(
            new Uri("http://myserver:8080/tfs/defaultcollection/myproject/_git/myrepository"),
            "refs/heads/feature/myfeature",
            TfsAuthenticationNtlm());

    TfsVotePullRequest(
        pullRequestSettings,
        TfsPullRequestVote.Approved);
});
```

[Cake.Tfs addin]: https://www.nuget.org/packages/Cake.Tfs