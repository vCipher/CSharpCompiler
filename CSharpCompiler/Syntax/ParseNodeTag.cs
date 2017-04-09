﻿namespace CSharpCompiler.Syntax
{
    public enum ParseNodeTag
    {
        Program,
        Terminal,
        Block,
        Stmt,
        StmtSeq,
        DeclarationStmt,
        ForStmt,
        VarDeclaration,
        Type,
        MultiplicativeExpression,
        AdditiveExpression,
        ShiftExpression,
        EqualityExpression,
        RelationalExpression,
        BitAndExpression,
        BitXorExpression,
        BitOrExpression,
        ConditionalAndExpression,
        ConditionalOrExpression,
        TernaryExpression,
        Assignment,
        PrimitiveType,
        ExpressionStmt,
        Expression,
        UnaryExpression,
        CastExpression,
        Literal,
        ParenthesisExpression,
        InvokeExpression,
        ArgumentList,
        Argument,
        VarAccess,
        ElementAccess,
        ExpressionList,
        PostfixDecrement,
        PostfixIncrement,
        ObjectCreation,
        VarDeclaratorList,
        VarDeclarator
    }
}
