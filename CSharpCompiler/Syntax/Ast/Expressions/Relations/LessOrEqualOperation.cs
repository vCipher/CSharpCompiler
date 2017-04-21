using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;

namespace CSharpCompiler.Syntax.Ast.Expressions.Relations
{
    public sealed class LessOrEqualOperation : RelationOperation
    {
        public LessOrEqualOperation(Token @operator, Expression leftOperand, Expression rightOperand) 
            : base(@operator, leftOperand, rightOperand)
        { }

        public override void Build(MethodBuilder builder)
        {
            LeftOperand.Build(builder);
            RightOperand.Build(builder);
            builder.Emit(OpCodes.Clt);
            builder.Emit(OpCodes.Ldc_I4_0);
            builder.Emit(OpCodes.Ceq);
        }
    }
}
