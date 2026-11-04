# Contribution Guidelines

This repository uses [GitFlow] with default configuration.
Development is happening on `develop` branch.

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

Packages are created from `nuspec/nuget/*.nuspec`, not by `dotnet pack`.
When changing dependencies, update the script-runner bundled files and the Frosting dependency groups.
The build pipeline runs `tests/Verify-Packages.ps1` to check that the script-runner package includes its runtime dependency chain.

## Release

See [Cake.Recipe documentation] how to create a new release of this addin.

[GitFlow]: (http://nvie.com/posts/a-successful-git-branching-model/)
[Cake.Recipe documentation]: https://cake-contrib.github.io/Cake.Recipe/docs/usage/creating-release