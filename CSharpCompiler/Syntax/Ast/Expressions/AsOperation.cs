using CSharpCompiler.Syntax.Ast.Types;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class AsOperation : Expression
    {
        public Expression Operand { get; private set; }

        public AstType Type { get; private set; }

        public AsOperation(Expression operand, AstType type)
        {
            Operand = operand;
            Type = type;
        }
    }
}
