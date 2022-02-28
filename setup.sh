echo "Restoring git submodules"
git submodule update --init --recursive

echo "Building ManagedPatcher"
dotnet build ./src/ManagedPatcher.sln --configuration Debug
dotnet ./src/ManagedPatcher/bin/Debug/net6.0/ManagedPatcher.dll

echo "Running ManagedPatcher"