using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Utility
{
    public class Enumerator<T> : IEnumerator<T>
    {
        protected const int MOVE_STEP = 1;
        protected const int ILLEGAL_INDEX = -1;

        protected int _index;
        protected T _currentElement;
        protected IReadOnlyList<T> _source;

        public int Position
        {
            get { return _index; }
        }

        public T Current
        {
            get { return _currentElement; }
        }

        protected virtual T DefaultValue
        {
            get { return default(T); }
        }

        object IEnumerator.Current
        {
            get { return _currentElement; }
        }

        public Enumerator(IReadOnlyList<T> source)
        {
            _source = source;
            _index = ILLEGAL_INDEX;
            _currentElement = DefaultValue;
        }

        public bool MoveNext()
        {
            if (CanMove(MOVE_STEP))
            {
                _index += MOVE_STEP;
                _currentElement = _source[_index];
                return true;
            }

            return false;
        }

        public void Reset()
        {
            _index = ILLEGAL_INDEX;
            _currentElement = DefaultValue;
        }

        protected virtual bool CanMove(int shift)
        {
            int index = _index + shift;
            return index >= 0 && index < _source.Count;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Reset();
                    if (_source is IDisposable)
                        ((IDisposable)_source).Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
