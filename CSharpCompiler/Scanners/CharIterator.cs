using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpCompiler.Scanners
{
    /// <summary>
    /// Supports iterating over a <see cref="string" /> object and reading its individual characters. 
    /// This class cannot be inherited.
    /// </summary>
    /// <remarks>Forked from <see cref="CharEnumerator"/></remarks>
    [Serializable]
    public sealed class CharIterator : IEnumerator, ICloneable, IEnumerator<char>, IDisposable
    {
        private string _str;
        private int _index;
        private char _currentElement;

        /// <summary>
        /// Gets the currently referenced character in the string enumerated by this <see cref="CharIterator" /> object. 
        /// For a description of this member, see <see cref="IEnumerator.Current" />. 
        /// </summary>
        /// <returns>The boxed Unicode character currently referenced by this <see cref="CharIterator" /> object.</returns>
        /// <exception cref="InvalidOperationException">
        /// Enumeration has not started.-or-Enumeration has ended.
        /// </exception>
        object IEnumerator.Current
        {
            get
            {
                if (this._index == -1)
                    throw new InvalidOperationException();

                if (this._index >= this._str.Length)
                    throw new InvalidOperationException();

                return this._currentElement;
            }
        }

        /// <summary>
        /// Gets the currently referenced character in the string enumerated by this <see cref="CharIterator" /> object.
        /// </summary>
        /// <returns>The Unicode character currently referenced by this <see cref="CharIterator" /> object.</returns>
        /// <exception cref="InvalidOperationException">
        /// The index is invalid; that is, it is before the first or after the last character of the enumerated string.
        /// </exception>
        public char Current
        {
            get
            {
                if (this._index == -1)
                    throw new InvalidOperationException();

                if (this._index >= this._str.Length)
                    throw new InvalidOperationException();

                return this._currentElement;
            }
        }

        internal CharIterator(string str)
        {
            this._str = str;
            this._index = -1;
        }

        /// <summary>
        /// Creates a copy of the current <see cref="CharIterator" /> object.
        /// </summary>
        /// <returns>
        /// An <see cref="object" /> that is a copy of the current <see cref="CharIterator" /> object.
        /// </returns>
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        /// <summary>
        /// Check a posibility of incrementing the internal index of the current <see cref="CharIterator" /> 
        /// object to the next character of the enumerated string.
        /// </summary>
        /// <returns>
        /// true if the index it's posibile to increment the internal index; otherwise, false.
        /// </returns>
        public bool CanMoveNext()
        {
            return this._index < this._str.Length - 1;
        }

        /// <summary>
        /// Increments the internal index of the current <see cref="CharIterator" /> object to the next character of the enumerated string.
        /// </summary>
        /// <returns>
        /// true if the index is successfully incremented and within the enumerated string; otherwise, false.
        /// </returns>
        public bool MoveNext()
        {
            if (this._index < this._str.Length - 1)
            {
                this._index++;
                this._currentElement = this._str[this._index];
                return true;
            }
            this._index = this._str.Length;
            return false;
        }

        /// <summary>
        /// Decrements the internal index of the current <see cref="CharIterator" /> object to the previous character of the enumerated string.
        /// </summary>
        /// <returns>
        /// true if the index is successfully decremented and within the enumerated string; otherwise, false.
        /// </returns>
        public bool MovePrevious()
        {
            if (this._index > 0)
            {
                this._index--;
                this._currentElement = this._str[this._index];
                return true;
            }
            this._index = 0;
            return false;
        }

        /// <summary>
        /// Releases all resources used by the current instance of the <see cref="CharIterator" /> class.
        /// </summary>
        public void Dispose()
        {
            if (this._str != null)
            {
                this._index = this._str.Length;
            }
            this._str = null;
        }

        /// <summary>
        /// Initializes the index to a position logically before the first character of the enumerated string.
        /// </summary>
        public void Reset()
        {
            this._currentElement = '\0';
            this._index = -1;
        }
    }
}
