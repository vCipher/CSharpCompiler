using CSharpCompiler.Syntax.Ast.Variables;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class DeclarationStatement : Statement
    {
        public IList<VarDeclaration> Declarations { get; private set; }

        public DeclarationStatement(IList<VarDeclaration> declarations)
        {
            Declarations = declarations;
        }

        public DeclarationStatement(params VarDeclaration[] declarations)
        {
            Declarations = declarations;
        }

        public override void Accept(IStatementVisitor visitor)
        {
            visitor.VisitDeclarationStatement(this);
        }
    }
}
