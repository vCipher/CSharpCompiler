namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class Dfa : FiniteAutomata<DfaState>
    {
        internal Dfa(DfaState head) : base(head)
        { }

        public static Dfa FromNfa(Nfa nfa)
        {
           return new DfaBuilder(nfa).Build();
        }
    }
}
