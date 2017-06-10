using System.Collections.Generic;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class FieldInfoComparer : IEqualityComparer<IFieldInfo>
    {
        public static readonly FieldInfoComparer Default = new FieldInfoComparer();

        private FieldInfoComparer()
        { }

        public bool Equals(IFieldInfo x, IFieldInfo y)
        {
            if (ReferenceEquals(x, y)) return true;

            return string.Equals(x.Name, y.Name)
                && x.FieldType.Equals(y.FieldType)
                && x.DeclaringType.Equals(y.DeclaringType);
        }

        public int GetHashCode(IFieldInfo obj)
        {
            if (obj == null)
                return 0;

            unchecked
            {
                int hash = 17;
                hash = hash * 23 + EqualityComparer<string>.Default.GetHashCode(obj.Name);
                hash = hash * 23 + TypeInfoComparer.Default.GetHashCode(obj.DeclaringType);
                hash = hash * 23 + RuntimeBindingSignature.GetFieldSignature(obj).GetHashCode();
                return hash;
            }
        }
    }
}
