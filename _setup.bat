@echo off

echo Restoring git submodules
git submodule update --init --recursive

echo Building ManagedPatcher
dotnet build ./src/ManagedPatcher.sln --configuration Debug

dotnet ./src/ManagedPatcher/bin/Debug/net6.0/ManagedPatcher.dll decompile
dotnet ./src/ManagedPatcher/bin/Debug/net6.0/ManagedPatcher.dll patch --input "Patch Patched"
dotnet ./src/ManagedPatcher/bin/Debug/net6.0/ManagedPatcher.dll patch --input "Patch Mod"
