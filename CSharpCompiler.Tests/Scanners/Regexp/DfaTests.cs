using Xunit;
using CSharpCompiler.Tests;

namespace CSharpCompiler.Scanners.Regexp.Tests
{
    public class DfaTests
    {
        private DfaBuilder _builder;

        public DfaTests()
        {
            _builder = new DfaBuilder(Dfa.Builder);
        }

        [Fact]
        public void CovertFromTest_Trivial()
        {
            Nfa nfa = Nfa.Builder.Parse("a");
            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
        }

        [Fact]
        public void CovertFromTest_Concat()
        {
            Nfa nfa = Nfa.Builder.Parse("ab");
            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
        }

        [Fact]
        public void CovertFromTest_Union()
        {
            Nfa nfa = Nfa.Builder.Parse("a|b");
            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
        }

        [Fact]
        public void CovertFromTest_KleeneClosure()
        {
            Nfa nfa = Nfa.Builder.Parse("a*");
            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
        }

        [Fact]
        public void CovertFromTest_Complex()
        {
            Nfa nfa = Nfa.Builder.Parse("a(b|c)*");
            Dfa dfa = Dfa.Builder.CovertFrom(nfa);
        }
    }
}