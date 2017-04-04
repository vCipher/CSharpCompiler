using System;

namespace CSharpCompiler.Syntax
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message) { }
        public SyntaxException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
