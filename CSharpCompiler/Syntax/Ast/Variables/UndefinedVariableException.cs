namespace CSharpCompiler.Syntax.Ast.Variables
{
    public class UndefinedVariableException : SyntaxException
    {
        public UndefinedVariableException(string name) : base("Undefined variable: {0}", name)
        { }
    }
}
