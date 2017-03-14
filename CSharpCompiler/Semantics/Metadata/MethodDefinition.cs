using System.Collections.ObjectModel;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodDefinition : IMethodInfo
    {
        public string Name { get; private set; }
        public MetadataToken Token { get; private set; }
        public ITypeInfo ReturnType { get; private set; }
        public ITypeInfo DeclaringType { get; private set; }
        public CallingConventions CallingConventions { get; private set; }
        public Collection<ParameterDefinition> Parameters { get; private set; }

        public MethodBody Body { get; private set; }
        public MethodImplAttributes ImplAttributes { get; private set; }
        public MethodAttributes Attributes { get; private set; }

        public bool HasThis
        {
            get { return !Attributes.HasFlag(MethodAttributes.Static); }
        }

        public MethodDefinition(string name, MethodAttributes attributes, TypeDefinition typeDef)
        {
            Name = name;
            Token = new MetadataToken(MetadataTokenType.Method, 0);            
            ReturnType = new TypeReference(typeof(void));
            DeclaringType = typeDef;
            Body = new MethodBody();
            ImplAttributes = MethodImplAttributes.IL | MethodImplAttributes.Managed;
            Attributes = attributes;
            Parameters = new Collection<ParameterDefinition>();
            CallingConventions = GetCallingConvention();
        }

        private CallingConventions GetCallingConvention()
        {
            var convention = CallingConventions.Default;
            if (HasThis) convention |= CallingConventions.HasThis;
            return convention;
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }
    }
}
