using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Semantics.Metadata.Tests
{
    public sealed class TypeDefinitionTests : TestCase
    {
        public TypeDefinitionTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void GetMethodTest()
        {
            var assembly = AssemblyFactory.GetAssemblyDefinition(KnownAssembly.System_CorLib);
            var type = assembly.Module.GetType("Int32", "System");

            type.Should().BeNotNull();
            type.GetMethod("ToString", CallingConventions.HasThis).Should().BeNotNull();
            type.GetMethod("ToString", CallingConventions.HasThis, KnownType.String).Should().BeNotNull();
        }
    }
}
