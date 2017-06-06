using CSharpCompiler.Lexica.Regexp;

namespace CSharpCompiler.Tests.Helpers
{
    public static class NfaBuilderHelper
    {
        public static Nfa EmulateTrivial(char character)
        {
            NfaBuilder builder = new NfaBuilder();
            NfaState head = builder.CreateState();
            NfaState tail = builder.CreateState();
            Transition.Create(head, tail, character);

            return builder.Create(head, tail);
        }

        public static Nfa EmulateConcat(char fistCharacter, char secondCharacter)
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

        public static Nfa EmulateUnion(char fistCharacter, char secondCharacter)
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

        public static Nfa EmulateKleeneClosure(char character)
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
