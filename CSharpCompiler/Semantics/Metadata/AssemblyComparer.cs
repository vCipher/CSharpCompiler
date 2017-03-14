using CSharpCompiler.Utility;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class AssemblyComparer : IEqualityComparer<IAssemblyInfo>
    {
        private ByteArrayComparer _byteArrayComparer;

        public AssemblyComparer()
        {
            _byteArrayComparer = new ByteArrayComparer();
        }

        public bool Equals(IAssemblyInfo x, IAssemblyInfo y)
        {
            if (x == null || y == null) return false;
            if (x.GetType() != y.GetType()) return false;

            return string.Equals(x.Name, y.Name) &&
                string.Equals(x.Culture, y.Culture) &&
                x.Version.Equals(y.Version) &&
                _byteArrayComparer.Equals(x.PublicKey, y.PublicKey) &&
                _byteArrayComparer.Equals(x.PublicKeyToken, y.PublicKeyToken);
        }

        public int GetHashCode(IAssemblyInfo obj)
        {
            if (obj == null) return 0;

            unchecked
            {
                int res = 31;
                res ^= (obj.Name == null ? 0 : obj.Name.GetHashCode());
                res ^= (obj.Culture == null ? 0 : obj.Culture.GetHashCode());
                res ^= (obj.Version == null ? 0 : obj.Version.GetHashCode());
                res ^= _byteArrayComparer.GetHashCode(obj.PublicKey);
                res ^= _byteArrayComparer.GetHashCode(obj.PublicKeyToken);
                return res;
            }
        }
    }
}
