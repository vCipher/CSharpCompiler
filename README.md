# CSharpCompiler

Simple c# compiler for education purpose. It targets to [.NET Core](https://docs.microsoft.com/en-us/dotnet/articles/core/) platform.

Supported:
* arithmetic expressions
* `writeLine(...)` console output
* a script like file structure (without `using`, `namespace`, `class` or method declarations)

Work in progress:
* `for` loop
* method invokation with name and signature resolving
* file structure with `using`, `namespace`, `class`, and method declaration

# Usage
Compiler testing example:
* `cd CSharpCompiler.Tests`
* `dotnet test`

Compiler cli building example:
* `cd CSharpCompiler.Cmd`
* `dotnet build`
* `cd bin/BNB/Debug/netcoreapp1.1`
* `dotnet sample netcoreapp1.1` (where: `sample` - a source file name, `netcoreapp1.1` - a target platform)
* `dotnet sample.dll`