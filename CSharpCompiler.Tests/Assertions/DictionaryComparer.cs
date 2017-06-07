using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Tests.Assertions
{
    public sealed class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
    {
        private IEqualityComparer<TKey> _keyComparer;
        private IEqualityComparer<TValue> _valueComparer;

        public DictionaryComparer()
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            _valueComparer = EqualityComparer<TValue>.Default;
        }

        public DictionaryComparer(IEqualityComparer<TValue> valueComparer)
        {
            _keyComparer = EqualityComparer<TKey>.Default;
            _valueComparer = valueComparer;
        }

        public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
        {
            return (x.Count == y.Count)
                && !x.Keys.Except(y.Keys, _keyComparer).Any()
                && !x.Any(pair => !Equals(pair.Value, y[pair.Key]));
        }

        public int GetHashCode(Dictionary<TKey, TValue> obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                int hash = 1;
                foreach (var pair in obj)
                {
                    hash = (hash * 37) ^ _keyComparer.GetHashCode(pair.Key);
                    hash = (hash * 37) ^ _valueComparer.GetHashCode(pair.Value);
                }

                return hash;
            }
        }

        private bool Equals(TValue x, TValue y)
        {
            return _valueComparer.Equals(x, y);
        }
    }
}
