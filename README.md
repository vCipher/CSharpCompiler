# CSharpCompiler

Simple c# compiler for education purpose. It targets to [.NET Core](https://docs.microsoft.com/en-us/dotnet/articles/core/) platform.

Supported:
* expressions
* for loop
* arrays
* `System.Console.WriteLine(...)` console output
* method invokation with name and signature resolving
* a script like file structure (without `using`, `namespace`, `class` or method declarations)

Work in progress:
* file structure with `using`, `namespace`, `class`, and method declaration

# Usage
Compiler testing example:
* `cd CSharpCompiler.Tests`
* `dotnet test`

Compiler cli building example:
* `cd CSharpCompiler.Cmd`
* `dotnet build`
* `cd bin/Debug/netcoreapp1.1`
* `dotnet sample netcoreapp1.1` (where: `sample` - a source file name, `netcoreapp1.1` - a target platform)
* `dotnet sample.dll`

# Sample of code
```csharp
int length = 5;
int[] array = new int[length];
array[0] = 5;
array[1] = 2;
array[2] = 7;
array[3] = 3;
array[4] = 2;

for (int i = 0; i < length; i++)
{
    for (int j = i; j >= 1; j--)
    {
        if (array[j] < array[j - 1])
        {
            int num2 = array[j - 1];
            array[j - 1] = array[j];
            array[j] = num2;
        }
    }
}

for (int i = 0; i < length; i++)
{
    System.Console.WriteLine(array[i]);
}
```