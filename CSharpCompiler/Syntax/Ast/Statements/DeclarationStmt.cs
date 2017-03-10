using CSharpCompiler.Syntax.Ast.Expressions;
using CSharpCompiler.Syntax.Ast.Types;
using System.Collections.Generic;

namespace CSharpCompiler.Syntax.Ast.Statements
{
    public sealed class DeclarationStmt : Stmt
    {
        public VarDeclaration Declaration { get; private set; }

        public DeclarationStmt(VarDeclaration declaration)
        {
            Declaration = declaration;
        }
    }
}
