using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Variables
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

        public void Accept(IVarDeclarationVisitor visitor)
        {
            visitor.VisitVarDeclaration(this);
        }

        public string GetUniqueVarName()
        {
            return string.Format("{0}#{1}", VarName, Scope.GetHashCode());
        }
    }
}
