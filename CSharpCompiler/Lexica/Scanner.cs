using CSharpCompiler.Lexica.Regexp;
using CSharpCompiler.Lexica.Tokens;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Lexica
{
    public sealed class Scanner
    {
        private const int END_STATE = -2;

        private int _state;
        private Stack<int> _states;
        private TransitionTable _table;
        private List<Token> _tokens;
        private StringBuilder _lexeme;
        private CharEnumerator _enumerator;

        private Scanner(string content, TransitionTable table)
        {
            _enumerator = new CharEnumerator(content);
            _table = table;
            _tokens = new List<Token>();
            _states = new Stack<int>();
            _lexeme = new StringBuilder();
        }

        public static List<Token> Scan(string content)
        {
            return Scan(content, TransitionTable.Default);
        }

        public static List<Token> Scan(string content, TransitionTable table)
        {
            return new Scanner(content, table).Scan();
        }

        public List<Token> Scan()
        {
            while (_enumerator.MoveNext())
            {
                if (IsEmpty(_enumerator.Current))
                    continue;

                PrepareForNextLexeme();
                ScanNextLexeme();
                RollbackIfNotAcceptLexeme();

                _tokens.Add(CreateToken());
            }

            return _tokens;
        }

        private void PrepareForNextLexeme()
        {
            _lexeme.Clear();
            _states.Clear();
            _states.Push(END_STATE);
            _state = _table.Head;
        }

        private void ScanNextLexeme()
        {
            do
            {
                _lexeme.Append(_enumerator.Current);

                if (_table.IsAcceptingState(_state))
                    _states.Clear();

                _states.Push(_state);
                _state = _table[_state, _enumerator.Current];
            }
            while (_state != TransitionTable.UNKNOWN_STATE && _enumerator.MoveNext());
        }

        private void RollbackIfNotAcceptLexeme()
        {
            string originLexeme = _lexeme.ToString();
            while (!_table.IsAcceptingState(_state))
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
            var alias = _table.GetTokenAlias(_state);
            return Tokens.Tokens.GetToken(alias, _lexeme.ToString());
        }

        private bool IsEmpty(char ch)
        {
            return ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n';
        }
    }
}
