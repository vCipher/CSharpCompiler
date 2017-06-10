using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Semantics.Metadata.Tests
{
    public sealed class TypeReferenceTests : TestCase
    {
        public TypeReferenceTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void GetMethodTest()
        {
            KnownType.Int32.GetMethod("ToString", CallingConventions.HasThis).Should().BeNotNull();
            KnownType.Int32.GetMethod("ToString", CallingConventions.HasThis, KnownType.String).Should().BeNotNull();
        }
    }
}
