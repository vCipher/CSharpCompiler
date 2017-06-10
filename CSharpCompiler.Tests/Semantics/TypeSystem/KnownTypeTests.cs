using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using Xunit;
using Xunit.Abstractions;

namespace CSharpCompiler.Semantics.TypeSystem.Tests
{
    public sealed class KnownTypeTests : TestCase
    {
        public KnownTypeTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void ElementTypeTest()
        {
            KnownType.Object.ElementType.Should().Be(ElementType.Object);
            KnownType.Boolean.ElementType.Should().Be(ElementType.Boolean);
            KnownType.Char.ElementType.Should().Be(ElementType.Char);
            KnownType.SByte.ElementType.Should().Be(ElementType.UInt8);
            KnownType.Byte.ElementType.Should().Be(ElementType.Int8);
            KnownType.UInt16.ElementType.Should().Be(ElementType.UInt16);
            KnownType.Int16.ElementType.Should().Be(ElementType.Int16);
            KnownType.UInt32.ElementType.Should().Be(ElementType.UInt32);
            KnownType.Int32.ElementType.Should().Be(ElementType.Int32);
            KnownType.UInt64.ElementType.Should().Be(ElementType.UInt64);
            KnownType.Int64.ElementType.Should().Be(ElementType.Int64);
            KnownType.UIntPtr.ElementType.Should().Be(ElementType.UIntPtr);
            KnownType.IntPtr.ElementType.Should().Be(ElementType.IntPtr);
            KnownType.Single.ElementType.Should().Be(ElementType.Single);
            KnownType.Double.ElementType.Should().Be(ElementType.Double);
            KnownType.Decimal.ElementType.Should().Be(ElementType.ValueType);
            KnownType.String.ElementType.Should().Be(ElementType.String);
            KnownType.Void.ElementType.Should().Be(ElementType.Void);
        }
    }
}
