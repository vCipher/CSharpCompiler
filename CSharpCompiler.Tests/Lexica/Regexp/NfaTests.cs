using Xunit;
using CSharpCompiler.Tests;
using Xunit.Abstractions;
using CSharpCompiler.Tests.Assertions;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class NfaTests : TestCase
    {
        public NfaTests(ITestOutputHelper output) : base(output)
        { }

        [Fact]
        public void ParseTest_Trivial()
        {
            Nfa expected = Nfa.Create('a');
            Nfa actual = Nfa.Parse("a");

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void ParseTest_Concat()
        {
            Nfa expected = Nfa.Create('a').Concat(builder => builder.Create('b'));
            Nfa actual = Nfa.Parse("ab");

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void ParseTest_Union()
        {
            Nfa expected = Nfa.Create('a').Union(builder => builder.Create('b'));
            Nfa actual = Nfa.Parse("a|b");

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void ParseTest_KleeneClosure()
        {
            Nfa expected = Nfa.Create('a').KleeneClosure();
            Nfa actual = Nfa.Parse("a*");

            actual.Should().Be(expected, Output);
        }
        
        [Fact]
        public void ParseTest_EscapedChar()
        {
            Nfa expected = Nfa.Create('|');
            Nfa actual = Nfa.Parse("\\|");

            actual.Should().Be(expected, Output);
        }

        [Fact]
        public void ParseTest_Complex()
        {
            Nfa expected = Nfa.Create('a').Concat(builder =>
                builder.Create('a')
                    .Union(inner => inner.Create('b'))
                    .KleeneClosure()
            );
            Nfa actual = Nfa.Parse("a(a|b)*");

            actual.Should().Be(expected, Output);
        }
    }
}