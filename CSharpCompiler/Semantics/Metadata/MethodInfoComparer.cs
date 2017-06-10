using System.Collections.Generic;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodInfoComparer : IEqualityComparer<IMethodInfo>
    {
        public static readonly MethodInfoComparer Default = new MethodInfoComparer();

        private MethodInfoComparer()
        { }

        public bool Equals(IMethodInfo x, IMethodInfo y)
        {
            if (ReferenceEquals(x, y)) return true;

            return string.Equals(x.Name, y.Name)
                && x.ReturnType.Equals(y.ReturnType)
                && x.DeclaringType.Equals(y.DeclaringType)
                && x.CallingConventions == x.CallingConventions
                && x.Parameters.SequenceEqual(y.Parameters);
        }

        public int GetHashCode(IMethodInfo obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Name);
                hash = hash * 23 + TypeInfoComparer.Default.GetHashCode(obj.DeclaringType);
                hash = hash * 23 + RuntimeBindingSignature.GetMethodSignature(obj).GetHashCode();
                return hash;
            }
        }
    }
}
