# Contribution Guidelines

This repository uses [GitFlow] with default configuration. Development happens on the `develop` branch.

To contribute:

* Fork this repository.
* Create a feature branch from `develop`.
* Implement your changes.
* Push your feature branch.
* Create a pull request.

## Build

To build this package we are using Cake.

On Windows PowerShell run:

```powershell
./build
```

On OSX/Linux run:

```bash
./build.sh
```

## Release

Releases are created from `release/X.Y.Z` branches using [Cake.Recipe](https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release). The release process is captured in the agent skills under `.github/skills`:

1. Create a GitHub milestone named for the release version, with all relevant issues and pull requests assigned appropriate labels.
2. Ask the agent to [start a release](.github/skills/start-release/SKILL.md). It creates the branch from `develop`, generates a GitHub release draft from the milestone, and updates the `<releaseNotes>` links in both NuGet package specifications using the [update-release-notes-link](.github/skills/update-release-notes-link/SKILL.md) skill.
3. Review the draft and ensure release branch CI passes. Associate any release fixes with the milestone.
4. Ask the agent to [finish the release](.github/skills/finish-release/SKILL.md). It verifies the milestone and draft, then merges the release branch into `master` and `develop` after confirmation.
5. Publish the release draft on the [GitHub releases page](https://github.com/cake-contrib/Cake.AzureDevOps/releases) with `master` as the target. Publishing creates the tag and triggers package publishing; do not create the tag separately.

The skills automate the release steps but do not publish the release. They assume GitHub CLI (`gh`) is installed and authenticated, and that the operator has permission to push the release, `master`, and `develop` branches.

[GitFlow]: https://nvie.com/posts/a-successful-git-branching-model/
