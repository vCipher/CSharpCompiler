using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Scanners.Regexp
{
    /// <summary>
    /// Nondeterministic Finite Automata
    /// </summary>
    public sealed class Nfa
    {
        public static readonly NfaBuilder Builder = new NfaBuilder();

        public NfaState Head { get; set; }

        public NfaState Tail { get; set; }

        public Nfa(NfaState head, NfaState tail)
        {
            Head = head;
            Tail = tail;
        }
    }
}
