using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Semantics.TypeSystem;
using System.Collections.ObjectModel;

namespace CSharpCompiler.Semantics.Builders
{
    public sealed class ConstructorBuilder : IMethodDefinitionResolver
    {
        private TypeDefinition _typeDef;

        private ConstructorBuilder(TypeDefinition typeDef)
        {
            _typeDef = typeDef;
        }

        public static MethodDefinition Build(TypeDefinition typeDef)
        {
            var builder = new ConstructorBuilder(typeDef);
            return new MethodDefinition(builder);
        }

        public MethodAttributes GetAttributes()
        {
            return MethodAttributes.Public
                | MethodAttributes.HideBySig
                | MethodAttributes.ReuseSlot
                | MethodAttributes.SpecialName
                | MethodAttributes.RTSpecialName;
        }

        public CallingConventions GetCallingConventions()
        {
            return CallingConventions.Default
                | CallingConventions.HasThis;
        }

        public Collection<CustomAttribute> GetCustomAttributes(MethodDefinition method)
        {
            return new Collection<CustomAttribute>();
        }

        public ITypeInfo GetDeclaringType()
        {
            return _typeDef;
        }

        public MethodImplAttributes GetImplAttributes()
        {
            return MethodImplAttributes.IL
                | MethodImplAttributes.Managed;
        }

        public MethodBody GetMethodBody()
        {
            var body = new MethodBody();
            var emitter = new OpCodeEmitter(body);
            emitter.Emit(OpCodes.Ldarg_0);
            emitter.Emit(OpCodes.Call, KnownType.Object.GetConstructor());
            emitter.Emit(OpCodes.Ret);
            return body;
        }

        public string GetName()
        {
            return ".ctor";
        }

        public Collection<ParameterDefinition> GetParameters(MethodDefinition method)
        {
            return new Collection<ParameterDefinition>();
        }

        public ITypeInfo GetReturnType()
        {
            return KnownType.Void;
        }
    }
}
