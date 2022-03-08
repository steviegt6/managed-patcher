@echo off

echo Restoring git submodules
git submodule update --init --recursive

echo Building ManagedPatcher
dotnet build ./src/ManagedPatcher.sln --configuration Debug

echo Running ManagedPatcher
dotnet ./src/ManagedPatcher/bin/Debug/net6.0/ManagedPatcher.dll setup
