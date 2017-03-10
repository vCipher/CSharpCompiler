using CSharpCompiler.Syntax.Ast.Expressions;

namespace CSharpCompiler.Syntax.Ast
{
    public sealed class VarDeclarator : AstNode
    {
        public VarLocation Location { get; private set; }

        public Expression Value { get; private set; }

        public VarDeclarator(VarLocation location, Expression value)
        {
            Location = location;
            Value = value;
        }
    }
}
