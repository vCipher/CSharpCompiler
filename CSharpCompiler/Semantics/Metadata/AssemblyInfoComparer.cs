using CSharpCompiler.Utility;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyInfoComparer : IEqualityComparer<IAssemblyInfo>
    {
        public static readonly AssemblyInfoComparer Default = new AssemblyInfoComparer();

        private AssemblyInfoComparer() { }

        public bool Equals(IAssemblyInfo x, IAssemblyInfo y)
        {
            if (ReferenceEquals(x, y)) return true;

            return string.Equals(x.Name, y.Name)
                && x.Version.Equals(y.Version)
                && ByteArrayComparer.Default.Equals(x.PublicKeyToken, y.PublicKeyToken);
        }

        public int GetHashCode(IAssemblyInfo obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + obj.Version?.GetHashCode() ?? 0;
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Name);
                hash = hash * 23 + ByteArrayComparer.Default.GetHashCode(obj.PublicKeyToken);
                return hash;
            }
        }
    }
}
