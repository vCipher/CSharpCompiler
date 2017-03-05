using CSharpCompiler.Tests;
using Xunit;

namespace CSharpCompiler.Lexica.Regexp.Tests
{
    public class NfaBuilderTests
    {
        [Fact]
        public void TrivialTest()
        {
            Nfa expected = EmulateTrivial('a');
            Nfa actual = new NfaBuilder().Create('a');

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void ConcatTest()
        {
            Nfa expected = EmulateConcat('a', 'b');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.Concat(builder.Create('a'), builder.Create('b'));

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void UnionTest()
        {
            Nfa expected = EmulateUnion('a', 'b');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.Union(builder.Create('a'), builder.Create('b'));

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        [Fact]
        public void KleeneClosureTest()
        {
            Nfa expected = EmulateKleeneClosure('a');

            NfaBuilder builder = new NfaBuilder();
            Nfa actual = builder.KleeneClosure(builder.Create('a'));

            Assert.Equal(expected, actual, new JsonEqualityComparer<Nfa>());
        }

        private Nfa EmulateTrivial(char character)
        {
            NfaBuilder builder = new NfaBuilder();
            NfaState head = builder.CreateState();
            NfaState tail = builder.CreateState();
            Transition.Create(head, tail, character);

            return builder.Create(head, tail);
        }

        private Nfa EmulateConcat(char fistCharacter, char secondCharacter)
        {
            NfaBuilder builder = new NfaBuilder();
            NfaState fistHead = builder.CreateState();
            NfaState fistTail = builder.CreateState();
            Transition.Create(fistHead, fistTail, fistCharacter);

            NfaState secondHead = builder.CreateState();
            NfaState secondTail = builder.CreateState();
            Transition.Create(secondHead, secondTail, secondCharacter);
            Transition.Create(fistTail, secondHead);

            return builder.Create(fistHead, secondTail);
        }

        private Nfa EmulateUnion(char fistCharacter, char secondCharacter)
        {
            NfaBuilder builder = new NfaBuilder();
            NfaState firstHead = builder.CreateState();
            NfaState firstTail = builder.CreateState();
            Transition.Create(firstHead, firstTail, fistCharacter);

            NfaState secondHead = builder.CreateState();
            NfaState secondTail = builder.CreateState();
            Transition.Create(secondHead, secondTail, secondCharacter);

            NfaState head = builder.CreateState();
            NfaState tail = builder.CreateState();
            Transition.Create(head, firstHead);
            Transition.Create(head, secondHead);
            Transition.Create(firstTail, tail);
            Transition.Create(secondTail, tail);

            return builder.Create(head, tail);
        }

        private Nfa EmulateKleeneClosure(char character)
        {
            NfaBuilder builder = new NfaBuilder();
            NfaState a_head = builder.CreateState();
            NfaState a_tail = builder.CreateState();
            Transition.Create(a_head, a_tail, character);
            Transition.Create(a_tail, a_head);

            NfaState head = builder.CreateState();
            NfaState tail = builder.CreateState();
            Transition.Create(head, a_head);
            Transition.Create(a_tail, tail);
            Transition.Create(head, tail);

            return builder.Create(head, tail);
        }
    }
}
