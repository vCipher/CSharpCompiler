using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler
{
    public sealed class DictionaryComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
    {
        private IEqualityComparer<TValue> _valueComparer;

        public DictionaryComparer()
        {
            _valueComparer = EqualityComparer<TValue>.Default;
        }

        public DictionaryComparer(IEqualityComparer<TValue> valueComparer)
        {
            _valueComparer = valueComparer;
        }

        public bool Equals(Dictionary<TKey, TValue> x, Dictionary<TKey, TValue> y)
        {
            if (x.Count != y.Count) return false;
            if (x.Keys.Except(y.Keys).Any()) return false;
            if (x.Any(pair => !Equals(pair.Value, y[pair.Key]))) return false;
            return true;
        }

        public int GetHashCode(Dictionary<TKey, TValue> obj)
        {
            return obj.GetHashCode();
        }

        private bool Equals(TValue x, TValue y)
        {
            return _valueComparer.Equals(x, y);
        }
    }
}
