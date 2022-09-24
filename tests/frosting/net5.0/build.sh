$ADDIN_PACKAGE_PATH = "packages/cake.azuredevops"
if [ -d "$ADDIN_PACKAGE_PATH" ]
then
    echo "Cleaning up cached version of $ADDIN_PACKAGE_PATH..."
    rm -Rf $ADDIN_PACKAGE_PATH
else
    echo "$ADDIN_PACKAGE_PATH not cached..."
fi

dotnet run --project ./build/Build.csproj -- "$@"