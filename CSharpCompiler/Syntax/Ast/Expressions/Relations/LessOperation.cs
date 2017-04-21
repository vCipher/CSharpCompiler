using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions.Relations
{
    public sealed class LessOperation : RelationOperation
    {
        public LessOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            LeftOperand.Build(builder);
            RightOperand.Build(builder);
            builder.Emit(OpCodes.Clt);
        }
    }
}
