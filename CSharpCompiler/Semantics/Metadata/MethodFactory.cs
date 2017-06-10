using CSharpCompiler.Semantics.Resolvers;
using CSharpCompiler.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CSharpCompiler.Semantics.Metadata
{
    public static class MethodFactory
    {
        private static Func<MethodDefinition, MethodReference> _getMethodReference
            = Func.Memoize<MethodDefinition, MethodReference>(MethodReferenceResolver.Resolve);

        public static MethodReference GetMethodReference(IMethodInfo method)
        {
            if (method is MethodReference) return (MethodReference)method;
            return _getMethodReference((MethodDefinition)method);
        }

        private sealed class MethodReferenceResolver : IMethodReferenceResolver
        {
            private MethodDefinition _methodDef;

            private MethodReferenceResolver(MethodDefinition methodDef)
            {
                _methodDef = methodDef;
            }

            public static MethodReference Resolve(MethodDefinition methodDef)
            {
                var resolver = new MethodReferenceResolver(methodDef);
                return new MethodReference(resolver);
            }

            public CallingConventions GetCallingConventions()
            {
                return _methodDef.CallingConventions;
            }

            public ITypeInfo GetDeclaringType()
            {
                return TypeFactory.GetTypeReference(_methodDef.DeclaringType);
            }

            public string GetName()
            {
                return _methodDef.Name;
            }

            public Collection<ParameterDefinition> GetParameters(MethodReference method)
            {
                return _methodDef.Parameters
                    .Select(param => ParameterDefinitionResolver.Resolve(param))
                    .ToCollection(_methodDef.Parameters.Count);
            }

            public ITypeInfo GetReturnType()
            {
                return TypeFactory.GetTypeReference(_methodDef.ReturnType);
            }
        }

        private sealed class ParameterDefinitionResolver : IParameterDefinitionResolver
        {
            private ParameterDefinition _param;

            private ParameterDefinitionResolver(ParameterDefinition param)
            {
                _param = param;
            }

            public static ParameterDefinition Resolve(ParameterDefinition param)
            {
                var resolver = new ParameterDefinitionResolver(param);
                return new ParameterDefinition(resolver);
            }

            public ParameterAttributes GetAttributes()
            {
                return _param.Attributes;
            }

            public int GetIndex()
            {
                return _param.Index;
            }

            public IMethodInfo GetMethod()
            {
                return GetMethodReference(_param.Method);
            }

            public string GetName()
            {
                return _param.Name;
            }

            public ITypeInfo GetParameterType()
            {
                return TypeFactory.GetTypeReference(_param.Type);
            }
        }
    }
}
