using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Cil;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Semantics.TypeSystem;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class Assignment : BinaryOperation
    {
        private bool _isStatementExpression;

        public Assignment(Token @operator, Expression leftOperand, Expression rightOperand, bool isStatementExpression) 
            : base(@operator, leftOperand, rightOperand)
        {
            _isStatementExpression = isStatementExpression;
        }

        public override void Build(MethodBuilder builder)
        {
            if (LeftOperand is VarAccess)
            {
                Build(builder, (VarAccess)LeftOperand);
                return;
            }

            if (LeftOperand is Assignment)
            {
                Build(builder, (VarAccess)((Assignment)LeftOperand).LeftOperand);
                return;
            }

            throw new SyntaxException("The left-hand side of an assignment must be a variable, property or indexer");
        }

        private void Build(MethodBuilder builder, VarAccess varAccess)
        {
            var varDef = builder.GetVarDefinition(varAccess);
            RightOperand.Build(builder);

            if (!_isStatementExpression) builder.Emit(OpCodes.Dup);
            builder.Emit(OpCodes.Stloc, varDef);
        }

        public override ITypeInfo InferType()
        {
            var leftType = LeftOperand.InferType();
            var rightType = RightOperand.InferType();

            if (leftType.Equals(rightType))
                return leftType;

            throw new TypeInferenceException("Can't infer a type for: {0} and for: {1}", leftType, rightType);
        }
    }
}
