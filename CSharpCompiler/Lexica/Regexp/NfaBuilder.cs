using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Lexica.Regexp
{
    public sealed class NfaBuilder
    {
        private const string NUMBER_PATTERN = "0|1|2|3|4|5|6|7|8|9";
        private const string WORD_PATTERN = "a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|_";
        private const string WHITE_SPACE_PATTERN = " |\t";
        private const string ANY_CHAR_PATTERN = "\\w|\\d|\\s|!|\"|#|$|%|&|\\(|\\)|\\*|\\+|,|\\-|.|/|:|;|<|=|>|\\?|@|[|\\\\|]|^|_|`|{|\\||}|~";

        private readonly Dictionary<char, Func<CharEnumerator, Nfa, Nfa>> _mappers;
        private readonly Dictionary<char, Func<Nfa>> _macros;
        private int _stateCounter;
        
        public NfaBuilder()
        {
            _stateCounter = 0;
            _mappers = new Dictionary<char, Func<CharEnumerator, Nfa, Nfa>>();
            _mappers.Add(')', (enumerator, nfa) => { throw new ScanException("Imbalance parenthesis"); });
            _mappers.Add('(', (enumerator, nfa) => Unwrap(enumerator, nfa));
            _mappers.Add('*', (enumerator, nfa) => KleeneClosure(nfa));
            _mappers.Add('\\', (enumerator, nfa) => Macros(enumerator, nfa));
            _mappers.Add('|', (enumerator, nfa) => Union(enumerator, nfa));
            _mappers.Add('+', (enumerator, nfa) => OneOrMore(nfa));
            _mappers.Add('?', (enumerator, nfa) => OneOrNothing(nfa));

            _macros = new Dictionary<char, Func<Nfa>>();
            _macros.Add('d', () => Parse(NUMBER_PATTERN));
            _macros.Add('w', () => Parse(WORD_PATTERN));
            _macros.Add('s', () => Parse(WHITE_SPACE_PATTERN));
            _macros.Add('.', () => Parse(ANY_CHAR_PATTERN));
        }

        public Nfa Parse(string regexp)
        {
            return Parse(new CharEnumerator(regexp));
        }
        
        private Nfa Parse(CharEnumerator enumerator)
        {
            Nfa nfa = null;
            while (enumerator.MoveNext())
                nfa = Parse(enumerator, nfa);

            return nfa;
        }

        private Nfa Parse(CharEnumerator enumerator, Nfa nfa = null)
        {
            Func<CharEnumerator, Nfa, Nfa> mapper;
            if (!_mappers.TryGetValue(enumerator.Current, out mapper))
                return Concat(nfa, CheckForClosure(enumerator, Create(enumerator.Current)));

            return mapper(enumerator, nfa);
        }

        public NfaState CreateState()
        {
            return new NfaState(_stateCounter++);
        }

        public Nfa Create(char character)
        {
            Transition<NfaState> transition = Transition.Create(
                CreateState(),
                CreateState(), 
                character);
            return new Nfa(transition.From, transition.To, this);
        }

        public Nfa Create(NfaState head, NfaState tail)
        {
            return new Nfa(head, tail, this);
        }

        public Nfa Union(Nfa fst, Nfa snd)
        {
            if (fst == null) return snd;
            if (snd == null) return fst;

            NfaState head = CreateState();
            NfaState tail = CreateState();

            Transition.Create(head, fst.Head);
            Transition.Create(head, snd.Head);
            Transition.Create(fst.Tail, tail);
            Transition.Create(snd.Tail, tail);

            return Create(head, tail);
        }

        public Nfa Concat(Nfa fst, Nfa snd)
        {
            if (fst == null) return snd;
            if (snd == null) return fst;

            Transition.Create(fst.Tail, snd.Head);
            return Create(fst.Head, snd.Tail);
        }

        public Nfa OneOrNothing(Nfa nfa)
        {
            if (nfa == null)
                throw new ArgumentNullException("Operator [?] can't be applied without prefix", "nfa");

            NfaState head = CreateState();
            NfaState tail = CreateState();

            Transition.Create(head, nfa.Head);
            Transition.Create(nfa.Tail, tail);
            Transition.Create(head, tail);

            return Create(head, tail);
        }

        public Nfa KleeneClosure(Nfa nfa)
        {
            if (nfa == null)
                throw new ArgumentNullException("Operator [*] can't be applied without prefix", "nfa");

            NfaState head = CreateState();
            NfaState tail = CreateState();

            Transition.Create(nfa.Tail, nfa.Head);
            Transition.Create(head, nfa.Head);
            Transition.Create(nfa.Tail, tail);
            Transition.Create(head, tail);

            return Create(head, tail);
        }

        public Nfa OneOrMore(Nfa nfa)
        {
            NfaState head = CreateState();
            NfaState tail = CreateState();

            Transition.Create(nfa.Tail, nfa.Head);
            Transition.Create(head, nfa.Head);
            Transition.Create(nfa.Tail, tail);

            return Create(head, tail);
        }

        private Nfa Unwrap(CharEnumerator enumerator, Nfa nfa)
        {
            StringBuilder sb = new StringBuilder();
            for (int parenthesisCounter = 1; ; sb.Append(enumerator.Current))
            {
                if (!enumerator.MoveNext())
                    throw new ScanException();

                if (enumerator.Current == '(') parenthesisCounter++;
                if (enumerator.Current == ')') parenthesisCounter--;
                if (parenthesisCounter == 0) break;
            }

            Nfa innerNfa = Parse(sb.ToString());
            return Concat(nfa, CheckForClosure(enumerator, innerNfa));
        }

        private Nfa Union(CharEnumerator enumerator, Nfa nfa)
        {
            if (!enumerator.MoveNext() || enumerator.Current == '|')
                throw new ScanException("Unexpected end of input stream");

            return Union(nfa, Parse(enumerator, null));
        }

        private Nfa Macros(CharEnumerator enumerator, Nfa nfa)
        {
            if (!enumerator.MoveNext())
                throw new ScanException("Unexpected end of input stream");

            Func<Nfa> macros;
            if (_macros.TryGetValue(enumerator.Current, out macros))
                return Concat(nfa, CheckForClosure(enumerator, macros()));

            Nfa escapeCharNfa = Concat(nfa, CheckForClosure(enumerator, Create(enumerator.Current)));
            return escapeCharNfa;
        }

        private Nfa CheckForClosure(CharEnumerator enumerator, Nfa nfa)
        {
            if (!enumerator.MoveNext())
                return nfa;

            switch (enumerator.Current)
            {
                case '*': return KleeneClosure(nfa);
                case '+': return OneOrMore(nfa);
                case '?': return OneOrNothing(nfa);
            }

            enumerator.MovePrevious();
            return nfa;
        }
    }
}
