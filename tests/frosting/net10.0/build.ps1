$ADDIN_PACKAGE_PATH = "packages/cake.azuredevops"
if (Test-Path $ADDIN_PACKAGE_PATH)
{
    Write-Host "Cleaning up cached version of $ADDIN_PACKAGE_PATH..."
    Remove-Item $ADDIN_PACKAGE_PATH -Recurse;
}
else
{
    Write-Host "$ADDIN_PACKAGE_PATH not cached..."
}

dotnet run --project build/Build.csproj -- $args
exit $LASTEXITCODE;