using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Scanners.Regexp
{
    public sealed class NfaBuilder
    {
        private readonly Dictionary<char, Func<CharIterator, Nfa, Nfa>> _mappers;
        private readonly Dictionary<char, Nfa> _macros;
        private readonly NfaStateFactory _stateFactory;

        public NfaBuilder()
        {
            _stateFactory = new NfaStateFactory();
            _mappers = new Dictionary<char, Func<CharIterator, Nfa, Nfa>>();
            _mappers.Add(')', (iterator, nfa) => { throw new ScanException(); });
            _mappers.Add('(', (iterator, nfa) => Unwrap(iterator, nfa));
            _mappers.Add('*', (iterator, nfa) => KleeneClosure(iterator, nfa));
            _mappers.Add('\\', (iterator, nfa) => Macros(iterator, nfa));
            _mappers.Add('|', (iterator, nfa) => Union(iterator, nfa));
            _mappers.Add('+', (iterator, nfa) => PositiveClosure(iterator, nfa));
            _mappers.Add('?', (iterator, nfa) => SingleClosure(iterator, nfa));

            _macros = new Dictionary<char, Nfa>();
            _macros.Add('d', Parse("0|1|2|3|4|5|6|7|8|9"));
            _macros.Add('w', Parse("a|b|c|d|e|f|g|h|i|j|k|l|m|n|o|p|q|r|s|t|u|v|w|x|y|z|A|B|C|D|E|F|G|H|I|J|K|L|M|N|O|P|Q|R|S|T|U|V|W|X|Y|Z|_"));
            _macros.Add('s', Parse(" "));
            _macros.Add('.', Parse("\\w|\\d|\\s|!|\"|#|$|%|&|\\(|\\)|\\*|\\+|,|\\-|\\.|/|:|;|<|=|>|\\?|@|[|\\\\|]|^|_|`|{|\\|}|~"));
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="builder">Source NFA builder</param>
        public NfaBuilder(NfaBuilder builder)
        {
            _macros = builder._macros;
            _mappers = builder._mappers;
            _stateFactory = new NfaStateFactory(builder._stateFactory);
        }

        public Transition CreateTransition(NfaState from, NfaState to)
        {
            Transition transition = new Transition(from, to);
            from.Transitions.Add(transition);
            return transition;
        }

        public Transition CreateTransition(NfaState from, NfaState to, char character)
        {
            Transition transition = new Transition(from, to, character);
            from.Transitions.Add(transition);
            return transition;
        }

        public Nfa Parse(string regexp)
        {
            return Parse(new CharIterator(regexp));
        }

        private Nfa Parse(CharIterator iterator)
        {
            Nfa nfa = null;
            while (iterator.MoveNext())
                nfa = Parse(iterator, nfa);

            return nfa;
        }

        private Nfa Parse(CharIterator iterator, Nfa nfa = null)
        {
            Func<CharIterator, Nfa, Nfa> mapper;
            if (!_mappers.TryGetValue(iterator.Current, out mapper))
                return Concat(nfa, CheckForClosure(iterator, Create(iterator.Current)));

            return mapper(iterator, nfa);
        }

        public NfaState CreateState()
        {
            return _stateFactory.Create();
        }

        public Nfa Create(char character)
        {
            NfaState head = _stateFactory.Create();
            NfaState tail = _stateFactory.Create();

            CreateTransition(head, tail, character);
            return new Nfa(head, tail);
        }

        public Nfa Union(Nfa fst, Nfa snd)
        {
            if (fst == null) return snd;
            if (snd == null) return fst;

            NfaState head = _stateFactory.Create();
            NfaState tail = _stateFactory.Create();

            CreateTransition(head, fst.Head);
            CreateTransition(head, snd.Head);
            CreateTransition(fst.Tail, tail);
            CreateTransition(snd.Tail, tail);

            return new Nfa(head, tail);
        }

        public Nfa Concat(Nfa fst, Nfa snd)
        {
            if (fst == null) return snd;
            if (snd == null) return fst;

            CreateTransition(fst.Tail, snd.Head);
            return new Nfa(fst.Head, snd.Tail);
        }

        public Nfa SingleClosure(Nfa nfa)
        {
            if (nfa == null)
                throw new ArgumentNullException("Operator [?] can't be applied without prefix", nameof(nfa));

            NfaState head = _stateFactory.Create();
            NfaState tail = _stateFactory.Create();

            CreateTransition(head, nfa.Head);
            CreateTransition(nfa.Tail, tail);
            CreateTransition(head, tail);

            return new Nfa(head, tail);
        }

        public Nfa KleeneClosure(Nfa nfa)
        {
            if (nfa == null)
                throw new ArgumentNullException("Operator [*] can't be applied without prefix", nameof(nfa));

            NfaState head = _stateFactory.Create();
            NfaState tail = _stateFactory.Create();

            CreateTransition(nfa.Tail, nfa.Head);
            CreateTransition(head, nfa.Head);
            CreateTransition(nfa.Tail, tail);
            CreateTransition(head, tail);

            return new Nfa(head, tail);
        }

        public Nfa PositiveClosure(Nfa nfa)
        {
            NfaState head = _stateFactory.Create();
            NfaState tail = _stateFactory.Create();

            CreateTransition(nfa.Tail, nfa.Head);
            CreateTransition(head, nfa.Head);
            CreateTransition(nfa.Tail, tail);

            return new Nfa(head, tail);
        }

        private Nfa Unwrap(CharIterator iterator, Nfa nfa)
        {
            StringBuilder sb = new StringBuilder();
            for (int counter = 1; ; sb.Append(iterator.Current))
            {
                if (!iterator.MoveNext())
                    throw new ScanException();

                if (iterator.Current == '(') counter++;
                if (iterator.Current == ')') counter--;
                if (counter == 0) break;
            }

            Nfa innerNfa = Parse(sb.ToString());
            return Concat(nfa, CheckForClosure(iterator, innerNfa));
        }

        private Nfa SingleClosure(CharIterator iterator, Nfa nfa)
        {
            return SingleClosure(nfa);
        }

        private Nfa KleeneClosure(CharIterator iterator, Nfa nfa)
        {
            return KleeneClosure(nfa);
        }

        private Nfa PositiveClosure(CharIterator iterator, Nfa nfa)
        {
            return PositiveClosure(nfa);
        }

        private Nfa Union(CharIterator iterator, Nfa nfa)
        {
            if (!iterator.MoveNext() || iterator.Current == '|')
                throw new ScanException();
            
            return Union(nfa, Parse(iterator, null));
            //Nfa macrosNfa;
            //if (_macros.TryGetValue(iterator.Current, out macrosNfa))
            //    return Union(nfa, CheckForClosure(iterator, macrosNfa));

            //return Union(nfa, CheckForClosure(iterator, Create(iterator.Current)));
        }

        private Nfa Macros(CharIterator iterator, Nfa nfa)
        {
            if (!iterator.MoveNext())
                throw new ScanException();

            Nfa macrosNfa;
            if (_macros.TryGetValue(iterator.Current, out macrosNfa))
                return Concat(nfa, CheckForClosure(iterator, macrosNfa));
            
            // there is an escape character
            return Concat(nfa, CheckForClosure(iterator, Create(iterator.Current)));
        }

        private Nfa CheckForClosure(CharIterator iterator, Nfa nfa)
        {
            if (!iterator.MoveNext())
                return nfa;

            switch (iterator.Current)
            {
                case '*': return KleeneClosure(iterator, nfa);
                case '+': return PositiveClosure(iterator, nfa);
                case '?': return SingleClosure(iterator, nfa);
            }

            iterator.MovePrevious();
            return nfa;
        }
    }
}
