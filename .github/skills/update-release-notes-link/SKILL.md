---
name: update-release-notes-link
description: Update the GitHub release URL in every Cake.AzureDevOps NuGet nuspec to a specified release version. Use when preparing a release branch or asked to update package release-note links.
---

Update the `<releaseNotes>` element in every NuGet package specification to point to the GitHub release for the version being prepared.

## Input

* Release version in `X.Y.Z` format. If the current branch is named `release/X.Y.Z`, use that version; otherwise ask for the version.

## Files

Update every `.nuspec` file in [nuspec/nuget](../../../nuspec/nuget). Both `Cake.AzureDevOps.nuspec` and `Cake.Frosting.AzureDevOps.nuspec` must point to:

```xml
<releaseNotes>https://github.com/cake-contrib/Cake.AzureDevOps/releases/tag/X.Y.Z</releaseNotes>
```

Only change the version at the end of that URL. Do not edit package versions or unrelated release metadata.

For PowerShell, preserve each file's existing UTF-8 BOM status:

```powershell
$version = "X.Y.Z"
Get-ChildItem nuspec\nuget\*.nuspec | ForEach-Object {
    $bytes = [System.IO.File]::ReadAllBytes($_.FullName)
    $hasBom = $bytes.Length -ge 3 -and $bytes[0] -eq 0xEF -and $bytes[1] -eq 0xBB -and $bytes[2] -eq 0xBF
    $content = [System.IO.File]::ReadAllText($_.FullName)
    $expectedUrl = "https://github.com/cake-contrib/Cake.AzureDevOps/releases/tag/$version"
    if ($content.Contains("<releaseNotes>$expectedUrl</releaseNotes>")) { return }
    $updated = $content -replace '(<releaseNotes>https://github\.com/cake-contrib/Cake\.AzureDevOps/releases/tag/)[^<]+(</releaseNotes>)', "`${1}$version`${2}"
    if ($updated -eq $content) { throw "Expected releaseNotes URL not found in $($_.Name)" }
    [System.IO.File]::WriteAllText($_.FullName, $updated, [System.Text.UTF8Encoding]::new($hasBom))
}
```

For Bash:

```bash
version="X.Y.Z"
sed -i.bak -E "s#(<releaseNotes>https://github\.com/cake-contrib/Cake\.AzureDevOps/releases/tag/)[^<]+(</releaseNotes>)#\1${version}\2#" nuspec/nuget/*.nuspec && rm -- nuspec/nuget/Cake.AzureDevOps.nuspec.bak nuspec/nuget/Cake.Frosting.AzureDevOps.nuspec.bak
```

## Validation

* Verify every `.nuspec` file contains exactly the expected release URL, and no `.nuspec` still points at the previous version.
* Review `git diff` to ensure only the `<releaseNotes>` values changed. Preserve existing line endings and encoding.
* When used during `start-release`, commit the change separately as `Update release notes link`.
