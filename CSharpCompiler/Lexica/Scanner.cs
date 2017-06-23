using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Lexica
{
    public sealed class Scanner
    {
        private const int END_STATE = -2;

        private int _state;
        private Stack<int> _states;
        private ScannerOptions _options;
        private StringBuilder _lexeme;
        private CharEnumerator _enumerator;

        private Scanner(string content, ScannerOptions options)
        {
            _enumerator = new CharEnumerator(content);
            _options = options;
            _states = new Stack<int>();
            _lexeme = new StringBuilder();
        }

        public static TokenEnumerable Scan(string content)
        {
            return Scan(content, new ScannerOptions());
        }

        public static TokenEnumerable Scan(string content, ScannerOptions options)
        {
            var scanner = new Scanner(content, options);
            return new TokenEnumerable(scanner);
        }

        internal List<Token> Scan()
        {
            var tokens = new List<Token>();

            while (_enumerator.MoveNext())
            {
                PrepareForNextLexeme();
                ScanNextLexeme();
                RollbackIfNotAcceptLexeme();
                ProccessToken(tokens);
            }

            return tokens;
        }

        private void ProccessToken(List<Token> tokens)
        {
            var token = CreateToken();
            if (Array.IndexOf(_options.BlackList, token.Tag) == -1)
                tokens.Add(token);
        }

        private void PrepareForNextLexeme()
        {
            _lexeme.Clear();
            _states.Clear();
            _states.Push(END_STATE);
            _state = _options.Transitions.Head;
        }

        private void ScanNextLexeme()
        {
            do
            {
                _lexeme.Append(_enumerator.Current);

                if (_options.Transitions.IsAcceptingState(_state))
                    _states.Clear();

                _states.Push(_state);
                _state = _options.Transitions[_state, _enumerator.Current];
            }
            while (_state != TransitionTable.UNKNOWN_STATE && _enumerator.MoveNext());
        }

        private void RollbackIfNotAcceptLexeme()
        {
            string originLexeme = _lexeme.ToString();
            while (!_options.Transitions.IsAcceptingState(_state))
            {
                bool isTerminalState = _state == END_STATE || _lexeme.Length == 0;
                if (isTerminalState)
                    throw new NotAcceptLexemeException(originLexeme);

                _lexeme.Remove(_lexeme.Length - 1, 1);
                _enumerator.MovePrevious();
                _state = _states.Pop();
            }
        }

        private Token CreateToken()
        {
            var alias = _options.Transitions.GetTokenAlias(_state);
            return Tokens.Tokens.GetToken(alias, _lexeme.ToString());
        }
    }
}
