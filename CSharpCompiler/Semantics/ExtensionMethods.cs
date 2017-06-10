using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Utility;
using System.Linq;

namespace CSharpCompiler.Semantics
{
    public static class ExtensionMethods
    {
        public static IMethodInfo GetMethod(this InvokeExpression invokeExpr, SemanticContext context)
        {
            var parts = invokeExpr.MethodName.Parts.ToInvertedStack();

            TypeDefinition type;
            if (!context.TypeRegistry.TrySearch(parts, out type))
                throw new SemanticException("Can't find method by identifier: {0}", invokeExpr.MethodName);

            // todo: implement member chaining

            var methodName = parts.Pop();
            var types = invokeExpr.Arguments
                .Select(x => TypeInference.InferType(x.Value, context))
                .ToArray(invokeExpr.Arguments.Count);

            var method = type.GetMethod(methodName, types);

            if (method == null)
                throw new SemanticException("Can't find method by identifier: {0}", invokeExpr.MethodName);

            if (method.DeclaringType.Assembly.Equals(context.TypeDefinition.Assembly))
                return method;

            return MethodFactory.GetMethodReference(method);
        }

        public static bool IsTypeOf(this ITypeInfo self, ITypeInfo other)
        {
            return TypeInfoComparer.Default.Equals(self, other);
        }

        public static bool IsNested(this TypeAttributes attributes)
        {
            switch (attributes & TypeAttributes.VisibilityMask)
            {
                case TypeAttributes.NestedAssembly:
                case TypeAttributes.NestedFamANDAssem:
                case TypeAttributes.NestedFamily:
                case TypeAttributes.NestedFamORAssem:
                case TypeAttributes.NestedPrivate:
                case TypeAttributes.NestedPublic:
                    return true;
                default:
                    return false;
            }
        }
    }
}
