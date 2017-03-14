using CSharpCompiler.Utility;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public sealed class MethodReference : IMethodInfo
    {
        public string Name { get; private set; }
        public MetadataToken Token { get; private set; }
        public ITypeInfo ReturnType { get; private set; }
        public ITypeInfo DeclaringType { get; private set; }
        public CallingConventions CallingConventions { get; private set; }
        public Collection<ParameterDefinition> Parameters { get; private set; }
        
        public MethodReference(System.Reflection.ConstructorInfo ctorInfo)
        {
            Name = ctorInfo.Name;
            Token = new MetadataToken(MetadataTokenType.MemberRef, 0);
            ReturnType = new TypeReference(typeof(void));
            DeclaringType = new TypeReference(ctorInfo.DeclaringType);
            CallingConventions = GetCallingConventions(ctorInfo);
            Parameters = ctorInfo.GetParameters()
                .Select(param => new ParameterDefinition(param.Name, new TypeReference(param.ParameterType), this))
                .ToCollection();
        }

        public MethodReference(System.Reflection.MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            Token = new MetadataToken(MetadataTokenType.MemberRef, 0);
            ReturnType = new TypeReference(methodInfo.ReturnType);
            DeclaringType = new TypeReference(methodInfo.DeclaringType);
            CallingConventions = GetCallingConventions(methodInfo);
            Parameters = methodInfo.GetParameters()
                .Select(param => new ParameterDefinition(param.Name, new TypeReference(param.ParameterType), this))
                .ToCollection();
        }

        public void ResolveToken(uint rid)
        {
            Token = new MetadataToken(Token.Type, rid);
        }

        private CallingConventions GetCallingConventions(System.Reflection.MethodBase methodBase)
        {
            CallingConventions conventions = CallingConventions.Default;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.HasThis))
                conventions |= CallingConventions.HasThis;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.ExplicitThis))
                conventions |= CallingConventions.ExplicitThis;

            if (methodBase.CallingConvention.HasFlag(System.Reflection.CallingConventions.VarArgs))
                conventions |= CallingConventions.VarArg;

            return conventions;
        }
    }
}
