using CSharpCompiler.Semantics.Metadata;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IMethodDefinitionResolver : IMethodInfoResolver
    {
        MethodBody GetMethodBody();
        MethodAttributes GetAttributes();
        MethodImplAttributes GetImplAttributes();
        Collection<ParameterDefinition> GetParameters(MethodDefinition method);
        Collection<CustomAttribute> GetCustomAttributes(MethodDefinition method);
    }
}
