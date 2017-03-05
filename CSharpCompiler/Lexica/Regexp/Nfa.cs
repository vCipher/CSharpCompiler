using System;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class Nfa : FiniteAutomata<NfaState>
    {
        private NfaBuilder _builder;

        public NfaState Tail { get; set; }

        internal Nfa(NfaState head, NfaState tail, NfaBuilder builder) : base(head)
        {
            _builder = builder;
            Tail = tail;
        }

        public static Nfa Parse(string regexp)
        {
            return new NfaBuilder().Parse(regexp);
        }

        public static Nfa Create(char character)
        {
            return new NfaBuilder().Create(character);
        }

        public Nfa Concat(Nfa other)
        {
            return _builder.Concat(this, other);
        }

        public Nfa Concat(Func<NfaBuilder, Nfa> build)
        {
            return _builder.Concat(this, build(_builder));
        }

        public Nfa Union(Nfa other)
        {
            return _builder.Union(this, other);
        }

        public Nfa Union(Func<NfaBuilder, Nfa> build)
        {
            return _builder.Union(this, build(_builder));
        }

        public Nfa KleeneClosure()
        {
            return _builder.KleeneClosure(this);
        }

        public Nfa OneOrMore()
        {
            return _builder.OneOrMore(this);
        }

        public Nfa OneOrNothing()
        {
            return _builder.OneOrNothing(this);
        }
    }
}
