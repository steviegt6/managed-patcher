# ManagedPatcher
A helpful tool for patching files, specifically designed for modifying C# applications (not limited to C# applications, though).

This is a template repository containing the _ManagedPatcher_ source code and an example implementation for applying patches to `vanilla-patched` and `modded`.

## Purpose
_ManagedPatcher_ serves as a (mostly) easy way to patch files and distribute patches without revealing most of the file being patched. This is very similar to git's concept of diffs and patches, but doesn't rely on git for creating and applying patches. This is adapted from (_tModLoader_)[https://github.com/tModLoader/tModLoader]'s patching system.

## Understanding the Patch Cycle
When you first clone a project using _ManagedPatcher_, you shouldn't have access to any of the source files. You will either need to supply these yourself or, if the project permits it, enter a patch to a C# assembly (this is the only C#-specific part of the program) which may be automatically decompiled.

_ManagedPatcher_ programs should provide a `setup` script which will automatically run all patch tasks.

Please take a look at `example.config.json` to understand the configuration and diff/patch tasks.

When applying a patch, the source directory is deleted and replaced with the base directory (the third item in the patch task array), which then has its patches applied.

Diffing just compares two directories and spits them out to a directory housing the generated patches.
