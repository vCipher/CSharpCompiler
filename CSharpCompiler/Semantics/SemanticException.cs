using System;

namespace CSharpCompiler.Semantics
{
    public class SemanticException : Exception
    {
        public SemanticException() { }
        public SemanticException(string message) : base(message) { }
        public SemanticException(string format, params object[] args) : base(string.Format(format, args)) { }
    }
}
