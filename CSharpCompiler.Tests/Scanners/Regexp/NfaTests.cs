using Xunit;
using CSharpCompiler.Tests;

namespace CSharpCompiler.Scanners.Regexp.Tests
{
    public class NfaTests
    {
        private NfaBuilder _builder;

        public NfaTests()
        {
            _builder = new NfaBuilder(Nfa.Builder);
        }

        [Fact]
        public void TrivialTest()
        {
            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, tail, 'a');

            Nfa expected = new Nfa(head, tail);
            Nfa actual = Nfa.Builder.Create('a');

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
        
        [Fact]
        public void ConcatTest()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');

            NfaState b_head = _builder.CreateState();
            NfaState b_tail = _builder.CreateState();
            _builder.CreateTransition(b_head, b_tail, 'b');
            _builder.CreateTransition(a_tail, b_head);
            
            NfaBuilder builder = Nfa.Builder;
            Nfa expected = new Nfa(a_head, b_tail);
            Nfa actual = builder.Concat(
                builder.Create('a'), 
                builder.Create('b')
            );

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
        
        [Fact]
        public void UnionTest()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');

            NfaState b_head = _builder.CreateState();
            NfaState b_tail = _builder.CreateState();
            _builder.CreateTransition(b_head, b_tail, 'b');

            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, a_head);
            _builder.CreateTransition(head, b_head);
            _builder.CreateTransition(a_tail, tail);
            _builder.CreateTransition(b_tail, tail);

            NfaBuilder builder = Nfa.Builder;
            Nfa expected = new Nfa(head, tail);
            Nfa actual = builder.Union(
                builder.Create('a'),
                builder.Create('b')
            );

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void KleeneClosureTest()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');
            _builder.CreateTransition(a_tail, a_head);

            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, a_head);
            _builder.CreateTransition(a_tail, tail);
            _builder.CreateTransition(head, tail);

            NfaBuilder builder = Nfa.Builder;
            Nfa expected = new Nfa(head, tail);
            Nfa actual = builder.KleeneClosure(builder.Create('a'));

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Trivial()
        {
            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, tail, 'a');

            Nfa expected = new Nfa(head, tail);
            Nfa actual = Nfa.Builder.Parse("a");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Concat()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');

            NfaState b_head = _builder.CreateState();
            NfaState b_tail = _builder.CreateState();
            _builder.CreateTransition(b_head, b_tail, 'b');
            _builder.CreateTransition(a_tail, b_head);

            Nfa expected = new Nfa(a_head, b_tail);
            Nfa actual = Nfa.Builder.Parse("ab");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Union()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');

            NfaState b_head = _builder.CreateState();
            NfaState b_tail = _builder.CreateState();
            _builder.CreateTransition(b_head, b_tail, 'b');

            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, a_head);
            _builder.CreateTransition(head, b_head);
            _builder.CreateTransition(a_tail, tail);
            _builder.CreateTransition(b_tail, tail);

            Nfa expected = new Nfa(head, tail);
            Nfa actual = Nfa.Builder.Parse("a|b");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_KleeneClosure()
        {
            NfaState a_head = _builder.CreateState();
            NfaState a_tail = _builder.CreateState();
            _builder.CreateTransition(a_head, a_tail, 'a');
            _builder.CreateTransition(a_tail, a_head);

            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, a_head);
            _builder.CreateTransition(a_tail, tail);
            _builder.CreateTransition(head, tail);

            Nfa expected = new Nfa(head, tail);
            Nfa actual = Nfa.Builder.Parse("a*");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
        
        [Fact]
        public void ParseTest_EscapedChar()
        {
            NfaState head = _builder.CreateState();
            NfaState tail = _builder.CreateState();
            _builder.CreateTransition(head, tail, '|');

            Nfa expected = new Nfa(head, tail);
            Nfa actual = Nfa.Builder.Parse("\\|");

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ParseTest_Complex()
        {
            Nfa expected = _builder.Concat(
                _builder.Create('a'),
                _builder.KleeneClosure(
                    _builder.Union(
                        _builder.Create('a'),
                        _builder.Create('b')
                    )
                )
            );
            
            Nfa actual = Nfa.Builder.Parse("a(a|b)*");
            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }
    }
}