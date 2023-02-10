# Azure DevOps support for Cake

This addin for Cake allows you to work with Azure DevOps.

[![License](http://img.shields.io/:license-mit-blue.svg)](https://github.com/cake-contrib/Cake.AzureDevOps/blob/feature/build/LICENSE)

## Information

| | Stable | Pre-release |
|:--:|:--:|:--:|
|GitHub Release|-|[![GitHub release](https://img.shields.io/github/release/cake-contrib/Cake.AzureDevOps.svg)](https://github.com/cake-contrib/Cake.AzureDevOps/releases/latest)|
|NuGet|[![NuGet](https://img.shields.io/nuget/v/Cake.AzureDevOps.svg)](https://www.nuget.org/packages/Cake.AzureDevOps)|[![NuGet](https://img.shields.io/nuget/vpre/Cake.AzureDevOps.svg)](https://www.nuget.org/packages/Cake.AzureDevOps)|

## Build & Test Status

| Type               | CI Server    | Runner                          | Operating System    | Develop | Master |
|:------------------:|:------------:|:-------------------------------:|:-------------------:|:-------:|:------:|
| Build & Unit Tests | AppVeyor     | N/A                             | Windows Server 2019 |[![Build status](https://ci.appveyor.com/api/projects/status/45blf3csh70opuos/branch/develop?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-azuredevops/branch/develop)|[![Build status](https://ci.appveyor.com/api/projects/status/45blf3csh70opuos/branch/master?svg=true)](https://ci.appveyor.com/project/cakecontrib/cake-azuredevops/branch/master)|
| Build & Unit Tests | Azure DevOps | N/A                             | Windows Server 2022 |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Build%20%26%20Test%20Windows)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Build%20%26%20Test%20Windows)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Build & Unit Tests | Azure DevOps | N/A                             | macOS 11            |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Build%20%26%20Test%20macOS)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Build%20%26%20Test%20macOS)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Build & Unit Tests | Azure DevOps | N/A                             | Ubuntu 20.04        |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Build%20%26%20Test%20Ubuntu)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Build%20%26%20Test%20Ubuntu)](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 6.0       | Windows Server 2022 |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20Windows%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20Windows%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 7.0       | Windows Server 2022 |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20Windows%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20Windows%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 6.0      | Windows Server 2022 |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20Windows%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20Windows%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 7.0      | Windows Server 2022 |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20Windows%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20Windows%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 6.0       | macOS 11            |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20macOS%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20macOS%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 7.0       | macOS 11            |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20macOS%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20macOS%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 6.0      | macOS 11            |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20macOS%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20macOS%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 7.0      | macOS 11            |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20macOS%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20macOS%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 6.0       | Ubuntu 20.04        |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20Ubuntu%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20Ubuntu%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Frosting on .NET 7.0       | Ubuntu 20.04        |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Frosting%20Ubuntu%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Frosting%20Ubuntu%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 6.0      | Ubuntu 20.04        |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20Ubuntu%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20Ubuntu%20(.NET%206))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|
| Integration Tests  | Azure DevOps | Cake Scripting on .NET 7.0      | Ubuntu 20.04        |[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=develop&jobName=Integration%20Tests%20Script%20Runner%20Ubuntu%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=develop)|[![Build Status](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_apis/build/status/cake-contrib.Cake.AzureDevOps?branchName=master&jobName=Integration%20Tests%20Script%20Runner%20Ubuntu%20(.NET%207))](https://dev.azure.com/cake-contrib/Cake.AzureDevOps/_build/latest?definitionId=24&branchName=master)|

## Demos

Click on the build server and repository links to see the build definition or source code and try out the addin yourself.

|Build Server|Repository| |
|:--:|:--:|:--:|
|[Azure Pipelines](https://dev.azure.com/pberger/Cake.AzureDevOps-Demo/_build?definitionId=8)|[Azure Repos](https://dev.azure.com/pberger/_git/Cake.AzureDevOps-Demo)|[![Build Status](https://dev.azure.com/pberger/Cake.AzureDevOps-Demo/_apis/build/status/Cake.AzureDevOps-Demo?branchName=master)](https://dev.azure.com/pberger/Cake.AzureDevOps-Demo/_build/latest?definitionId=8&branchName=master)|

## Code Coverage

[![Coverage Status](https://coveralls.io/repos/github/cake-contrib/Cake.AzureDevOps/badge.svg?branch=develop)](https://coveralls.io/github/cake-contrib/Cake.AzureDevOps?branch=develop)

## Quick Links

- [Documentation](https://cake-contrib.github.io/Cake.AzureDevOps)

## Discussion

For questions and to discuss ideas & feature requests, use the [GitHub discussions on the Cake GitHub repository](https://github.com/cake-build/cake/discussions), under the [Extension Q&A](https://github.com/cake-build/cake/discussions/categories/extension-q-a) category.

[![Join in the discussion on the Cake repository](https://img.shields.io/badge/GitHub-Discussions-green?logo=github)](https://github.com/cake-build/cake/discussions)

## Contributing

Contributions are welcome. See [Contribution Guidelines](CONTRIBUTING.md).
