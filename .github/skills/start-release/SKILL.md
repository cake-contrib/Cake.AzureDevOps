---
name: start-release
description: Start a Cake.AzureDevOps release by creating a release branch from develop, preparing the GitHub release draft, and updating the NuGet release notes links. Use when asked to start or prepare a release.
---

Use this skill to start a stable Cake.AzureDevOps release. The project follows Git Flow and uses Cake.Recipe to generate GitHub release notes from a GitHub milestone.

See [the release process](../../../CONTRIBUTING.md#release) and the [Cake.Recipe release documentation](https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release).

## Inputs

* The release version in `X.Y.Z` format (for example `6.0.0`). If it was not provided, list open milestones and ask which version to release. Replace each `X.Y.Z` placeholder in commands below with that version:

  ```bash
  gh api repos/cake-contrib/Cake.AzureDevOps/milestones --jq '.[] | select(.state == "open") | "\(.title) (open: \(.open_issues), closed: \(.closed_issues))"'
  ```

## Steps

### 1. Verify prerequisites

* Confirm the working tree is clean (`git status --porcelain` returns nothing). Stop if it is not; do not stash or discard user changes.
* Confirm `gh auth status` succeeds and the authenticated user can push to `cake-contrib/Cake.AzureDevOps` and create releases.
* Check `git remote -v`. Use the remote whose fetch and push URLs point to `cake-contrib/Cake.AzureDevOps` (usually `upstream` in a fork). Stop if no writable canonical remote is configured; do not push a release branch to a fork by mistake.
* Confirm that an open GitHub milestone named exactly `X.Y.Z` exists:

  ```bash
  gh api repos/cake-contrib/Cake.AzureDevOps/milestones --jq '.[] | select(.title == "X.Y.Z" and .state == "open")'
  gh issue list --repo cake-contrib/Cake.AzureDevOps --milestone X.Y.Z --state open --limit 1000
  gh pr list --repo cake-contrib/Cake.AzureDevOps --search "milestone:X.Y.Z" --state open --limit 1000
  ```

  Report open milestone items and ask whether to proceed. Check that release changes have the appropriate labels; `Build`-only changes are excluded from release notes by [GitReleaseManager.yaml](../../../GitReleaseManager.yaml).
* Ensure `release/X.Y.Z` does not already exist locally or on the canonical remote. Never overwrite or reuse an existing release branch. Check whether a draft release for `X.Y.Z` already exists; if it does, inspect it and ask before regenerating or changing it.

### 2. Create the release branch

Fetch the canonical remote and branch from its latest `develop` without setting an upstream tracking branch. Replace `upstream` below with the verified remote name if needed:

```bash
git fetch upstream --prune
git switch --no-track -c release/X.Y.Z upstream/develop
```

### 3. Create and review the release draft

Cake.Recipe uses `GITHUB_PAT` to create a draft release from the milestone. Set it from the authenticated GitHub CLI token and run the target for the current platform:

```powershell
$env:GITHUB_PAT = gh auth token
.\build.ps1 --target=releasenotes
```

```bash
GITHUB_PAT=$(gh auth token) ./build.sh --target=releasenotes
```

Check that the GitHub release draft for `X.Y.Z` exists and has not been published:

```bash
gh release view X.Y.Z --repo cake-contrib/Cake.AzureDevOps --json isDraft,targetCommitish,url,body
```

Review the generated notes against the milestone and make any necessary manual corrections to the draft. If draft creation fails, report the error and stop before pushing; do not publish the release.

### 4. Update NuGet release-note links

Use the [update-release-notes-link](../update-release-notes-link/SKILL.md) skill for version `X.Y.Z`. Commit this change separately:

```bash
git add nuspec/nuget/*.nuspec
git commit -m "Update release notes link"
```

### 5. Verify and push

* Confirm the release branch contains only the intended release-preparation commit(s) on top of `upstream/develop` and the draft points to the correct version.
* Push only after asking the user for confirmation:

  ```bash
  git push -u upstream release/X.Y.Z
  ```

* After pushing, check the branch's CI/build status in the repository's configured providers (Azure Pipelines and AppVeyor). Report failures or pending builds; do not claim the release branch is ready until successful.

## Next steps

Tell the user to review the release draft and branch build. Release fixes should be associated with the `X.Y.Z` milestone and have appropriate labels. When the release is ready, use the [finish-release](../finish-release/SKILL.md) skill. Publishing the draft is a separate final step after the release branch has been merged.
