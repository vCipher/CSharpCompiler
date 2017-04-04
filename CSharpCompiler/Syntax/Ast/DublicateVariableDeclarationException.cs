﻿namespace CSharpCompiler.Syntax.Ast
{
    public class DublicateVariableDeclarationException : SyntaxException
    {
        public DublicateVariableDeclarationException(string name) : base("Variable: {0}, is already defined", name)
        { }
    }
}
