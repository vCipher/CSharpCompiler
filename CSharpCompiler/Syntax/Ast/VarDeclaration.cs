using CSharpCompiler.Semantics;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class VarDeclaration : AstNode
    {
        public TypeNode Type { get; private set; }
        public string VarName { get; private set; }
        public Expression Initializer { get; private set; }
        public VarScope Scope { get; private set; }
        public bool IsImplicit { get { return Type == null; } }

        /// <summary>
        /// Create a local variable declaration with the implicit typification.
        /// </summary>
        public VarDeclaration(string varName, Expression initializer, VarScope scope)
            : this(null, varName, initializer, scope)
        { }

        /// <summary>
        /// Create a local variable declaration with the explicit typification.
        /// </summary>
        public VarDeclaration(TypeNode type, string varName, Expression initializer, VarScope scope)
        {
            Type = type;
            VarName = varName;
            Initializer = initializer;
            Scope = scope;
            Scope.Register(varName, this);
        }

        public void Build(MethodBuilder builder)
        {
            var varDef = builder.GetVarDefinition(this);
            builder.Register(varDef);

            var initialier = Initializer;
            if (initialier == null) return;

            initialier.Build(builder);
            builder.Emit(OpCodes.Stloc, varDef);
        }

        public ITypeInfo InferType()
        {
            if (IsImplicit)
            {
                if (Initializer == null)
                    throw new SemanticException("Can't declare unintialized local variable with implicit typification.");

                return Initializer.InferType();
            }

            return Type.ToType();
        }

        public string GetUniqueVarName()
        {
            return string.Format("{0}#{1}", VarName, Scope.GetHashCode());
        }
    }
}
