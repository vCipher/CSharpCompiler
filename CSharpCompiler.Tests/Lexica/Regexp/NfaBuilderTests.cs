using CSharpCompiler.Tests;
using CSharpCompiler.Tests.Assertions;
using Xunit;
using Xunit.Abstractions;

using static CSharpCompiler.Tests.Helpers.NfaBuilderHelper;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class NfaBuilderTests : TestCase
    {
        public NfaBuilderTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void TrivialTest()
        {
            Nfa expected = EmulateTrivial('a');
            Nfa actual = new NfaBuilder().Create('a');

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void ConcatTest()
        {
            Nfa expected = EmulateConcat('a', 'b');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.Concat(builder.Create('a'), builder.Create('b'));

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void UnionTest()
        {
            Nfa expected = EmulateUnion('a', 'b');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.Union(builder.Create('a'), builder.Create('b'));

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void KleeneClosureTest()
        {
            Nfa expected = EmulateKleeneClosure('a');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.KleeneClosure(builder.Create('a'));

            actual.Should().Be(expected, Output);
        }
    }
}
