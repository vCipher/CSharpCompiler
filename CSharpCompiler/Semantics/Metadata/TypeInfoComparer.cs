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

            return string.Equals(x.Name, y.Name) 
                && string.Equals(x.Namespace, y.Namespace) 
                && AssemblyInfoComparer.Default.Equals(x.Assembly, y.Assembly);
        }

        public int GetHashCode(ITypeInfo obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Name);
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Namespace);
                hash = hash * 23 + AssemblyInfoComparer.Default.GetHashCode(obj.Assembly);
                return hash;
            }
        }
    }
}
