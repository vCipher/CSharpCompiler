using CSharpCompiler.Semantic.Cil;
using CSharpCompiler.Semantics.Metadata;
using System.Collections.Generic;
using System;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class DeclarationStmt : Stmt
    {
        public List<VarDeclaration> Declarations { get; private set; }

        public DeclarationStmt(List<VarDeclaration> declarations)
        {
            Declarations = declarations;
        }

        public DeclarationStmt(params VarDeclaration[] declarations)
        {
            Declarations = new List<VarDeclaration>(declarations);
        }

        public override void Build(MethodBuilder builder)
        {
            foreach (var declaration in Declarations)
                builder.Build(declaration);
        }
    }
}
