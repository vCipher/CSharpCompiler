using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeComparer : IEqualityComparer<ITypeInfo>
    {
        private AssemblyComparer _assemblyComparer;

        public TypeComparer()
        {
            _assemblyComparer = new AssemblyComparer();
        }

        public bool Equals(ITypeInfo x, ITypeInfo y)
        {
            if (x == null || y == null) return false;
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Namespace, y.Namespace) &&
                _assemblyComparer.Equals(x.Assembly, y.Assembly);
        }

        public int GetHashCode(ITypeInfo obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int res = 31;
                res ^= (obj.Name == null ? 0 : obj.Name.GetHashCode());
                res ^= (obj.Namespace == null ? 0 : obj.Namespace.GetHashCode());
                res ^= _assemblyComparer.GetHashCode(obj.Assembly);
                return res;
            }
        }
    }
}
