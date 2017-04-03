using CSharpCompiler.Syntax.Ast.Expressions.Conditions;
using CSharpCompiler.Syntax.Ast.Expressions.Relations;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public interface IExpressionVisitor
    {
        #region conditions
        void VisitAndOperation(AndOperation node);
        void VisitOrOperation(OrOperation node);
        #endregion

        #region relations
        void VisitEqualOperation(EqualOperation node);
        void VisitGreaterOperation(GreaterOperation node);
        void VisitGreaterOrEqualOperation(GreaterOrEqualOperation node);
        void VisitLessOperation(LessOperation node);
        void VisitLessOrEqualOperation(LessOrEqualOperation node);
        void VisitNotEqualOperation(NotEqualOperation node);
        #endregion

        #region expressions
        void VisitArithmeticOperation(ArithmeticOperation node);
        void VisitArrayCreation(ArrayCreation node);
        void VisitAsOperation(AsOperation node);
        void VisitAssignment(Assignment node);
        void VisitCastExpression(CastExpression node);
        void VisitElementAccess(ElementAccess node);
        void VisitElementStore(ElementStore node);
        void VisitEmptyExpression(EmptyExpression node);
        void VisitInvokeExpression(InvokeExpression node);
        void VisitIsOperation(IsOperation node);
        void VisitLiteral(Literal node);
        void VisitObjectCreation(ObjectCreation node);
        void VisitPostfixDecrement(PostfixDecrement node);
        void VisitPostfixIncrement(PostfixIncrement node);
        void VisitPrefixDecrement(PrefixDecrement node);
        void VisitPrefixIncrement(PrefixIncrement node);
        void VisitTernaryOperation(TernaryOperation node);
        void VisitUnaryOperation(UnaryOperation node);
        void VisitVarAccess(VarAccess node);
        #endregion
    }
}
