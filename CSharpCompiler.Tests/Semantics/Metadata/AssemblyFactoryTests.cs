using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Semantics.Metadata.Tests
{
    public sealed class AssemblyFactoryTests : TestCase
    {
        public AssemblyFactoryTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void GetAssemblyDefinitionTest()
        {
            var assemblyRef = new AssemblyReference("System.Console, Version=4.0.1.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a");
            var assemblyDef = AssemblyFactory.GetAssemblyDefinition(assemblyRef);

            assemblyDef.Name.Should().Be(assemblyRef.Name);
            assemblyDef.Version.Should().Be(assemblyRef.Version);
            assemblyDef.Culture.Should().Be(assemblyRef.Culture);
            assemblyDef.PublicKeyToken.Should().Be(assemblyRef.PublicKeyToken);

            assemblyDef.HashAlgorithm.Should().Be(AssemblyHashAlgorithm.SHA1);
            assemblyDef.EntryPoint.Should().BeNull();
            assemblyDef.Attributes.Should().Be(AssemblyAttributes.PublicKey);

            assemblyDef.Module.Name.Should().Be("System.Console.dll");
            assemblyDef.Module.Mvid.Should().Be(new Guid("4b786cd1-97ab-44f0-ac1b-00ca356783a6"));
            assemblyDef.Module.EntryPoint.Should().BeNull();
            assemblyDef.Module.Types.Count.Should().Be(39);

            assemblyDef.References.Should().Be(new[]
            {
                new AssemblyReference("System.Runtime, Version=4.0.20.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Resources.ResourceManager, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.IO, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Text.Encoding, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Threading.Tasks, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.IO.FileSystem.Primitives, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Threading, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Runtime.InteropServices, Version=4.0.20.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"),
                new AssemblyReference("System.Text.Encoding.Extensions, Version=4.0.10.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")
            }, Output, AssemblyInfoComparer.Default);
        }
    }
}
