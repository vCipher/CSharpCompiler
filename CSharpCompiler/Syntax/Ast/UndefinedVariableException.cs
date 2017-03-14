using System;

namespace CSharpCompiler.Syntax.Ast
{
    [Serializable]
    public class UndefinedVariableException : SyntaxException
    {
        public UndefinedVariableException(string name) 
            : base("Undefined variable: {0}", name)
        { }
    }
}
