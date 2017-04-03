using CSharpCompiler.Syntax.Ast.Variables;

namespace CSharpCompiler.Syntax.Ast.Expressions
{
    public sealed class VarAccess : Expression
    {
        public string VarName { get; private set; }
        public VarScope Scope { get; private set; }

        public VarAccess(string varName, VarScope scope)
        {
            VarName = varName;
            Scope = scope;
        }

        public override void Accept(IExpressionVisitor visitor)
        {
            visitor.VisitVarAccess(this);
        }

        public VarDeclaration GetVarDeclaration()
        {
            if (Scope == null) throw new UndefinedVariableException(VarName);
            return Scope.Resolve(VarName);
        }
    }
}
