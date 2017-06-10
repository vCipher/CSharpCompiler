using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IFieldDefinitionResolver : IFieldInfoResolver
    {
        FieldAttributes GetAttributes();
    }
}
