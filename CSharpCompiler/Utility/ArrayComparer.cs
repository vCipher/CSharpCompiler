using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Utility
{
    public sealed class ArrayComparer<T> : IEqualityComparer<T[]>
    {
        public static readonly ArrayComparer<T> Default = new ArrayComparer<T>();
        
        private ArrayComparer() { }

        public bool Equals(T[] x, T[] y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x.Length != y.Length) return false;

            return x.SequenceEqual(y);
        }

        public int GetHashCode(T[] array)
        {
            if (array == null)
                return 0;

            unchecked
            {
                int hash = 1;
                for (int i = 0; i < array.Length; i++)
                    hash = (hash * 37) ^ array[i].GetHashCode();

                return hash;
            }
        }
    }
}
