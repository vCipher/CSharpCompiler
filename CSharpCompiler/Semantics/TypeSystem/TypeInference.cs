using CSharpCompiler.Lexica.Tokens;
using CSharpCompiler.Semantics.Metadata;
using CSharpCompiler.Syntax.Ast;
using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Expressions.Conditions;
using CSharpCompiler.Syntax.Ast.Expressions.Relations;
using CSharpCompiler.Syntax.Ast.Types;
using CSharpCompiler.Syntax.Ast.Variables;

namespace CSharpCompiler.Semantics.TypeSystem
{
    public sealed class TypeInference : IExpressionVisitor, ITypeVisitor, IVarDeclarationVisitor
    {
        private ITypeInfo _type;
        private TypeInference() { }

        public static ITypeInfo InferType(Expression node)
        {
            var inference = new TypeInference();
            node.Accept(inference);
            return inference._type;
        }

        public static ITypeInfo InferType(TypeNode node)
        {
            var inference = new TypeInference();
            node.Accept(inference);
            return inference._type;
        }

        public static ITypeInfo InferType(VarDeclaration node)
        {
            var inference = new TypeInference();
            node.Accept(inference);
            return inference._type;
        }

        public void VisitVarDeclaration(VarDeclaration node)
        {
            if (node.IsImplicit && node.Initializer == null) throw new SemanticException("Can't declare unintialized local variable with implicit typification.");
            _type = node.IsImplicit ? InferType(node.Initializer) : InferType(node.Type);
        }

        #region conditions
        public void VisitAndOperation(AndOperation node) => InferConditionalOperationType(node);
        public void VisitOrOperation(OrOperation node) => InferConditionalOperationType(node);

        private void InferConditionalOperationType(BinaryOperation node)
        {
            var leftType = InferType(node.LeftOperand);
            var rightType = InferType(node.RightOperand);

            if (KnownType.Boolean.Equals(leftType) && KnownType.Boolean.Equals(rightType))
                throw new TypeInferenceException(leftType, rightType);

            _type = KnownType.Boolean;
        }
        #endregion

        #region relations
        public void VisitEqualOperation(EqualOperation node) => InferBinaryOperationType(node);
        public void VisitGreaterOperation(GreaterOperation node) => InferBinaryOperationType(node);
        public void VisitGreaterOrEqualOperation(GreaterOrEqualOperation node) => InferBinaryOperationType(node);
        public void VisitLessOperation(LessOperation node) => InferBinaryOperationType(node);
        public void VisitLessOrEqualOperation(LessOrEqualOperation node) => InferBinaryOperationType(node);
        public void VisitNotEqualOperation(NotEqualOperation node) => InferBinaryOperationType(node);
        #endregion

        #region expressions
        public void VisitArithmeticOperation(ArithmeticOperation node) => InferBinaryOperationType(node);
        public void VisitAssignment(Assignment node) => InferBinaryOperationType(node);
        public void VisitArrayCreation(ArrayCreation node) => InferSimpleType(node.ContainedType);
        public void VisitAsOperation(AsOperation node) => InferSimpleType(node.Type);
        public void VisitCastExpression(CastExpression node) => InferSimpleType(node.Type);
        public void VisitEmptyExpression(EmptyExpression node) => InferSimpleType(KnownType.Void);
        public void VisitInvokeExpression(InvokeExpression node) => InferSimpleType(node.MethodReference.ReturnType);
        public void VisitIsOperation(IsOperation node) => InferSimpleType(KnownType.Boolean);        
        public void VisitObjectCreation(ObjectCreation node) => InferSimpleType(node.Type);
        public void VisitPostfixDecrement(PostfixDecrement node) => InferSimpleType(node.Operand);
        public void VisitPostfixIncrement(PostfixIncrement node) => InferSimpleType(node.Operand);
        public void VisitPrefixDecrement(PrefixDecrement node) => InferSimpleType(node.Operand);
        public void VisitPrefixIncrement(PrefixIncrement node) => InferSimpleType(node.Operand);
        public void VisitUnaryOperation(UnaryOperation node) => InferSimpleType(node.Operand);
        public void VisitVarAccess(VarAccess node) => InferSimpleType(node.GetVarDeclaration().Type);

        public void VisitElementAccess(ElementAccess node)
        {
            var arrayType = InferType(node.Array) as ArrayType;
            var indexType = InferType(node.Index);

            if (arrayType == null) throw new TypeInferenceException("Array access expression must be type array");
            if (indexType != KnownType.Int32) throw new TypeInferenceException("Array element index must be only type int32");

            _type = indexType;
        }

        public void VisitElementStore(ElementStore node)
        {
            var arrayType = InferType(node.Array) as ArrayType;
            var indexType = InferType(node.Index);
            var valueType = InferType(node.Value);

            if (arrayType == null) throw new TypeInferenceException("Array access expression must have an array type");
            if (indexType != KnownType.Int32) throw new TypeInferenceException("Array element store index must have only a int32 type");
            if (!arrayType.ContainedType.Equals(valueType)) throw new TypeInferenceException("Array contained type must be assignable from a value type");

            _type = arrayType;
        }

        public void VisitLiteral(Literal node)
        {
            switch (node.Value.Tag)
            {
                case TokenTag.INT_LITERAL: InferSimpleType(KnownType.Int32); return;
                case TokenTag.FLOAT_LITERAL: InferSimpleType(KnownType.Single); return;
                case TokenTag.DOUBLE_LITERAL: InferSimpleType(KnownType.Double); return;
                case TokenTag.STRING_LITERAL: InferSimpleType(KnownType.String); return;
                case TokenTag.TRUE: InferSimpleType(KnownType.Boolean); return;
                case TokenTag.FALSE: InferSimpleType(KnownType.Boolean); return;
            }

            throw new TypeInferenceException("Can't infer a type from the literal: {0}", node.Value);
        }

        public void VisitTernaryOperation(TernaryOperation node)
        {
            var conditionType = InferType(node.Condition);
            if (!KnownType.Boolean.Equals(conditionType))
                throw new TypeInferenceException("Condition of a ternary operation must have a boolean type");

            var trueBranchType = InferType(node.TrueBranch);
            var falseBranchType = InferType(node.FalseBranch);

            if (!trueBranchType.Equals(falseBranchType))
                throw new TypeInferenceException(trueBranchType, falseBranchType);

            _type = trueBranchType;
        }
        #endregion

        #region types
        public void VisitArrayType(ArrayTypeNode node)
        {
            var containedType = InferType(node.ContainedType);
            _type = new ArrayType(containedType, node.Rank);
        }

        public void VisitPrimitiveType(PrimitiveTypeNode node)
        {
            var typeToken = node.TypeToken;
            var knownTypeCode = typeToken.Tag.GetKnownTypeCode();
            _type = KnownType.Get(knownTypeCode);
        }
        #endregion

        #region utils
        private void InferBinaryOperationType(BinaryOperation node)
        {
            var leftType = InferType(node.LeftOperand);
            var rightType = InferType(node.RightOperand);

            if (!leftType.Equals(rightType))
                throw new TypeInferenceException(leftType, rightType);

            _type = leftType;
        }

        private void InferSimpleType(ITypeInfo type)
        {
            _type = type;
        }

        private void InferSimpleType(TypeNode type)
        {
            _type = InferType(type);
        }

        private void InferSimpleType(Expression expression)
        {
            _type = InferType(expression);
        }
        #endregion
    }
}
