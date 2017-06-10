using CSharpCompiler.Semantics.Metadata;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Resolvers
{
    public interface IMethodReferenceResolver : IMethodInfoResolver
    {
        Collection<ParameterDefinition> GetParameters(MethodReference method);
    }
}
