param(
    [string]$PackageDirectory = (Join-Path $PSScriptRoot '..\BuildArtifacts\Packages\NuGet')
)

$ErrorActionPreference = 'Stop'
Add-Type -AssemblyName System.Reflection.Metadata
Add-Type -AssemblyName System.IO.Compression.FileSystem

$repository = Split-Path $PSScriptRoot -Parent
[xml]$project = Get-Content (Join-Path $repository 'src\Cake.AzureDevOps\Cake.AzureDevOps.csproj')
$frameworks = $project.Project.PropertyGroup.TargetFrameworks | Where-Object { $_ } | ForEach-Object { $_.Split(';') }
$hostAssemblies = @('Cake.Core.dll', 'Cake.Common.dll', 'Newtonsoft.Json.dll')
$packages = @(Get-ChildItem $PackageDirectory -Filter 'Cake.AzureDevOps.*.nupkg' | Where-Object { $_.Name -notlike '*.symbols.nupkg' })
if ($packages.Count -ne 1) {
    throw "Expected one script-runner package in $PackageDirectory, found $($packages.Count)."
}

$archive = [IO.Compression.ZipFile]::OpenRead($packages[0].FullName)
try {
    foreach ($framework in $frameworks) {
        $output = Join-Path $repository "src\Cake.AzureDevOps\bin\Release\$framework"
        if (-not (Test-Path $output -PathType Container)) {
            throw "Missing Release build output for $framework at $output."
        }
        $entries = @{}
        foreach ($entry in $archive.Entries) {
            if ($entry.FullName -like "lib/$framework/*.dll" -and $entry.FullName.Split('/').Count -eq 3) {
                $entries[$entry.Name] = $entry
            }
        }
        if (-not $entries.ContainsKey('Cake.AzureDevOps.dll')) {
            throw "Missing Cake.AzureDevOps.dll in the $framework package assets."
        }

        foreach ($entry in $entries.Values) {
            $stream = $entry.Open()
            $memory = [IO.MemoryStream]::new()
            try {
                $stream.CopyTo($memory)
                $memory.Position = 0
                $pe = [System.Reflection.PortableExecutable.PEReader]::new($memory)
                try {
                    $metadata = [System.Reflection.Metadata.PEReaderExtensions]::GetMetadataReader($pe)
                    foreach ($handle in $metadata.AssemblyReferences) {
                        $reference = $metadata.GetAssemblyReference($handle)
                        $name = $metadata.GetString($reference.Name) + '.dll'
                        if ($name -notin $hostAssemblies -and (Test-Path (Join-Path $output $name)) -and -not $entries.ContainsKey($name)) {
                            throw "$framework/$($entry.Name) requires $name, but it is missing from the script-runner package."
                        }
                    }
                }
                finally {
                    $pe.Dispose()
                }
            }
            finally {
                $stream.Dispose()
                $memory.Dispose()
            }
        }
        Write-Host "Verified script-runner runtime dependencies for $framework."
    }
}
finally {
    $archive.Dispose()
}
