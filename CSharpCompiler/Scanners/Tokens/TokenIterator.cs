using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Scanners.Tokens
{
    public sealed class TokenIterator : IEnumerator<Token>
    {
        private const int MOVE_STEP = 1;
        private int _index;        
        private Token _currentElement;
        private List<Token> _tokens;

        public int Position
        {
            get { return _index; }
        }

        public Token Current
        {
            get { return _currentElement; }
        }

        object IEnumerator.Current
        {
            get { return _currentElement; }
        }

        public TokenIterator(List<Token> tokens)
        {
            _tokens = tokens;
            _index = -1;
            _currentElement = Tokens.EOF;
        }

        public bool MoveNext()
        {
            if (CanMove(MOVE_STEP))
            {
                _index += MOVE_STEP;
                _currentElement = _tokens[_index];
                return true;
            }

            _index = _tokens.Count;
            return false;
        }

        public bool CanMove(int shift)
        {
            int index = _index + shift;
            return index >= 0 && index < _tokens.Count;
        }

        public void Reset()
        {
            _index = -1;
            _currentElement = Tokens.EOF;
        }

        public void Reset(int index)
        {
            if (index < 0 || index >= _tokens.Count)
                throw new ArgumentOutOfRangeException("index");

            _index = index;
            _currentElement = _tokens[index];
        }

        public Token Lookahead()
        {
            return Lookahead(MOVE_STEP);
        }

        public Token Lookahead(int shift)
        {
            return CanMove(shift) ? _tokens[_index + shift] : Tokens.EOF;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tokens.Clear();
                    _tokens = null;
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// This code added to correctly implement the disposable pattern.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
