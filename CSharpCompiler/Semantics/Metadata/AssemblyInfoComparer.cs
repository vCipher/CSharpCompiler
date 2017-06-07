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
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Name, y.Name) 
                && string.Equals(x.Culture, y.Culture) 
                && x.Version.Equals(y.Version) 
                && ByteArrayComparer.Default.Equals(x.PublicKey, y.PublicKey) 
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
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Culture);
                hash = hash * 23 + ByteArrayComparer.Default.GetHashCode(obj.PublicKey);
                hash = hash * 23 + ByteArrayComparer.Default.GetHashCode(obj.PublicKeyToken);
                return hash;
            }
        }
    }
}
