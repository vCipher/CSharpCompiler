using System;

namespace CSharpCompiler.Syntax
{
    [Serializable]
    public class SyntaxException : Exception
    {
        public SyntaxException(string message) : base(message) { }
    }
}
