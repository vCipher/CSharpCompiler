using CSharpCompiler.Scanners.Regexp;
using CSharpCompiler.Scanners.Tokens;
using System.Collections.Generic;
using System.Text;

namespace CSharpCompiler.Scanners
{
    public sealed class Scanner
    {
        private const int END_STATE = -1;
        private const int BAD_STATE = -2;

        private TransitionTable _table;

        public Scanner(TransitionTable table)
        {
            _table = table;
        }

        public List<Token> Scan(string content)
        {
            List<Token> tokens = new List<Token>();
            Stack<int> states = new Stack<int>();
            StringBuilder lexeme = new StringBuilder();
            CharIterator iterator = new CharIterator(content);

            while (iterator.MoveNext())
            {
                char ch = iterator.Current;

                if (IsEmpty(ch))
                    continue;

                // prepare before scan next lexeme
                lexeme.Clear();
                states.Clear();
                states.Push(BAD_STATE);
                int state = _table.Head;

                // scan next lexeme
                do
                {
                    ch = iterator.Current;
                    lexeme.Append(ch);

                    if (_table.IsAcceptingState(state))
                        states.Clear();

                    states.Push(state);
                    state = _table[state, ch];
                }
                while (state != END_STATE && iterator.MoveNext());

                // roll back if DFA doesn't accept lexeme
                while (!_table.IsAcceptingState(state) && (state = states.Pop()) != BAD_STATE)
                {
                    lexeme.Remove(lexeme.Length - 1, 1);
                    iterator.MovePrevious();
                }

                if (!_table.IsAcceptingState(state) || lexeme.Length == 0)
                    throw new ScanException();

                var alias = _table.GetTokenAlias(state);
                var token = CSharpTokens.GetToken(alias, lexeme.ToString());
                tokens.Add(token);
            }

            return tokens;
        }

        private bool IsEmpty(char ch)
        {
            return ch == ' ' || ch == '\t' || ch == '\r' || ch == '\n';
        }
    }
}
