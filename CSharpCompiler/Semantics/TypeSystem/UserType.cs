using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class UserType : IType
    {
        public ElementType ElementType { get { return ElementType.Class; } }
    }
}
