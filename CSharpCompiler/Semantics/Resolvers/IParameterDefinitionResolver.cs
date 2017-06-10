using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IParameterDefinitionResolver
    {
        int GetIndex();
        string GetName();
        IMethodInfo GetMethod();
        ITypeInfo GetParameterType();
        ParameterAttributes GetAttributes();
    }
}
