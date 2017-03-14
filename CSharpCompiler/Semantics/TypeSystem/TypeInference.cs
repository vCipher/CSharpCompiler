using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Semantics.TypeSystem
{
    /// <summary>
    /// Rules for type inference
    /// </summary>
    public static class TypeInference
    {
        public static IType InferType(Literal literal)
        {
            switch (literal.Value.Tag)
            {
                case TokenTag.INT_LITERAL:
                    return KnownType.Int32;
                case TokenTag.FLOAT_LITERAL:
                    return KnownType.Single;
                case TokenTag.DOUBLE_LITERAL:
                    return KnownType.Double;
                case TokenTag.STRING_LITERAL:
                    return KnownType.String;
                case TokenTag.TRUE:
                case TokenTag.FALSE:
                    return KnownType.Boolean;
                default:
                    throw new TypeInferenceException("Can't inference type from literal: {0}", literal.Value);
            }
        }

        public static IType InferType(VarAccess varAccess)
        {
            return varAccess.Resolve().Type.ToType();
        }

        public static IType InferType(VarDeclaration declaration)
        {
            if (declaration.IsImplicit)
            {
                if (declaration.Initializer == null)
                    throw new SemanticException("Can't declare unintialized local variable with implicit typification.");

                return declaration.Initializer.InferType();
            }

            return declaration.Type.ToType();
        }

        public static IType InferType(IType leftType, IType rightType)
        {
            if (leftType.Equals(rightType))
                return leftType;

            throw new TypeInferenceException("Can't inference type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
