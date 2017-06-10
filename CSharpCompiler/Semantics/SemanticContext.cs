using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.Generic;

namespace CSharpCompiler.Semantics
{
    public sealed class SemanticContext
    {
        public List<List<string>> Usings { get; private set; }
        public TypeRegistry TypeRegistry { get; private set; }
        public TypeDefinition TypeDefinition { get; private set; }

        public SemanticContext(List<List<string>> usings, TypeRegistry typeRegistry, TypeDefinition typeDef)
        {
            Usings = usings;
            TypeRegistry = typeRegistry;
            TypeDefinition = typeDef;
        }
    }
}
