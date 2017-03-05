using Xunit;
using CSharpCompiler.Tests;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class NfaTests
    {
        [Fact]
        public void ParseTest_Trivial()
        {
            Nfa expected = Nfa.Create('a');
            Nfa actual = Nfa.Parse("a");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Concat()
        {
            Nfa expected = Nfa.Create('a').Concat(builder => builder.Create('b'));
            Nfa actual = Nfa.Parse("ab");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Union()
        {
            Nfa expected = Nfa.Create('a').Union(builder => builder.Create('b'));
            Nfa actual = Nfa.Parse("a|b");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_KleeneClosure()
        {
            Nfa expected = Nfa.Create('a').KleeneClosure();
            Nfa actual = Nfa.Parse("a*");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
        
        [Fact]
        public void ParseTest_EscapedChar()
        {
            Nfa expected = Nfa.Create('|');
            Nfa actual = Nfa.Parse("\\|");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
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

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
    }
}