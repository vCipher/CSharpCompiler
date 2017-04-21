using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class DeclarationStatement : Statement
    {
        public List<VarDeclaration> Declarations { get; private set; }

        public DeclarationStatement(List<VarDeclaration> declarations)
        {
            Declarations = declarations;
        }

        public DeclarationStatement(params VarDeclaration[] declarations)
        {
            Declarations = new List<VarDeclaration>(declarations);
        }

        public override void Build(MethodBuilder builder)
        {
            foreach (var declaration in Declarations)
                declaration.Build(builder);
        }
    }
}
