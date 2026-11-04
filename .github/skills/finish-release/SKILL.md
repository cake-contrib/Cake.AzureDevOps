---
name: finish-release
description: Verify and finish a Cake.AzureDevOps release by checking the milestone and GitHub release notes, merging the release branch into master and develop, and removing the release branch. Use when asked to finish a release.
---

Use this skill to finish a release prepared with [start-release](../start-release/SKILL.md). It follows the Git Flow release process and Cake.Recipe release guidance.

See [the release process](../../../CONTRIBUTING.md#release) and the [Cake.Recipe release documentation](https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release).

This skill does **not** publish the release or create a tag. Publishing the GitHub draft after the merges creates the tag and triggers package publishing.

## Input

* Release version in `X.Y.Z` format. If the current branch is `release/X.Y.Z`, use that version. Otherwise list remote `release/*` branches and ask which version to finish.

## Steps

### 1. Verify prerequisites

* Require a clean working tree. Stop on any uncommitted changes; do not stash or discard them.
* Confirm `gh auth status` succeeds and the user can push to `master` and `develop` on `cake-contrib/Cake.AzureDevOps`.
* Verify the canonical repository remote using `git remote -v` and use that remote in all commands. Stop rather than pushing to a fork or a remote with an unexpected URL.
* Fetch the canonical remote and ensure `release/X.Y.Z` exists remotely. If a local `release/X.Y.Z` branch exists, compare it with the remote using `git rev-list --left-right --count release/X.Y.Z...upstream/release/X.Y.Z`. If the counts are not `0 0`, stop and ask how to handle local-only commits or remote changes.
* Check `git worktree list` for other worktrees with `master`, `develop`, or the release branch checked out; if one has uncommitted changes or commits absent from the canonical remote, stop and resolve that first.
* Check the current CI/build status for the release branch in Azure Pipelines and AppVeyor. If a build is failing or still running, report it and ask before continuing.

### 2. Verify milestone and release notes

The release notes are generated from the `X.Y.Z` GitHub milestone by Cake.Recipe and can be manually edited in the draft. There is no separate website news post for this repository.

1. List the commits being released and inspect any changes added after the release branch was created:

   ```bash
   git --no-pager log --no-merges --format="%h %ad %s" --date=short upstream/master..upstream/release/X.Y.Z
   ```

2. Check that every release change is represented by a pull request or issue assigned to milestone `X.Y.Z` and labeled appropriately. `Build`-only changes are excluded from release notes; review user-facing changes and dependency updates against [GitReleaseManager.yaml](../../../GitReleaseManager.yaml).
3. Compare the milestone items with the release draft:

   ```bash
   gh issue list --repo cake-contrib/Cake.AzureDevOps --milestone X.Y.Z --state all --limit 1000 --json number,title,labels,state
   gh pr list --repo cake-contrib/Cake.AzureDevOps --search "milestone:X.Y.Z" --state all --limit 1000 --json number,title,labels,state
   gh release view X.Y.Z --repo cake-contrib/Cake.AzureDevOps --json isDraft,targetCommitish,url,body
   ```

4. Confirm all milestone issues and pull requests are closed, the draft exists and is still a draft, all user-facing changes are described, and contributors are credited. Check any referenced pull request or closing issue for its milestone and labels.
5. Report missing milestone assignments, open milestone items, and changes absent from the draft. If anything needs correction, stop and ask the user how to proceed. If the draft must be regenerated, confirm first because this may replace manual edits. Make and push fixes only on a clean checkout of `release/X.Y.Z`, wait for CI, then repeat this verification.

### 3. Merge release into master

Ask the user for confirmation before merging and pushing. Use the canonical remote name instead of `upstream` if different. A detached checkout avoids switching a branch that may be active in another worktree:

```bash
git fetch upstream
git switch --detach upstream/master
git merge --no-ff upstream/release/X.Y.Z -m "Merge branch 'release/X.Y.Z'"
git push upstream HEAD:master
```

If the merge conflicts, stop and ask the user; do not resolve release conflicts by guessing.

### 4. Merge master into develop

After the release merge is pushed, fetch again and ask the user to confirm this push:

```bash
git fetch upstream
git switch --detach upstream/develop
git merge --no-ff upstream/master -m "Merge branch 'master' into develop"
git push upstream HEAD:develop
```

Stop and ask the user if there are conflicts.

### 5. Delete the release branch

Ask for confirmation before deleting the remote branch. Check `git worktree list` first; do not delete a local branch checked out in another worktree. After confirming the release merge exists on both `master` and `develop`, delete the remote branch and, only if safe, the local branch:

```bash
git push upstream --delete release/X.Y.Z
git branch -d release/X.Y.Z
```

## Next step

Tell the user to publish the `X.Y.Z` draft on the [GitHub releases page](https://github.com/cake-contrib/Cake.AzureDevOps/releases), targeting `master`. Do not publish it, create a tag, or trigger package publishing as part of this skill.
