using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public interface IType
    {
        ElementType ElementType { get; }
    }
}
