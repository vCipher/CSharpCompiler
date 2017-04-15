using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class TypeInfoComparer : IEqualityComparer<ITypeInfo>
    {
        public static readonly TypeInfoComparer Default = new TypeInfoComparer();

        private TypeInfoComparer() { }

        public bool Equals(ITypeInfo x, ITypeInfo y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Namespace, y.Namespace) &&
                AssemblyInfoComparer.Default.Equals(x.Assembly, y.Assembly);
        }

        public int GetHashCode(ITypeInfo obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int res = 31;
                res ^= (obj.Name == null ? 0 : obj.Name.GetHashCode());
                res ^= (obj.Namespace == null ? 0 : obj.Namespace.GetHashCode());
                res ^= AssemblyInfoComparer.Default.GetHashCode(obj.Assembly);
                return res;
            }
        }
    }
}
